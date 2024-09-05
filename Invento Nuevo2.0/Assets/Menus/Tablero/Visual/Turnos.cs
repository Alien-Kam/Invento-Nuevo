using System.Collections;
using System.Collections.Generic;
using Logica;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

// Kam si funciona no lo toques 
public class Turnos : MonoBehaviour
{
    private Turno turno;
    public bool termino;
    public GameObject panel;
    public TMP_Text textpanel;
    public float activeTime = 0.01f;
    public Player player;
    public List<TMP_Text> textospunt;
    public int current;
    public bool[] pasados;
    public int playerspasados;
    // Start is called before the first frame update
    public void Start()
    {
        termino = false;
        current = 0;
        playerspasados = 0;
    }

    public void instanciasTurnos(List<Player> player)
    {
        turno = new Turno(player);
    }

    public void InicioTurno()
    {
        termino = false;
        player = turno.BegingTurn(turno.pasados);
        StartCoroutine(ActivatePanel());
        textpanel.text = $"Es el turno de :  {player.nombreplayer}";
    }

    public void PasarTurno()
    {
        Debug.Log("Hola");
        turno.pasados[current] = true;
        playerspasados ++;
        termino = true;
        Debug.Log(turno.pasados[current]);
    }

    public void TerminaraTurno()
    {
        turno.EndTurn();
        textospunt[current].text = $"{player.puntos}";
        current = (current + 1) % textospunt.Count;
    }

    IEnumerator ActivatePanel()
    {
        panel.SetActive(true); // Activa el panel
        yield return new WaitForSeconds(activeTime); // Espera el tiempo especificado
        panel.SetActive(false); // Desactiva el panel
    }

    public void Update()
    {
        if (termino)
        {
            TerminaraTurno();
            InicioTurno();
        }
    }
}
