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
  public bool move;

  public void Start()
  {
    move = false;
    _dragParent = GameObject.Find("DragParent");
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
      if(move) return;
    _startingPosition = transform.position;
    _startingParent = transform.parent;
    transform.SetParent(_dragParent.transform);
  } 

  public void OnDrag(PointerEventData eventData)
  {
    if(move) return;
    transform.position = eventData.position;
  }

  public void OnEndDrag(PointerEventData eventData)
  {
      if(move) return;
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
}
