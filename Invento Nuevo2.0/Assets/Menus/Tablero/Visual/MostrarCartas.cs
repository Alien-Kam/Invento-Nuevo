using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarCartas : MonoBehaviour
{
    public Image cardImage; // Asigna aquí el componente Image del panel

    public void ShowCardImage(Sprite cardSprite)
    {
        cardImage.sprite = cardSprite;
        Color color = cardImage.color;
        color.a = 1;
        cardImage.color = color;
        cardImage.gameObject.SetActive(true);
    }

    public void HideCardImage()
    {
        cardImage.gameObject.SetActive(false);
    }
}


