using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
   public Text level;
   public Text mode;
   public Text hints;
   // Start is called before the first frame update

   string nextLevel = "Level1";
   void Start()
   {

      level.text = LevelManager.Instance.levelName();
      nextLevel = LevelManager.Instance.nextLevel;

      if(nextLevel == "Level1" && !LevelManager.Instance.tutorialCompleted){
         hints.text = "Use the W A S D or the arrow keys to move, Shift to run and Space to jump.\n\nPress space to start the game.";
      }
      else
         hints.text = "";

      if (nextLevel == "MainMenu" || nextLevel == "Credits")
      {
         mode.text = "";
         GameObject player = GameObject.FindGameObjectWithTag("Player");
         if (player) Destroy(player.GetComponent<PlayerController>().gameObject);
         if(nextLevel != "Credits")
            Destroy(LevelManager.Instance.gameObject);
         Invoke("LoadLevel", 5f);
      }
      else
      {
         if (LevelManager.Instance.gameCompleted == 0)
            mode.text = "easy";
         else if (LevelManager.Instance.gameCompleted == 1)
            mode.text = "hard";
         else
            mode.text = "very hard";

         if(LevelManager.Instance.tutorialCompleted)
            Invoke("LoadLevel", 2f);
      }
   }

   void LoadLevel()
   {
      SceneManager.LoadScene(nextLevel);
   }

   void Update(){
      if(Input.GetKey(KeyCode.Space)){
         LevelManager.Instance.tutorialCompleted = true;
         LoadLevel();
      }
   }
}
