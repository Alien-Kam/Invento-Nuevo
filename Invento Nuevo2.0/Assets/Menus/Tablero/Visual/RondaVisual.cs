using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Logica;
using TMPro.EditorUtilities;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.Mathematics;

public class RondaVisual : MonoBehaviour
{

  //Variables de la logica
  Rondas rondas;
  List<Player> player;
  public bool terminoronda;
  int currentplayer => turnos.turno.current;

  //Variables del visual
  Turnos turnos;
  List<List<GameObject>> listdecks;
  List<List<GameObject>> handList;
  List<GameObject> posiciones;
  public List<TMP_Text> puntosRonda;
  public List<TMP_Text> puntos;
  public TMP_Text textodeganador;
  public GameObject panel;
  GameManager control;
  GameObject[] cementerios;
  List<string> tags;
  float tiempo;
  public bool isPanelActive;
  public List<GameObject> posicionesLider;
  // Start is called before the first frame update
  public void Start()
  {
    //Busca objetos de la escena
    posiciones = new List<GameObject>();
    control = GameObject.Find("Controlador de juego ").GetComponent<GameManager>();
    turnos = GameObject.Find("Controlador de Turno").GetComponent<Turnos>();

    GameObject posicion1 = GameObject.Find("Posicion de la mano 1");
    GameObject posicion2 = GameObject.Find("Posicion de la mano 2");
    //Instancias las variables
    tags = new List<string>();
    posiciones = new List<GameObject>();

    tiempo = 3f;
    terminoronda = false;
    isPanelActive = false;

    listdecks = control.decks;
    handList = control.hands;
    cementerios = control.cementerio;
    puntos = turnos.textospunt;


    //Agrecar cosas a las listas 
    posiciones.Add(posicion1);
    posiciones.Add(posicion2);

    tags.Add("Posiciones 1");
    tags.Add("Posiciones 2");

    // Como me dio pereza  esto le asigna las posiciones a los cementerios que deben recorrer forma parte de buscar objetos
    //en la escena
    for (int i = 0; i < cementerios.Length; i++)
    {
      cementerios[i].GetComponent<Cementerio>().Posiciones(tags[i]);
    }
  }

  // Update is called once per frame
  public void Update()
  {
    if (turnos.playerspasados == control.playerlog.Count)
    {
      Debug.Log("Termino Ronda");
      TerminarRonda();
    }
    // Detecta si se presiona la tecla Enter
    if (isPanelActive && Input.GetKeyDown(KeyCode.Return))
    {
      panel.SetActive(false); // Desactiva el panel
      isPanelActive = false; // Actualiza el estado del panel
      LimpiarCampo();
    }
    if (!control.terminojuego && terminoronda && !isPanelActive)
    {
      Debug.Log("Creo una nueva ronda");
      InstanciarRondas(control.playerlog);
      IniciarRonda();
    }
  }


  //Se llama al principio para crear una nueva ronda
  public void InstanciarRondas(List<Player> player)
  {
    rondas = new Rondas(player);
  }

  //Este es el inicio de toda ronda
  public void IniciarRonda()
  {
    // Porqye esto devuelve null y abajo en el metodo de terminar una ronda si tiene un valor asignado 
    //Creo que es porque en este momento au un turno no se haba instanciado tengo que arreglar eso 
    // Ademas tengo que arreglar que cuando comience una ronda empiece el ganador
    terminoronda = false;
    CrearManos(listdecks, control.hands);
    turnos.ReinicioTurnos();
    turnos.InicioTurno();
  }

  // Este asigna con que cartas jugara cada jugador
  public void CrearManos(List<List<GameObject>> decks, List<List<GameObject>> hands)
  {
    for (int i = 0; i < decks.Count; i++)
    {
      GameObject lider = decks[i].Find(x => x.GetComponent<Cartas>().tipoCarta == TipoCarta.Lider);

      if (lider != null)
      {
        InstanceLider(lider, posicionesLider[i].transform);
        decks[i].Remove(lider);
      }
      Distribuir(decks[i], posiciones[i]);
    }
  }

  //Lo primero que se nstancian son los lideres de cada faccion
  public void InstanceLider(GameObject lider, Transform pos)
  {
    GameObject newInstance = Instantiate(lider, pos.position, Quaternion.identity);
    newInstance.transform.SetParent(pos);
  }

