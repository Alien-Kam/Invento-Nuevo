using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Logica;
using UnityEngine;

public class Ronda : MonoBehaviour
{
  GameManager control;
  Turnos turno;
  // Start is called before the first frame update
  public void Start()
  {
    control = GameObject.Find("Controlador de juego ").GetComponent<GameManager>();
    turno = GameObject.Find("Controlador de Turno").GetComponent<Turnos>();
  }

  // Update is called once per frame
  public void Update()
  {
    if(turno.playerspasados == control.players.Count)
    {
      TerminarRonda();
    }
  }

  Rondas ronda = new Rondas();

  //Este metodo distribuye las cartas y las llama a instanciar una por una elimina las cartas del deck y las pone en la mano 
  public List<GameObject> DistribuirCard(List<GameObject> deck, GameObject posiciones, List<bool> posicionescartas) // No hay cartas repetidads tengo que arreglarlo
  {
    List<BaseCard> deckplayer = new List<BaseCard>(); // Se puede hacer para que devuelva tambien el deck 
    List<GameObject> handvisual;
    List<BaseCard> hand;

    for (int i = 0; i < deck.Count; i++)  // Aqui esta el problema 
    {
      deckplayer.Add(deck[i].GetComponent<Cartas>().CrearCarta());
    }

    hand = ronda.DistribucionCartas(deckplayer, posicionescartas);

    // Toma los elementos desde el final del deck en la cantidad designada, luego lo transforma en lista con ToList
    handvisual = deck.TakeLast(hand.Count).ToList();
    // Remueve los elementos del deck en un rango
    deck.RemoveRange(deck.Count - handvisual.Count, handvisual.Count);

    for (int i = 0; i < handvisual.Count; i++)
    {
      InstanciarCartas(handvisual[i], posicionescartas, posiciones);
    }
    return handvisual;
  }
  /* Cada posicion de la mano va a tener un hijo que seria la instancia de la carta */
  void InstanciarCartas(GameObject card, List<bool> posicionescartas, GameObject posiciones)
  {
    int index = posicionescartas.FindIndex(x => !x);

    Transform child = posiciones.transform.GetChild(index);
    GameObject newInstance = Instantiate(card, child.position, Quaternion.identity);
    newInstance.transform.SetParent(child);
    newInstance.AddComponent<DragItem>();
    newInstance.tag = "Cartas";
    posicionescartas[index] = true;
  }

  public void Robar(GameObject card, List<GameObject> deck, List<bool> posiciones, GameObject positionhand)
  {
    BaseCard cartadevuelta = card.GetComponent<Cartas>().CrearCarta();
    List<BaseCard> decklog = new List<BaseCard>();

    for (int i = 0; i < deck.Count; i++)
    {
      decklog.Add(deck[i].GetComponent<Cartas>().CrearCarta());
    }

    BaseCard carta = ronda.IntercambioCarta(cartadevuelta, decklog);
    GameObject cartaretorno = deck[0];
    deck.Add(card);
    deck.Remove(cartaretorno);
    Destroy(card);

    InstanciarCartas(cartaretorno, posiciones, positionhand);
  }

  public void TerminarRonda()
  {
    Debug.Log("Termino la ronda");
    turno.playerspasados  = 0;
    for(int i = 0; i < turno.pasados.Length; i++)
    {
      turno.pasados[i] = false;
    }
    
  }
}
