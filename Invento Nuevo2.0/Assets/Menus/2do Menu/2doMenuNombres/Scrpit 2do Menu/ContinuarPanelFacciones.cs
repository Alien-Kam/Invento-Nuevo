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
   // Este metodo coge los nombres y la facciones de los jugadores y los instancia
   public void Continuar()
   {
      string nameplayer1 = intputplayer1.textComponent.text;
      string nameplayer2 = intputplayer2.textComponent.text;

      string faccionName1 = player1drop.captionText.text;
      string faccionName2 = player1drop.captionText.text;

      Player player1 = new Player(nameplayer1, faccionName1);
      Player player2 = new Player(nameplayer2, faccionName2);
      

      DirectoryInfo directory1 = Directory.CreateDirectory(Path.GetDirectoryName($"storage/{faccionName1}"));
      DirectoryInfo directory2 = Directory.CreateDirectory(Path.GetDirectoryName($"storage/{faccionName2}"));

      List<GameObject> cartsFaccion1 = LoadFaccionCarts(directory1).ToList();
      List<GameObject> cartsFaccion2 = LoadFaccionCarts(directory2).ToList();

      deck1.GetComponent<DistribuirCartas>().Deck(cartsFaccion1);
      deck2.GetComponent<DistribuirCartas>().Deck(cartsFaccion2);

      Scene newScene = SceneManager.GetSceneAt(2);
      SceneManager.MoveGameObjectToScene(deck1, newScene);
      SceneManager.MoveGameObjectToScene(deck2, newScene);
      SceneManager.LoadScene(newScene.name);
   }

   // ES EL TC
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
