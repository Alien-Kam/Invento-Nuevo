using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using UnityEngine.SceneManagement;

public class ComponentesPlayers : MonoBehaviour 
{
  public string nameply;

   public void NombresPlayer(string nombre)
   {
    nameply = nombre;  
    Debug.Log("" + nameply);
   }
}
