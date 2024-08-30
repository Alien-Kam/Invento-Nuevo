using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logica;

public class Decks : MonoBehaviour
{
    public List<GameObject> deck;


    public void MostrarDeck() // Esto es un efecto 
    {

    }

   // Esto es para robar una carta 
   public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carta"))
        {
            Debug.Log("Objeto soltado encima!");
        }
    }
}
