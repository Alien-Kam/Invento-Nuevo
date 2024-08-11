using System.Collections;
using System.Collections.Generic;
namespace Logica
{
    public enum Faccion
    {
        PrimeraFaccion,
        SegundaFaccion,
    }
    public class Jugadores
    {

      public string nameplayer;
      public Faccion faccionplayer;
      public Jugadores(string nombre, Faccion faccion)
      {
        nameplayer = nombre;
        faccionplayer = faccion;
      }


    }
}