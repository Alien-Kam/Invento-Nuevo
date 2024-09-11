using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logica;
using UnityEngine.EventSystems;
using System;
public class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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
  public BaseCard carta;
  private CanvasGroup canvas;
  public bool ok;

  public void Start()
  {
    move = false;
    ok = false;
    _dragParent = GameObject.Find("DragParent");
    canvas = _dragParent.GetComponent<CanvasGroup>();
    turnoactual = GameObject.Find("Controlador de Turno").GetComponent<Turnos>();
    control = GameObject.Find("Controlador de juego ").GetComponent<GameManager>();
    card = gameObject;
  }

  public void Valido()
  {
    player = control.players[turnoactual.turno.current];
    ok = GetPathParent(card, player);

  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    Valido();
    if (move || !ok) return;
    _startingPosition = transform.position;
    _startingParent = transform.parent;
    transform.SetParent(_dragParent.transform);
    canvas.blocksRaycasts = false;
  }
  public void OnDrag(PointerEventData eventData)
  {
    if (move || !ok) return;
    transform.position = eventData.position;
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    canvas.blocksRaycasts = true;
    if (move || !ok) return;
    if (_isDropped)
    {
      move = true;
      Debug.Log(_isDropped);
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

  public bool GetPathParent(GameObject newitem, GameObject player)
  {
    string tags = "Seccion";
    GameObject parent1 = FindParentWithTag(newitem, tags);
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