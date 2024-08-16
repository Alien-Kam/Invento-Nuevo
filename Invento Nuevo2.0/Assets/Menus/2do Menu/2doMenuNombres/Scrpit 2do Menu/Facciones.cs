using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facciones: ComponentesPlayers
{
   public List<GameObject> PrimFacc;
   public List<GameObject> SegunFacc;
   public List<GameObject> TercerFaccio;
   public List<List<GameObject>> FaccionesCartas;
   public int faccion;

   public void EleccionFaccion(int indice)
   {
        faccion = indice;
        Debug.Log("" + faccion);
   }

}
