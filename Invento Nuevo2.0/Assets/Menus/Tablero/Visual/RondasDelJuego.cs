using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Logica;
using Scene = UnityEngine.SceneManagement.Scene;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/*public class RondasDelJuego : MonoBehaviour
{
    List<GameObject> mazo;
    List<BaseCard> handlog;
    List<BaseCard> handlog1;
    Rondas rondas = new Rondas();
    GameObject[] posicionmano;

    // Start is called before the first frame update
    public void Start()
    {
      posicionmano = GameObject.FindGameObjectsWithTag("Posiciones Carta");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InicioRon(List<GameObject> deck1, List<GameObject> deck2, Transform canvasTransform, List<GameObject> hand, List<GameObject> hand2)
    {
      for(int i = 0; i < deck1.Count ; i++)
      {
        handlog.Add(deck1[i].GetComponent<Cartas>().CrearCarta());
        handlog1.Add(deck2[i].GetComponent<Cartas>().CrearCarta());
      }
      handlog = rondas.InicioRonda(handlog);
      handlog1 = rondas.InicioRonda(handlog1);

      for(int j = 0; j < handlog.Count; j++)
      {
        for(int k = 0; k < deck1.Count; k++)
        {
            if(handlog[j].namecard == deck1[k].GetComponent<Cartas>().nombre)
            {
                hand.Add(deck1[k]);
                deck1.Remove(deck1[k]);
                InstasnciarCartas(mazo, 0, canvasTransform);              
            }
            if(handlog1[j].namecard == deck2[k].GetComponent<Cartas>().nombre)
            {
                hand2.Add(deck2[k]);
                deck2.Remove(deck2[k]);
                InstasnciarCartas(mazo, 1, canvasTransform);
            }
        }
      }
    }

    public void InstasnciarCartas(List<GameObject> hand, int index, Transform canvasTransform)
    {
      for(int i = 0; i < hand.Count; i ++)
      {
        Transform pos = posicionmano[i].transform.GetChild(i); 
        GameObject newInstance = Instantiate(hand[i], pos.position, Quaternion.identity, canvasTransform);

        newInstance.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        newInstance.AddComponent<CanvasGroup>();
      }
    }
}*/