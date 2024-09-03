using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logica;
using UnityEngine.EventSystems;

public enum TipoCarta
{
    Heroe,
    Normales,
    Clima,
    Aumento,
    Señuelo,
}
public class Cartas : MonoBehaviour
{

    public string nombre;
    public string habilidad;
    public int ataque;
    public uint clasificacion;
    public Faccion faccion;
    public TipoCarta tipoCarta;

    public void Awake()
    {
        CrearCarta();
    }
    public BaseCard CrearCarta()
    {

        switch (tipoCarta)
        {
            case TipoCarta.Heroe:
                Heroe card = new Heroe(nombre, habilidad, faccion, ataque, clasificacion);
                return card;

            case TipoCarta.Normales:
                Normales cardh = new Normales(nombre, habilidad, faccion, ataque, clasificacion);
                return cardh;

            case TipoCarta.Clima:
                Clima cardc = new Clima(nombre, habilidad, faccion);
                return cardc;

            case TipoCarta.Aumento:
                Aumento carda = new Aumento(nombre, habilidad, faccion);
                return carda;


            case TipoCarta.Señuelo:
                Senuelo cards = new Senuelo(nombre, habilidad, faccion);
                return cards;

            default:
                throw new System.Exception("Tu cart no es de ningun tipo vuelve a intentarlo");
        }
    }
}

