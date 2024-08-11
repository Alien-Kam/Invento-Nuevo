using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SegundaFaccion
{
  public class SegundaFaccion : MonoBehaviour
  {


    public enum TipoDeCarta
    {
      Lider,
      Héroe,
      Plata,
      Clima,
      Despeje,
      Aumento,
      Señuelo,
    }

    public enum Clasification
    {
      CuerpoaCuerpo,
      LargaDistancia,
      Asedio,
      Lider,

    }
    public enum Clasification2
    {
      CuerpoaCuerpo,
      LargaDistancia,
      Asedio,
      Lider,
    }
    public enum Clasification3
    {
      CuerpoaCuerpo,
      LargaDistancia,
      Asedio,
      Lider,
    }

    public class Carta1raFaccion //Cree un constructor para poder crear las cartas como objeto 
    {
      public string name { get; }
      string ability { get; }
      int Id { get; }
      public int stroke { get; set;}
      public TipoDeCarta tipoDeCarta { get; }
      public Clasification clasification { get; }
      public Clasification2 clasification2 { get; }
      public Clasification clasification3 { get; }

      // Cartas con 3 tipos de Clasificacion
      // Incluye las cartas sin poder osea las cartas de tipo clima, despeje, señuelo, y alguna otra q se me ocurra 
      public Carta1raFaccion(string nombre, string habilidad, int id, int ataque, TipoDeCarta tipodecarta, Clasification clasificacion, Clasification2 clasificacion2, Clasification3 clasificacion3)
      {
        name = nombre;
        ability = habilidad;
        Id = id;
        stroke = ataque;
        clasification = clasificacion;
        clasification2 = clasification2;
        clasification3 = clasification3;
        tipoDeCarta = tipodecarta;

      }
      //Cartas con 2 tipos de clasificacion 
      public Carta1raFaccion(string nombre, string habilidad, int id, int ataque, TipoDeCarta tipodecarta, Clasification clasificacion, Clasification2 clasificacion2)
      {
        name = nombre;
        ability = habilidad;
        Id = id;
        stroke = ataque;
        clasification = clasificacion;
        clasification2 = clasificacion2;
        tipoDeCarta = tipodecarta;
      }

      //Cartas con 1 tipos de  Clasificacion 
      public Carta1raFaccion(string nombre, string habilidad, int id, int ataque, TipoDeCarta tipodecarta, Clasification clasificacion)
      {
        name = nombre;
        ability = habilidad;
        Id = id;
        stroke = ataque;
        clasification = clasificacion;
        tipoDeCarta = tipodecarta;
      }

    }
  }
}
