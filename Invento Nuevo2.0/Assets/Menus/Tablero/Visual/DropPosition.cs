using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Logica;
using JetBrains.Annotations;
using Unity.Mathematics;


public class DropPosition : MonoBehaviour, IDropHandler
{
    public Vector2 position;
    public Faccion faccion;
    public TipoPosicion clasificacion;
    private GameObject _item;
    private FuncionesTablero tablero;
    private Turnos turnos;

   public void Start()
   {
     tablero = new FuncionesTablero();
     turnos = new Turnos();
   }
    // Si entra al metodo y este metodo hace la funcion de Drop
    public void OnDrop(PointerEventData eventData)
    {
        GameObject newitem = eventData.pointerDrag;
        
        if(newitem.GetComponent<DragItem>().move)
        {
          return;
        }
        Cartas card = newitem.GetComponent<Cartas>();
        BaseCard cardlog = card.CrearCarta();
        Debug.Log(card.nombre);
        
        if (_item && newitem.GetComponent<Cartas>().tipoCarta != TipoCarta.Señuelo)
        {
            newitem.GetComponent<DragItem>().ReturnToStartingPosition();
            return;
        }
        if (card.tipoCarta == TipoCarta.Aumento || card.tipoCarta == TipoCarta.Clima)
        {
            bool validespecial = tablero.IsValidoEspecial(clasificacion, card.tipoCarta, faccion, card.faccion);
            if (!validespecial || _item)
            {
                _item.GetComponent<DragItem>().ReturnToStartingPosition();
            }
        }
        else 
        {
            bool valido = tablero.IsValido((uint)clasificacion, faccion, card.clasificacion, card.faccion);

            if (!valido || _item)
            {
                _item.GetComponent<DragItem>().ReturnToStartingPosition();
            }
        }
        _item = newitem;
        _item.transform.position = transform.position;
        DragItem compitem = _item.GetComponent<DragItem>();
        _item.transform.SetParent(transform);
        compitem.IsDropped(true);
        compitem.move = true;
        tablero.PosicionarCarta(cardlog, (int)position.x, (int)position.y);
        turnos.TerminaraTurno();
    }
}
