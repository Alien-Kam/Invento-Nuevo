using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logica;
using Unity.VisualScripting;
using System.Linq;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // Cosas de la logica
    public List<Player> playerlog;
    public Tablero tablero;
    public FuncionesTablero funcionesTablero;
    public bool terminojuego;

    // Cosas Visuales
    Turnos turnos;
    public Transform canvasTransform;
    public List<GameObject> players;
    public List<List<GameObject>> decks;
    public List<List<GameObject>> hands;
    public GameObject[] cementerio;
    public RondaVisual rondavisual;
    public List<GameObject> listdeck1;
    public List<GameObject> listdeck2;
    public List<GameObject> hand1;
    public List<GameObject> hand2;
    public GameObject posiciondeck1;
    public GameObject posiciondeck2;

    public void Awake()
    {
        // Inicializar las listas
        playerlog = new List<Player>();

        decks = new List<List<GameObject>>();
        players = new List<GameObject>();
        hands = new List<List<GameObject>>();
        hand1 = new List<GameObject>();
        hand2 = new List<GameObject>();

        tablero = new Tablero();
        funcionesTablero = new FuncionesTablero(tablero);

        //Esto busca al objeto en la escena 
        GameObject deck1 =  GameObject.Find("Deck 1");
        GameObject deck2 = GameObject.Find("Deck 2");

        GameObject player1 = GameObject.Find("Player 1");
        GameObject player2 = GameObject.Find("Player 2");
        
        posiciondeck1 = GameObject.Find("Posicion Deck 1");
        posiciondeck2 = GameObject.Find("Posicion Deck 2");

        GameObject seccion1 = GameObject.Find("Seccion 1");
        GameObject seccion2 = GameObject.Find("Seccion 2");

        cementerio = GameObject.FindGameObjectsWithTag("Cementerio");
        rondavisual = GameObject.Find("Controlador de Ronda").GetComponent<RondaVisual>();
        turnos = GameObject.Find("Controlador de Turno").GetComponent<Turnos>();
        //Agrega y crea referencias a listas
        listdeck1 = deck1.GetComponent<Decks>().deck;
        listdeck2 = deck2.GetComponent<Decks>().deck;

        decks.Add(listdeck1);
        decks.Add(listdeck2);

        players.Add(player1);
        players.Add(player2);

        hands.Add(hand1);
        hands.Add(hand2);


        //Este le asigna posicion deck como padre del deck 
        deck1.transform.SetParent(posiciondeck1.transform, false);
        deck2.transform.SetParent(posiciondeck2.transform, false);

        //Lo pone en la posicion del padre 
        deck1.transform.position = deck1.transform.parent.position;
        deck2.transform.position = deck2.transform.parent.position;

        //Le asigna seccion como padre del player
        player1.transform.SetParent(seccion1.transform, false);
        player2.transform.SetParent(seccion2.transform, false);


        //Crea las instancias y se las añado  la lista 
        Player player3 = new Player(player1.GetComponent<PlayersVisual>().nameplayer, player1.GetComponent<PlayersVisual>().faccionplayer);
        Player player4 = new Player(player2.GetComponent<PlayersVisual>().nameplayer, player2.GetComponent<PlayersVisual>().faccionplayer);

        playerlog.Add(player3);
        playerlog.Add(player4);

        funcionesTablero.InicializarTablero(tablero);

        // Hacer un metodo que se llame al incio del turno y pasarle las cosas a ese turno para inicializar las cosas
    }

    public void Start()
    {
        //Funciona
        terminojuego = false;
        
        for (int i = 0; i < listdeck1.Count; i++)
        {
            SwapValues(listdeck1);
            SwapValues(listdeck2);
        }
        ControlJuego(listdeck1, listdeck2);
    }

    private void SwapValues(List<GameObject> deck)
    {
        int indexA = Random.Range(0, deck.Count);
        int indexB = Random.Range(0, deck.Count);

        GameObject tempcard = deck[indexA];
        deck[indexA] = deck[indexB];
        deck[indexB] = tempcard;
    }

    //Este metodo controla el flujo del juego aunque este se cambiara ya que antes de iniciar un turno debo iniciar una ronda
    public void ControlJuego(List<GameObject> listdeck1, List<GameObject> listdeck2)
    {
        PreparacionRonda(listdeck1, listdeck2);
        turnos.InstanciarTurnos(playerlog);
        rondavisual.IniciarRonda();

        hand1 = hands[0];
        hand2 = hands[1];
    }

    public void PreparacionRonda(List<GameObject> listdeck1, List<GameObject> listdeck2)
    {
        rondavisual.InstanciarRondas(playerlog);

        //Este metodo da null porque las posiciones de las cartas se instancian en ronda visual y no en este
        //rondavisual.IniciarRonda();

    }
}

