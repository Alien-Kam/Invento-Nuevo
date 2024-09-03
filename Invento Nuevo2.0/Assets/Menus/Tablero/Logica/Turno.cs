using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logica
{
  public class Turno
  {
    public List<Player> players;
    int current;
    int jugadorespas;
    public bool pasarTurno;
    public bool cartamovid;
    public bool habLider;
    private List<bool> pasados;
    public Turno()
    {
      players = new List<Player>();
      current = 0;
      jugadorespas = 0;
      pasarTurno = false;
      cartamovid = false;
      habLider = false;
      pasados = new List<bool>() { false, false };

    }
    public Player BegingTurn(List<Player> player)
    {
      Debug.Log("Entre");
      players = player;

      if (pasados[current])
      {
        EndTurn();
      }
      return players[current];
    }

    public void EndTurn()
    {
      Debug.Log(players.Count);

      if (cartamovid)
      {
        cartamovid = false;
      }
      if (pasarTurno)
      {
        pasados[current] = true;
        jugadorespas++;
      }
      current = (current + 1) % players.Count;
      Debug.Log("vvvvvrvr");
    }
  }
}
