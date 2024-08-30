using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logica;
using UnityEngine.EventSystems;
public class DragandDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    Vector3 starPosition; // Posicion inicial 
    Transform starParent; //posicion inicial del padre 
    Transform dragParent; //Es cuando se haga drag en el item se coloque dentro de ese parent
    public GameObject itemDragging; // gameobject en movimiento 

    // Start is called before the first frame update
    void Start()
    {
      dragParent = GameObject.FindGameObjectWithTag("DragParent").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
         itemDragging = gameObject; // El objeto que se mueve 
        starPosition = transform.position; // posicion inicial
        starParent = transform.parent; // posicion del padre 
        transform.SetParent(dragParent, false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemDragging = null;

        if (transform.parent == dragParent)
        {
            transform.position = starPosition;

            transform.SetParent(starParent);
        }
    }
}
