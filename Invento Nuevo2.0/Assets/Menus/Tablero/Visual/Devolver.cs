using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Devolver : MonoBehaviour, IDropHandler
{
     private GameObject _item;
     GameObject control;
     GameManager controlador;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("Control del juego");
        controlador = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
         GameObject newitem = eventData.pointerDrag;
        _item = newitem;
        _item.transform.position = transform.position;
        _item.transform.SetParent(transform);
    }

    public void Aceptar()
    {

    }
}
