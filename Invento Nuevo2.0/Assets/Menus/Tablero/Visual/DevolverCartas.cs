using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class DevolverCartas : Devolver, IDropHandler
{
    public GameObject _item;
    Devolver cant;
    // Start is called before the first frame update
    public void OnDrop(PointerEventData eventData)
    {
        if(_item != null)
        {
            return;
        }
        Debug.Log("Entro");
        GameObject newitem = eventData.pointerDrag;
        _item = newitem;
        DragItem compitem = _item.GetComponent<DragItem>();
        _item.transform.SetParent(transform);
        _item.transform.position = transform.position + new Vector3(0, 0, 1);
        cant = GameObject.Find("Devolver Cartas").GetComponent<Devolver>();
        cant.cartasdevul.Add(_item);
        compitem.IsDropped(true);

    }
}
