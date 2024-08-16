using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;
using Logica;
using Newtonsoft.Json;

public class ContinuarPanelFacciones : MonoBehaviour 
{
   public TMP_InputField intputplayer1;
   public TMP_InputField intputplayer2;
   public TMP_Dropdown player1drop;
   public TMP_Dropdown  player2drop;  
   string nameplayer;
   string nameplayer1;
   int faccion1;
   int faccion2;
   // Este metodo coge los nombres y la facciones de los jugadores y los instancia
   public void Continuar()
   {
      ComponentesPlayers scriptplayer1 = intputplayer1.GetComponent<ComponentesPlayers>();

      ComponentesPlayers scriptplayer2 = intputplayer2.GetComponent<ComponentesPlayers>();

      nameplayer = scriptplayer1.nameply;
     
      nameplayer1 = scriptplayer2.nameply;
     
     Facciones facciones1 = player1drop.GetComponent<Facciones>();

     Facciones facciones2 = player2drop.GetComponent<Facciones>();

     faccion1 = facciones1.faccion;
     faccion2 = facciones2.faccion;
      
     Player player1 = new Player(nameplayer, faccion1);
     Player player2 = new Player(nameplayer, faccion2);
   
     SceneManager.LoadScene(2);
   }
}
