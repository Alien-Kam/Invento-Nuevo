using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logica;

public class Cartas : MonoBehaviour
{

    public enum TipoCarta
    {
        Heroe,
        Normales,
        Clima,
        Aumento,
        Señuelo,
    }
    public string nombre;
    public string habilidad;
    public int ataque;
    public uint clasificacion;
    public Faccion faccion;
    public TipoCarta tipoCarta;

    void Start()
    {
        CrearCarta();
    }

    BaseCard CrearCarta()
    {

        switch (tipoCarta)
        {
            case TipoCarta.Heroe:
                Heroe card = new Heroe(nombre, habilidad, faccion, ataque, clasificacion);
                card.ToString();
                return card;

            case TipoCarta.Normales:
                Normales cardh = new Normales(nombre, habilidad, faccion, ataque, clasificacion);
                return cardh;

            case TipoCarta.Clima:
                Clima cardc = new Clima(nombre, habilidad, faccion);
                break;

            case TipoCarta.Aumento:
                Aumento carda = new Aumento(nombre, habilidad, faccion);
                break;

            case TipoCarta.Señuelo:
                Senuelo cards = new Senuelo(nombre, habilidad, faccion);
        break;
        }
        return null;
    }
}

