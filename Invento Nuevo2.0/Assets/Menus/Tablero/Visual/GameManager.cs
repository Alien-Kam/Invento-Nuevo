using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logica;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    List<GameObject> players = new List<GameObject>();
    List<List<GameObject>> hands = new List<List<GameObject>>();
    public Transform canvasTransform;
    List<Player> playerlog = new List<Player>();
    GameObject deck1;
    GameObject deck2;
    List<bool> posicionescartas1 = new List<bool>();
    List<bool> posicionescartas2 = new List<bool>();

    GameObject posicion1;
    GameObject posicion2;

    public void Awake()
    {

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
    }

    public void Start()
    {
        //Funciona
        for (int i = 0; i < deck1.GetComponent<Decks>().deck.Count; i++)
        {
            int indexA = Random.Range(0, deck1.GetComponent<Decks>().deck.Count);
            int indexB = Random.Range(0, deck1.GetComponent<Decks>().deck.Count);

            GameObject tempcard = deck1.GetComponent<Decks>().deck[indexA];
            deck1.GetComponent<Decks>().deck[indexA] = deck1.GetComponent<Decks>().deck[indexB];
            deck1.GetComponent<Decks>().deck[indexB] = tempcard;

            int indexC = Random.Range(0, deck2.GetComponent<Decks>().deck.Count);
            int indexD = Random.Range(0, deck2.GetComponent<Decks>().deck.Count);

            GameObject temcarddeck2 = deck2.GetComponent<Decks>().deck[indexC];
            deck2.GetComponent<Decks>().deck[indexC] = deck2.GetComponent<Decks>().deck[indexD];
            deck2.GetComponent<Decks>().deck[indexD] = temcarddeck2;
        }

         posicion1 = GameObject.Find("Posicion de la mano 1");
         posicion2 = GameObject.Find("Posicion de la mano 2");

        for (int i = 0; i < posicion1.transform.childCount; i++)
        {
            posicionescartas1.Add(false);
            posicionescartas2.Add(false);
        }
        Debug.Log(posicion1.transform.childCount);
        Debug.Log(posicion1.transform.childCount);
        ControlJuego();
    }


    public void ControlJuego()
    {
        Debug.Log("Control del juego");
        PreparacionRonda();
    }

    public void PreparacionRonda()
    {
        PruebaRonda ronda = new PruebaRonda();
        List<GameObject> hand1 = new List<GameObject>();
        List<GameObject> hand2 = new List<GameObject>();

        Debug.Log("PreparacionRonda");

        hand1 = ronda.DistribuirCard(deck1.GetComponent<Decks>().deck, posicion1, posicionescartas1);
        hand2 = ronda.DistribuirCard(deck2.GetComponent<Decks>().deck, posicion2, posicionescartas2);
    }
}

