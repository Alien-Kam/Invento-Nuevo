using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Cil;
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
    public bool[] pasados;
    public Turno(List<Player> player)
    {
      players = player;
      current = 0;
      jugadorespas = 0;
      pasarTurno = false;
      cartamovid = false;
      habLider = false;
      pasados = new bool[player.Count]; // Antes del implica es el parametro y luego del implica el codigo
    }

    public Player BegingTurn(bool[] pasados)
    {
      if (pasados[current])
      {
        EndTurn();
      }
      return players[current];
    }

    public void EndTurn()
    {
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
    }
  }
}
