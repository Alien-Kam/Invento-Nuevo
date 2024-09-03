using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logica
{
   public enum TipoPosicion
   {
      CuerpoaCuerpo = 0b_001,
      LargaDistancia = 0b_010,
      Asedio = 0b_100,
      Aumento,
      Clima,
   }
   class Tablero
   {
      // Porque las señuelos no se pueden hacer monstercards
      public MonsterCard[,] tablero;
      public bool[,] tablerobool;
      public Aumento[] aumentos;
      public bool[] aumentosbool;
      public Clima[] climas;
      public bool[] climasbool;
      public List<BaseCard> cementerio;

      public Tablero()
      {
         tablero = new MonsterCard[6, 5];
         tablerobool = new bool[6, 5];
         climas = new Clima[6];
         aumentos = new Aumento[6];
         aumentosbool = new bool[6];
         climasbool = new bool[6];
         cementerio = new List<BaseCard>();
      }
   }

   public class FuncionesTablero
   {
      Tablero tablerojuego = new Tablero();
      public void PosicionarCarta(BaseCard card, int fila, int columna)
      {
      
      }

      public bool IsValido(uint clas, Faccion tab, uint clascard, Faccion card)
      {
         bool val = (clas & clascard) == clas;
         if ((tab == card) && val)
         {
            return true;
         }

         return false;
      }

      public bool IsValidoEspecial(TipoPosicion tab, TipoCarta card, Faccion factab, Faccion faccard)
      {
         if((factab == faccard) && ((tab == TipoPosicion.Aumento && card == TipoCarta.Aumento) || (tab == TipoPosicion.Clima && card == TipoCarta.Clima)))
         {
            return true;
         }
         return false;
      }
   }
}
