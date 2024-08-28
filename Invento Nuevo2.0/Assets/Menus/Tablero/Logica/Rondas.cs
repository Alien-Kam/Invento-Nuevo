using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logica;
using System.Linq;

/* Al inicio de cada ronda las cartas son distribuidas
 2 - El jugador puede devolver hasta 2 cartas y robar 2 cartas 
 3 - Una ronda se finaliza cuando ambos jugadores se pasan o se quedan sin cartas
 4 - Se decide quien es el ganador de la ronda segun la cantidad de puntos que tenga ese jugador
 5 - 2 rondas ganadas deciden un ganador del juego 
 6 - En caso de empate se le da un punto de ronda a cada jugador*/
public class Rondas
{

    // Inicio de Ronda
    public List<BaseCard> InicioRonda(List<BaseCard> deck)
    {
        List<BaseCard> mano = new List<BaseCard>();

        for (int i = 0; i < deck.Count; i++)
        {
            BaseCard card = deck[Random.Range(0, deck.Count)];
            mano.Add(card);
            deck.Remove(card);
        }
        return mano;
    }
    public void RobarCarta(List<BaseCard> deck, List<BaseCard> mano)
    {

    }

    // Finalizar rondas 
    int maxpuntos = int.MinValue;
    int index = 0;
    bool empate = false;
    public void FinRonda(List<Player> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].puntos > maxpuntos)
            {
                maxpuntos = players[i].puntos;
                index = i;
            }
        }

        players[index].puntosronda++;

        for (int j = 0; j < players.Count; j++)
        {
            if (players[j].puntos == maxpuntos && j != index)
            {
                players[j].puntosronda++;
                empate = true;
            }
        }
    }
    public Player GanadorRonda(List<Player> players)
    {
        if (empate)
        {
            return null;
        }
        return players[index];
    }

    public void GanadorJuego(List<Player> players)
    {
        int maxrondas = int.MinValue;
      for(int i = 0; i < players.Count; i++)
      {
        if(players[i].puntosronda > maxrondas)
        {
            maxrondas = players[i].puntosronda;
        }
      }
    }
}
