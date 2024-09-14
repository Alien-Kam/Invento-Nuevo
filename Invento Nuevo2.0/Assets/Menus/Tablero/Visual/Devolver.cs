using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Logica;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;

public class Devolver : MonoBehaviour, IPointerClickHandler
{
    public GameObject panel;
    public GameObject[] posicion1;
    public GameObject[] posicion2;
    public List<GameObject> cartasdevul;
    public List<bool> playerob;
    RondaVisual rondavisual;
    Rondas ronda;
    Turnos turno;
    public List<DevolverCartas> posiciones;
   public List<List<GameObject>> cementerios;
   bool[] jugadores;
   public bool norobar;
   public TMP_Text texto;
   int current;
   

    // Start is called before the first frame update
    public void Start()
    {
        cementerios = new List<List<GameObject>>();
        cartasdevul = new List<GameObject>();
        posicion1 = GameObject.FindGameObjectsWithTag("Posiciones 1");
        posicion2 = GameObject.FindGameObjectsWithTag("Posiciones 2");
        rondavisual = GameObject.Find("Controlador de Ronda").GetComponent<RondaVisual>();
        turno = GameObject.Find("Controlador de Turno").GetComponent<Turnos>();
        ronda = rondavisual.rondas;
        jugadores = new bool[rondavisual.listdecks.Count];
        Debug.Log(ronda);
    }

    // Update is called once per frame
    public void Update() { }

    public void OnPointerClick(PointerEventData eventData)
    {
        panel.SetActive(true);
        norobar = turno.inicioronda;
        current = turno.turno.current;
        if(!norobar)
        {   
            Debug.Log(texto);
            texto.GetComponent<TextMeshProUGUI>().text = "No se puede robar mas";
            return;
        }
        for (int i = 0; i < posicion1.Length; i++)
        {
            posicion1[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
            posicion2[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
    public void Aceptar()
    {
        for (int i = 0; i < posicion1.Length; i++)
        {
            Debug.Log("Entro");
            posicion1[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
            posicion2[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        for (int i = 0; i < cartasdevul.Count; i++)
        {
            if (cartasdevul[i] == null)
            {
                continue;
            }
            rondavisual.Robar(cartasdevul[i]);
        }

        for (int i = 0; i < posiciones.Count; i++)
        {
            posiciones[i]._item = null;
        }
        panel.SetActive(false);
        jugadores[current] = true;
    }
}
