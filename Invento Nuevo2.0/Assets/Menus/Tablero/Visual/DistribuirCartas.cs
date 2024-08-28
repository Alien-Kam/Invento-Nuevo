using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Logica;
using Unity.VisualScripting;

public class DistribuirCartas : MonoBehaviour
{
   Rondas rondas = new Rondas();
   Transform posiciones;
   GameObject mano;
   public List<BaseCard> hand;
   public void Start()
   {
      mano = GameObject.Find("Cartas");
      posiciones = GameObject.Find("Posiciones de las cartas").transform;
   }
   public void Deck(List<GameObject> mazo)
   {
      List<BaseCard> deck = new List<BaseCard>();
      for (int i = 0; i < mazo.Count; i++)
      {
         deck.Add(mazo[i].GetComponent<Cartas>().CrearCarta());
      }
      hand = rondas.InicioRonda(deck);
   }
   public void InstanciaCarta()
   {
     for(int i = 0; i < hand.Count; i++)
     {
       GameObject instancia = GameObject.Find($"{hand[i].namecard}");
       Transform pos = posiciones.GetChild(i);
       GameObject nuevaInstacia = Instantiate(instancia, pos.position, Quaternion.identity, mano.transform );
       nuevaInstacia.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
     }   
   }
}
