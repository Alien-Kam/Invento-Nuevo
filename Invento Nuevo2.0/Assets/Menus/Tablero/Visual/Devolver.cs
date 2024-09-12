using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Devolver : MonoBehaviour, IPointerClickHandler
{
    public GameObject panel;
    private GameObject[] posicion1;
    private GameObject[] posicion2;
    public List<GameObject> cartasdevul;
    RondaVisual ronda;

    public List<DevolverCartas> posiciones;

    // Start is called before the first frame update
    public void Start()
    {
        cartasdevul = new List<GameObject>();
        posicion1 = GameObject.FindGameObjectsWithTag("Posiciones 1");
        posicion2 = GameObject.FindGameObjectsWithTag("Posiciones 2");
        ronda = GameObject.Find("Controlador de Ronda").GetComponent<RondaVisual>();
        Debug.Log(posicion1.Length);
        Debug.Log(posicion2.Length);
    }

    // Update is called once per frame
    public void Update() { }

    public void OnPointerClick(PointerEventData eventData)
    {
        panel.SetActive(true);
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
            posicion1[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
            posicion2[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        for (int i = 0; i < cartasdevul.Count; i++)
        {
            if(cartasdevul[i] == null)
            {
                continue;
            }
           ronda.Robar(cartasdevul[i]);
        }

        for(int i = 0; i < posiciones.Count; i++)
        {
            Debug.Log(posiciones[i]._item);
        }


    }
}
