using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Logica;
using UnityEngine;

public class PruebaRonda : MonoBehaviour
{
  // Start is called before the first frame update
  public void Start()
  {

  }

  // Update is called once per frame
  public void Update()
  {

  }

  Rondas ronda = new Rondas();
  
  //Este metodo distribuye las cartas y las llama a instanciar una por una elimina las cartas del deck y las pone en la mano 
  public List<GameObject> DistribuirCard(List<GameObject> deck, GameObject posiciones, List<bool> posicionescartas) // No hay cartas repetidads tengo que arreglarlo
  {
    List<BaseCard> deckplayer = new List<BaseCard>();
    List<BaseCard> hand = new List<BaseCard>();
    List<GameObject> handvisual = new List<GameObject>();

    for (int i = 0; i < deck.Count; i++)  // Aqui esta el problema 
    {
      deckplayer.Add(deck[i].GetComponent<Cartas>().CrearCarta());
    }

    hand = ronda.DistribucionCartas(deckplayer, posicionescartas);

    for (int i = 0; i < hand.Count; i++)
    {
      handvisual.Add(deck[i]);
    }
    for (int i = 0; i < hand.Count; i++)
    {
      deck.Remove(handvisual[i]);
    }
    for (int i = 0; i < handvisual.Count; i++)
    {
      InstanciarCartas(handvisual[i], posicionescartas, posiciones);
    }
    return handvisual;


    /* Cada posicion de la mano va a tener un hijo que seria la instancia de la carta */
    void InstanciarCartas(GameObject card, List<bool> posicionescartas, GameObject posiciones)
    {

      for (int i = 0; i < posiciones.transform.childCount; i++)
      {
        if (posiciones.transform.GetChild(i).transform.childCount != 0)
        {
          if (posiciones.transform.GetChild(i).transform.GetChild(0).Equals(card.GetComponent<Cartas>().nombre))
          {
            continue;
          }
        }

        if (!posicionescartas[i])
        {
          Transform pos = posiciones.transform.GetChild(i);
          GameObject newInstance = Instantiate(card, pos.position, Quaternion.identity);
          newInstance.transform.SetParent(posiciones.transform.GetChild(i));
          newInstance.AddComponent<DragandDrop>();
          newInstance.tag = "Carta";
          Debug.Log(newInstance.name);
          posicionescartas[i] = true;
          break;
        }
      }
    }
  }
}