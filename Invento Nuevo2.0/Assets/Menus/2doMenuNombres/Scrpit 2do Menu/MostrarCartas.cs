using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MostrarCartas : MonoBehaviour
{
    public GameObject primeraFaccion;
    public GameObject segundaFaccion;
    public GameObject cartascreadas;
    void Start()
    {
        primeraFaccion.SetActive(false);
        segundaFaccion.SetActive(false) ;
        cartascreadas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PrimeraFacion()
    {
       primeraFaccion.SetActive(true);
       segundaFaccion.SetActive(false);
       cartascreadas.SetActive(false);
    }
    public void SegundaFaccion()
    {
        primeraFaccion.SetActive(false);
       segundaFaccion.SetActive(true);
       cartascreadas.SetActive(false);
    }
    public void Cartascreadas()
    {
        primeraFaccion.SetActive(false);
       segundaFaccion.SetActive(false);
       cartascreadas.SetActive(true);
    }
    

}
