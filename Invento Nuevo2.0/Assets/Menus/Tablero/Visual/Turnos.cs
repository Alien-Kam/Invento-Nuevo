using System.Collections;
using System.Collections.Generic;
using Logica;
using UnityEngine;

public class Turnos : MonoBehaviour
{
    Turno turno;
    // Start is called before the first frame update
    void Start()
    {
        turno = new Turno();
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void InicioTurno()
    {
        GameObject controlador = GameObject.Find("Controlador de juego ");
        GameManager controladorjueg = controlador.GetComponent<GameManager>();
        List<Player> players = controladorjueg.playerlog;
        List<GameObject> playersvisual = controladorjueg.players;
        Player player = turno.BegingTurn(players);
    }

    public void TerminaraTurno()
    {
        turno.EndTurn();
    }
}
