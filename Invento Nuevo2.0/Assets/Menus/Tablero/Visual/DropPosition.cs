using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Logica;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;


public class DropPosition : MonoBehaviour, IDropHandler
{
    public Vector2 position;
    public Faccion faccion;
    public TipoPosicion clasificacion;
    private GameObject _item;
    private FuncionesTablero tablero;
    private Turnos turnos;
    Cartas card;
    BaseCard cardlog;
    GameObject newitem;
    GameManager control;
    bool okcard;

    public void Start()
    {
        turnos = GameObject.Find("Controlador de Turno").GetComponent<Turnos>();
        control = GameObject.Find("Controlador de juego ").GetComponent<GameManager>();
        tablero = control.funcionesTablero;
    }
    // Si entra al metodo y este metodo hace la funcion de Drop
    public void OnDrop(PointerEventData eventData)
    {
        GameObject player = control.players[turnos.current];
        newitem = eventData.pointerDrag;
        okcard = newitem.GetComponent<DragItem>().ok;

        if (newitem.GetComponent<DragItem>().move || !okcard)
        {
            return;
        }
        card = newitem.GetComponent<Cartas>();
        cardlog = card.CrearCarta();

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
        tablero.PonerCartas(cardlog, (int)position.x, (int)position.y, turnos.player);
        turnos.termino = true;
    }

}
