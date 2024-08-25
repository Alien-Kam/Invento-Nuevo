

namespace Invento
{

        public enum Faccion
        {
            faccion1,
            faccion2,
        }
    public class Players
    {
        string nombre;
        int puntos;
        Faccion faccionjugador;
 
      public List<Players> players = new List<Players>();
        public Players(string playername, Faccion player)
        {
            nombre = playername;
            puntos = 0;
            faccionjugador = player;
        }
    }
    public class Cartas
     {
        public string namecard;
        public int ataque;
        public Faccion faccioncard;
        public Cartas(string namecard, int ataq, Faccion card)
        {
            this.namecard = namecard;
            ataque = ataq;
            faccioncard = card;

        }
     }

     public class Tablero
     {
        Cartas[,] tableromonstruos;
        Cartas[] clima;
        Cartas[] aumento;
         
         public Tablero()
         {
           tableromonstruos = new Cartas[6,5];
           clima = new Cartas[6]; 
           aumento = new Cartas[3];
         }
     }
}