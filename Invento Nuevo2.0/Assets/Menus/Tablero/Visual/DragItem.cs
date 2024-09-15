using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logica;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
public class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
  private GameObject _dragParent;
  private bool _isDropped;
  private bool _enableDrop;
  private Vector3 _startingPosition;
  private Transform _startingParent;
  public bool move;
  public BaseCard carta;
  private CanvasGroup canvas;
  private MostrarCartas displayManager;
  Image cardSprite;
  private bool _inGame;

  public void Start()
  {
    move = false;
    _isDropped = false;
    _enableDrop = false;
    _dragParent = GameObject.Find("DragParent");
    canvas = _dragParent.GetComponent<CanvasGroup>();
    displayManager = FindObjectOfType<MostrarCartas>();
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    if (move) return;
    _startingPosition = transform.position;
    _startingParent = transform.parent;
    transform.SetParent(_dragParent.transform);
    canvas.blocksRaycasts = false;
  }
  public void OnDrag(PointerEventData eventData)
  {
    if (move) return;
    transform.position = eventData.position;
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    canvas.blocksRaycasts = true;
    if (move) return;
    if (_isDropped)
    {
      move = true;
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

  public void OnPointerEnter(PointerEventData eventData)
  {
    //Esto es para el 2do proyecto

    /*Transform childTransform = transform.GetChild(0);
    cardSprite = childTransform.GetComponent<Image>();
    displayManager.ShowCardImage(cardSprite.sprite);*/

    cardSprite = GetComponent<Image>();
    displayManager.ShowCardImage(cardSprite.sprite);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    displayManager.HideCardImage();
  }

  public void SetInGame(bool inGame)
  {
    _inGame = inGame;
  }
}