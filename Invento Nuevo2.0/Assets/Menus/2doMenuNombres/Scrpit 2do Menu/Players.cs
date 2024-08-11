using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class Players : MonoBehaviour
{
    private string nameplayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadStringInput(string nombre)
    {
        nameplayer = nombre;
        Debug.Log(nameplayer);
    }
}
