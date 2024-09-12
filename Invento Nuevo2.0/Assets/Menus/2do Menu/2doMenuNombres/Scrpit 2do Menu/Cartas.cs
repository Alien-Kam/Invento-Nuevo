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
    Lider,
}
public class Cartas : MonoBehaviour
{

    public string nombre;
    public string habilidad;
    public int ataque;
    public uint clasificacion;
    public Faccion faccion;
    public TipoCarta tipoCarta;
    public BaseCard baseCard;

    public BaseCard CrearCarta()
    {
        if (baseCard != null) return baseCard;
        switch (tipoCarta)
        {
            case TipoCarta.Heroe:
                baseCard = new Heroe(nombre, habilidad, faccion, ataque, clasificacion);
                break;
            case TipoCarta.Normales:
                baseCard = new Normales(nombre, habilidad, faccion, ataque, clasificacion);
                break;
            case TipoCarta.Clima:
                baseCard = new Clima(nombre, habilidad, faccion);
                break;
            case TipoCarta.Aumento:
                baseCard = new Aumento(nombre, habilidad, faccion);
                break;
            case TipoCarta.Señuelo:
                baseCard = new Senuelo(nombre, habilidad, faccion);
                break;
            case TipoCarta.Lider:
                baseCard = new Lider(nombre, habilidad, faccion);
                break;
            default:
                throw new System.Exception("Tu carta no es de ningun tipo vuelve a intentarlo");
        }
        return baseCard;
    }
}

