using System.Collections;
using System.Collections.Generic;
using Logica;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

// Kam si funciona no lo toques 
public class Turnos : MonoBehaviour
{
    public Turno turno;
    public bool termino;
    public GameObject panel;
    public TMP_Text textpanel;
    public float tiempo;
    public Player player;
    public List<TMP_Text> textospunt;
    public int current;
    public bool[] pasados;
    public int playerspasados;
    public bool inicioronda;
    public List<CanvasGroup> sections;

    RondaVisual ronda;
    // Start is called before the first frame update
    public void Start()
    {
        termino = false;
        tiempo = 6f;
        playerspasados = 0;
        ronda = GameObject.Find("Controlador de Ronda").GetComponent<RondaVisual>();
    }

    public void Update()
    {
        if (!ronda.terminoronda && termino)
        {
            TerminarTurno();
        }
        // Hacer un metodo 
        if (!ronda.terminoronda && termino && !ronda.isPanelActive)
        {
            InicioTurno();
        }
    }

    public void InstanciarTurnos(List<Player> player)
    {
        turno = new Turno(player);
        inicioronda = true;
        current = turno.current;
        pasados = new bool[player.Count];
    }

    public void ReinicioTurnos()
    {
        Debug.Log("Reinicio Ronda");
        termino = false;
        turno.ReinicioTurno();
        pasados = turno.pasados;
        playerspasados = turno.jugadorespas;
        current = turno.current;
    }

    public void InicioTurno(int current = -1)
    {
        termino = false;
        player = turno.BegingTurn(turno.pasados, current);
        StartCoroutine(ActivatePanel(panel, tiempo));
        textpanel.text = $"Es el turno de :  {player.nombreplayer}";
    }

    public void PasarTurno()
    {
        // hacer un metodo en la logica  con todo esto 
        current = turno.current;
        pasados[current] = true;
        turno.pasados[current] = true;
        playerspasados++;
        termino = true;
    }

    public void TerminarTurno()
    {
        textospunt[turno.current].text = $"{player.puntos}";
        turno.EndTurn();
    }

    public IEnumerator ActivatePanel(GameObject panel, float activeTime)
    {
        panel.SetActive(true); // Activa el panel
        yield return new WaitForSeconds(activeTime); // Espera el tiempo especificado
        panel.SetActive(false); // Desactiva el panel
    }
}
