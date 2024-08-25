using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logica;

public class PlayersVisual : MonoBehaviour
{
  private string nameplayer;
  private string faccionplayer;
  private int puntos;
  public void CreandoJugadores(string nombre, string faccion, int puntos)
  {
    nameplayer = nombre;
    faccionplayer = faccion;
    this.puntos = puntos;

    Player player1 = new Player(nombre, faccion);
    Debug.Log(player1.nombreplayer);
  }

}
