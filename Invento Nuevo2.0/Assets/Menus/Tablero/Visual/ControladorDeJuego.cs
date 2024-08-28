
using System.Collections;
using System.Collections.Generic;
using Logica;
using UnityEngine;

public class ControladorDeJuego : MonoBehaviour
{

    List<GameObject> players;
    List<Player> playerlog;
    public void Awake()
    {
        GameObject deck1 = GameObject.Find("Deck 1");
        GameObject deck2 = GameObject.Find("Deck 2");

        GameObject player1 = GameObject.Find("player1");
        GameObject player2 = GameObject.Find("player2");

        GameObject seccion1 = GameObject.Find("Seccion Jugador 1");
        GameObject seccion2 = GameObject.Find("Seccion Jugador 2");

        deck1.transform.SetParent(seccion1.transform, false);
        deck2.transform.SetParent(seccion2.transform, false);

        player1.transform.SetParent(seccion1.transform, false);
        player2.transform.SetParent(seccion2.transform, false);

        players.Add(player1);
        players.Add(player2);

        Player player3 = new Player(player1.GetComponent<Player>().nombreplayer, player1.GetComponent<Player>().faccion);
        Player player4 = new Player(player2.GetComponent<Player>().nombreplayer, player2.GetComponent<Player>().faccion);

        playerlog.Add(player3);
        playerlog.Add(player4);
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Control()
    {
        
    }
}
