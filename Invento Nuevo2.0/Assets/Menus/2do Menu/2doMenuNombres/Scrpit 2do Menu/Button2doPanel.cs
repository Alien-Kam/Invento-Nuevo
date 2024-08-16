using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Button2doPanel : MonoBehaviour
{
    public GameObject panelname;
    public GameObject panelfaccion;
    public void Continuar()
    {
        panelname.SetActive(false);

        panelfaccion.SetActive(true);
    }
    public void Atras()
    {
        panelname.SetActive(true);

        panelfaccion.SetActive(false);
    }
}
