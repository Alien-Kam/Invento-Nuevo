using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logica
{
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
        tablero = new MonsterCard[6,5];
        tablerobool = new bool[6,5];
        climas = new Clima[6];
        aumentos = new Aumento[6];
        aumentosbool = new bool[6];
        climasbool = new bool[6];
        cementerio = new List<BaseCard>();
    }
   }
}
