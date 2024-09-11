using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Devolver : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    private GameObject _item;
    public GameObject panel;

    // Start is called before the first frame update
    public void Start() { }

    // Update is called once per frame
    public void Update() { }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject newitem = eventData.pointerDrag;
        _item = newitem;
        _item.transform.position = transform.position;
        _item.transform.SetParent(transform);
    }

    public void Aceptar() { }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("BDMBDBMDBMDBM");
    }
}
