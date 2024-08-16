using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DistribuirCartas : MonoBehaviour
{
    public List<Cartas> hand;
    public Transform canvasTransform;
   public void Deck(List<GameObject> mazo)
   {
    for(int i = 0; i < 10; i++)
       {
         GameObject card = mazo[Random.Range(0, mazo.Count )];
         hand.Add(card.GetComponent<Cartas>());
         mazo.Remove(card);
       } 

       Transform posiciones = transform.Find("PositionCards");
      InstanciarCartas(hand, posiciones);
   }
   void InstanciarCartas(List<Cartas> hand, Transform posiciones)
   {
     for(int i = 0; i < hand.Count ; i++)
     {
        Transform posicion = posiciones.GetChild(i);

        GameObject nuevaInstancia = Instantiate(hand[i].gameObject, posicion.position, Quaternion.identity, canvasTransform);
        nuevaInstancia.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        nuevaInstancia.AddComponent<CanvasGroup>();
     }
   }
}
