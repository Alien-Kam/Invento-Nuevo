using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logica;
using Unity.VisualScripting;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> players;
    List<List<GameObject>> hands;
    public Transform canvasTransform;
    public List<Player> playerlog;
    GameObject deck1;
    GameObject deck2;
    List<bool> posicionescartas1;
    List<bool> posicionescartas2;
    GameObject posicion1;
    GameObject posicion2;
    public Tablero tablero;
    public FuncionesTablero funcionesTablero;

    public void Awake()
    {
        players = new List<GameObject>();
        playerlog = new List<Player>();
        posicionescartas1 = new List<bool>();
        posicionescartas2 = new List<bool>();

        //Esto busca al objeto en la escena 
        deck1 = GameObject.Find("Deck 1");
        deck2 = GameObject.Find("Deck 2");

        GameObject player1 = GameObject.Find("Player 1");
        GameObject player2 = GameObject.Find("Player 2");
  
        //Lo añade a la lista de players una herramienta misteriosa que utilizaremos mas tarde 
        players.Add(player1);
        players.Add(player2);

        GameObject posiciondeck1 = GameObject.Find("Posicion Deck 1");
        GameObject posiciondeck2 = GameObject.Find("Posicion Deck 2");

        GameObject seccion1 = GameObject.Find("Seccion 1");
        GameObject seccion2 = GameObject.Find("Seccion 2");

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

       tablero = new Tablero();
       funcionesTablero = new FuncionesTablero(tablero);
       funcionesTablero.InicializarTablero(tablero);

        // Hacer un metodo que se llame al incio del turno y pasarle las cosas a ese turno para inicializar las cosas
    }

    public void Start()
    {


        //Funciona
        hands = new List<List<GameObject>>();
        List<GameObject> listdeck1 = deck1.GetComponent<Decks>().deck;
        List<GameObject> listdeck2 = deck2.GetComponent<Decks>().deck;
        for (int i = 0; i < listdeck1.Count; i++)
        {
            SwapValues(listdeck1);
            SwapValues(listdeck2);
        }

        posicion1 = GameObject.Find("Posicion de la mano 1");
        posicion2 = GameObject.Find("Posicion de la mano 2");

        for (int i = 0; i < posicion1.transform.childCount; i++)
        {
            posicionescartas1.Add(false);
            posicionescartas2.Add(false);
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

    public void ControlJuego(List<GameObject> listdeck1, List<GameObject> listdeck2)
    {
        PreparacionRonda(listdeck1, listdeck2);
        Turnos turnos = GameObject.Find("Controlador de Turno").GetComponent<Turnos>();
        turnos.instanciasTurnos(playerlog);
        turnos.InicioTurno();
    }

    public void PreparacionRonda(List<GameObject> listdeck1, List<GameObject> listdeck2)
    {
        Ronda ronda = new Ronda();
        List<GameObject> hand1;
        List<GameObject> hand2;

        hand1 = ronda.DistribuirCard(listdeck1, posicion1, posicionescartas1);
        hand2 = ronda.DistribuirCard(listdeck2, posicion2, posicionescartas2);
        hands.Add(hand1);
        hands.Add(hand2);
    }
}

