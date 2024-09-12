using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Logica;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Cementerio : MonoBehaviour, IPointerClickHandler
{
    GameObject cemt;
    List<GameObject> cementerio;
    public List<GameObject> posicionestab;
    public GameObject cementerioscroll;
    Turno turnoactual;
    Turnos turnovisual;
    public List<GameObject> posvisual;
    public List<Transform> posscroll;
    public GameObject[] cartas;
    

    // Start is called before the first frame update
    public void Start()
    {
        cemt = this.gameObject;
        cementerio = new List<GameObject>();
        turnovisual = GameObject.Find("Controlador de Turno").GetComponent<Turnos>();
        cementerioscroll.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Posiciones(string tag)
    {
        posicionestab = GameObject.FindGameObjectsWithTag(tag).ToList();
    }
    public void CartasCementerio()
    {
        for (int i = 0; i < posicionestab.Count; i++)
        {
            DropPosition pos = posicionestab[i].GetComponent<DropPosition>();
            if (pos._item != null)
            {
                cementerio.Add(pos._item);
                pos._item.transform.SetParent(cemt.transform);
                pos._item.transform.position = cemt.transform.position;
                pos._item = null;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        cementerioscroll.SetActive(true);
        posvisual = GameObject.FindGameObjectsWithTag("Posiciones Scroll").ToList();
        posscroll= new List<Transform>();
        Debug.Log(posvisual.Count);
        for (int i = 0; i < posvisual.Count; i++)
        {
          posscroll.Add(posvisual[i].transform);
        }
        cartas = GameObject.FindGameObjectsWithTag("Cartas");
        Debug.Log(cartas.Length);
        MiraCementerio();
    }


    public void MiraCementerio()
    {
        Debug.Log(posscroll.Count);
        for (int i = 0; i < cementerio.Count; i++)
        {
            cementerio[i].transform.SetParent(posscroll[i].transform);
            cementerio[i].transform.localPosition = Vector3.zero;  // Arreglar
        }

        for(int i = 0; i < cartas.Length; i++)
        {
            cartas[i].GetComponent<CanvasGroup>() .blocksRaycasts = false;     
        }
    }

    public void Aceptar()
    {
        Debug.Log(posscroll);
        for (int i = 0; i < posscroll.Count; i++)
        {
            if (posscroll[i].transform.childCount != 0)
            {
                GameObject posicion = posscroll[i].transform.GetChild(0).gameObject;
                posicion.transform.SetParent(cemt.transform);
                posicion.transform.localPosition = Vector3.zero;
            }
        }
        cementerioscroll.SetActive(false);
        for(int i = 0; i < cartas.Length; i++)
        {
            cartas[i].GetComponent<CanvasGroup>() .blocksRaycasts = true;     
        }
    }
}
