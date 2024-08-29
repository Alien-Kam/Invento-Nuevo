using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Logica;
using Scene = UnityEngine.SceneManagement.Scene;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class ContinuarPanelFacciones : MonoBehaviour
{
   public TMP_InputField intputplayer1;
   public TMP_InputField intputplayer2;
   public TMP_Dropdown player1drop;
   public TMP_Dropdown player2drop;
   public GameObject deck1;
   public GameObject deck2;
   public GameObject player1;
   public GameObject player2;
   
   // Este metodo prepara las cosas iniciales para el juego osea crea los directorios de las cartas instasncia los jugadores y coge las facciones
   public void Continuar()
   {
      string nameplayer1 = intputplayer1.textComponent.text;
      string nameplayer2 = intputplayer2.textComponent.text;

      string faccionName1 = player1drop.captionText.text;
      string faccionName2 = player1drop.captionText.text;

      player1.GetComponent<PlayersVisual>().nameplayer = nameplayer1;
      player1.GetComponent<PlayersVisual>().faccionplayer = faccionName1;
      player2.GetComponent<PlayersVisual>().nameplayer =nameplayer2;
      player2.GetComponent<PlayersVisual>().faccionplayer= faccionName2;
      

      DirectoryInfo directory1 = Directory.CreateDirectory(Path.GetDirectoryName($"Assets/Prefabs Cartas/{faccionName1}"));
      DirectoryInfo directory2 = Directory.CreateDirectory(Path.GetDirectoryName($"Assets/Prefabs Cartas/{faccionName2}"));

      List<GameObject> cartsFaccion1 = LoadFaccionCarts(directory1).ToList();
      List<GameObject> cartsFaccion2 = LoadFaccionCarts(directory2).ToList();
      
      deck1.GetComponent<Decks>().deck = cartsFaccion1;
      deck2.GetComponent<Decks>().deck = cartsFaccion2;

      Scene newScene = SceneManager.GetSceneAt(2);
      SceneManager.MoveGameObjectToScene(player1, newScene);
      SceneManager.MoveGameObjectToScene(player2, newScene);
      SceneManager.MoveGameObjectToScene(deck1, newScene);
      SceneManager.MoveGameObjectToScene(deck2, newScene);

      // Es el indice de la escena en Build Settings
      SceneManager.LoadScene(newScene.buildIndex);
   }

   // ES EL TC
   //Este carga las cartas del directorio 
   public IEnumerable<GameObject> LoadFaccionCarts(DirectoryInfo directoryActual)
   {
      foreach (FileInfo file in directoryActual.GetFiles())
      {
         if (!file.Extension.Equals("prefab")) continue;

         string filePath = file.FullName;
         GameObject cart = PrefabUtility.LoadPrefabContents(filePath);
         yield return cart;
      }
      foreach (DirectoryInfo directory in directoryActual.GetDirectories())
      {
         foreach (GameObject cart in LoadFaccionCarts(directory))
         {
            yield return cart;
         }
      }
   }
}
