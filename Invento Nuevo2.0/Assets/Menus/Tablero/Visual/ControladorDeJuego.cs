using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeJuego : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject deck1 = GameObject.Find("Deck 1");
        GameObject deck2 = GameObject.Find("Deck 2");
        GameObject seccion1 = GameObject.Find("Seccion Jugador 1");
        GameObject seccion2 = GameObject.Find("Seccion Jugador 2");
        deck1.transform.SetParent(seccion1.transform, false);
        deck2.transform.SetParent(seccion2.transform, false);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
