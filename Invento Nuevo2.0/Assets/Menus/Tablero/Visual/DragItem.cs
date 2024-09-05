using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logica;
using UnityEngine.EventSystems;
using System;
public class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler
{
  private GameObject _dragParent;
  private bool _isDropped = false;
  private bool _enableDrop = false;
  private Vector3 _startingPosition;
  private Transform _startingParent;
  GameObject card;
  GameManager control;
  public bool move;
  Turnos turnoactual;
  GameObject player;
  public bool ok;

  public void Start()
  {
    move = false;
    ok = false;
    _dragParent = GameObject.Find("DragParent");
    turnoactual = GameObject.Find("Controlador de Turno").GetComponent<Turnos>();
    control = GameObject.Find("Controlador de juego ").GetComponent<GameManager>();
    card = gameObject;
  }

  public void Valido()
  {
    ok = GetPathParent(card, control.players[turnoactual.current]);

  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    Valido();
    Debug.Log(ok);
    if (move || !ok) return;
    _startingPosition = transform.position;
    _startingParent = transform.parent;
    transform.SetParent(_dragParent.transform);
  }

  public void OnDrag(PointerEventData eventData)
  {
    if (move || !ok) return;
    transform.position = eventData.position;
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    if (move || !ok) return;
    if (_isDropped)
    {
      return;
    }
    ReturnToStartingPosition();
  }

  public void ReturnToStartingPosition()
  {
    transform.position = _startingPosition;
    transform.SetParent(_startingParent);
  }

  public void IsDropped(bool dropped)
  {
    _isDropped = dropped;
  }

  public bool IsEnableDrop()
  {
    return _enableDrop;
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    Debug.Log("Hola aqui estamos");
  }

  public bool GetPathParent(GameObject newitem, GameObject player)
  {
    string tags = "Seccion";
    GameObject parent1 = FindParentWithTag(newitem, tags); // Este no lo capta bien 
    Debug.Log($"Este es el padre:  {parent1}");
    GameObject parent2 = FindParentWithTag(player, tags);

    bool ok1 = parent1.Equals(parent2);
    return ok1;

  }
  public GameObject FindParentWithTag(GameObject child, string tag)
  {
     Transform parentTransform = child.transform.parent;
    while (parentTransform != null)
    {
        if (parentTransform.CompareTag(tag))
        {
            return parentTransform.gameObject;
        }
        parentTransform = parentTransform.parent;
    }
    return null; 
  }
}