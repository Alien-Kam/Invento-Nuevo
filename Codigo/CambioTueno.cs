
namespace Invento
{
    public class Program
    {
        public void Main(string[] args)
        {
            Players player1 = new Players("Kamila", Faccion.faccion1);
            Players players2 = new Players("Raiza", Faccion.faccion2);

            Cartas carta1  = new Cartas("Luffy", 10, Faccion.faccion1);
            Cartas carta2 =  new Cartas("Zoro", 10, Faccion.faccion1);
            Cartas carta3 = new Cartas("Eren", 5, Faccion.faccion2);
            Cartas carta4 = new Cartas("Mikasa", 10, Faccion.faccion2); 
        }
    }
}