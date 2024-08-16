using System.Collections;
using System.Collections.Generic;


namespace Logica
{
    public class Player
    {
        public string nombreplayer;
        public int faccion;
        public Player(string name, int faccion)
        {
            nombreplayer = name;
            this.faccion = faccion;
        }
    }
}