  public void Distribuir(List<GameObject> deck, GameObject posiciones)
  {
    List<BaseCard> deckplayer = new List<BaseCard>();
    List<BaseCard> handplayer = new List<BaseCard>();
    List<GameObject> cartinstnacias = new List<GameObject>();
    List<Transform> handPoss = new List<Transform>();
    List<BaseCard> cartasinstanciar;
    int cantcard = 0;

    for (int i = 0; i < deck.Count; i++)
    {
      deckplayer.Add(deck[i].GetComponent<Cartas>().CrearCarta());
    }

    for (int i = 0; i < posiciones.transform.childCount; i++)
    {
      var child = posiciones.transform.GetChild(i);
      PosicionMano pos = child.GetComponent<PosicionMano>();
      if (pos.ocupada)
      {
        handplayer.Add(child.transform.GetComponentInChildren<Cartas>().CrearCarta());
        continue;
      }
      cantcard++;
      handPoss.Add(child);
    }
    cartasinstanciar = rondas.CartasMano(cantcard, handplayer, deckplayer);

    for (int i = 0; i < cantcard; i++)
    {
      GameObject card = deck.Find(x => x.GetComponent<Cartas>().nombre == cartasinstanciar[i].namecard);
      if (card == null) break;
      cartinstnacias.Add(card);
      deck.Remove(card);
    }

    for (int i = 0; i < cartinstnacias.Count; i++)
    {
      InstanciarCartas(cartinstnacias[i], handPoss[i]);
    }
  }

  /* Cada posicion de la mano va a tener un hijo que seria la instancia de la carta */
  private GameObject InstanciarCartas(GameObject card, Transform posiciones)
  {
    GameObject gameObject = Instantiate(card, posiciones.position, Quaternion.identity);
    gameObject.transform.SetParent(posiciones);
    gameObject.AddComponent<DragItem>();
    gameObject.AddComponent<CanvasGroup>();
    gameObject.tag = "Cartas";
    gameObject.layer = 6;

    return gameObject;
  }

  public void Robar(GameObject card)
  {
    BaseCard cartadevuelta = card.GetComponent<Cartas>().CrearCarta();
    List<BaseCard> decklog = new List<BaseCard>();

    for (int i = 0; i < listdecks[currentplayer].Count; i++)
    {
      decklog.Add(listdecks[currentplayer][i].GetComponent<Cartas>().CrearCarta());
    }

    BaseCard carta = rondas.IntercambioCarta(cartadevuelta, decklog);
    GameObject cartaretorno = listdecks[currentplayer][0];
    listdecks[currentplayer].Add(card);
    listdecks[currentplayer].Remove(cartaretorno);
    Destroy(card);

    for (int i = 0; i < posiciones[currentplayer].transform.childCount; i++)
    {
      var child = posiciones[currentplayer].transform.GetChild(i);
      PosicionMano pos = child.GetComponent<PosicionMano>();
      if (!pos.ocupada)
      {
        InstanciarCartas(cartaretorno, child);
        break;
      }
    }

  }

  //Estos son los metodos de Terminar una ronda
  public void TerminarRonda()
  {
    terminoronda = true;
    Pasados();
    AsignarPuntos();
    GanadorRonda(); // El problema de los puntos esta aqui
  }

  public void Pasados()
  {
    turnos.playerspasados = 0;
    for (int i = 0; i < turnos.pasados.Length; i++)
    {
      turnos.pasados[i] = false;
    }
    // Hasta ahora funciona
    rondas.BorrarPasados(turnos.turno.pasados);
  }

  //Asigna los puntos de las rondas
  public void AsignarPuntos()
  {
    player = rondas.player;
    rondas.PuntosRonda(player);

    for (int i = 0; i < puntosRonda.Count; i++)
    {
      puntosRonda[i].text = $"{player[i].puntosronda}";
    }
  }

  //Decide un ganador de la ronda
  public void GanadorRonda()
  {
    Player playerwin = rondas.GanadorRonda(player);

    MostrarGanador();

    if (playerwin != null)
    {
      textodeganador.text = $"El ganador de la ronda es {playerwin.nombreplayer} :)";
      //rondas.SwapWinner(playerwin, control.playerlog);
    }
    else
    {
      textodeganador.text = "Empate";
    }
  }

  public void LimpiarCampo()
  {
    for (int i = 0; i < player.Count; i++)
    {
      player[i].puntos = 0;
      puntos[i].text = "0";
      cementerios[i].GetComponent<Cementerio>().CartasCementerio();
    }
  }

  public void MostrarGanador()
  {
    panel.SetActive(true);
    isPanelActive = true;
  }
}
