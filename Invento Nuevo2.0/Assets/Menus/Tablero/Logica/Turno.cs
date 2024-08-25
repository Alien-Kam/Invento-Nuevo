using System.Collections;
using System.Collections.Generic;
using Logica;
using UnityEngine;

public class Turno
{
  public List<Player> players = new List<Player>();
  int current = 0;
  int cantpas = 0;
  public  bool pasarTurno = false;
  public bool cartamovid = false;
  public bool habLider = false;
  private List<bool> pasados = new List<bool>();
  public Player StartTurn()
  {
    if(pasados[current])
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
    if(pasarTurno )
    {
      pasados[current] = true;
      cantpas ++;
    }
    current = (current + 1) % players.Count;
  }
}
