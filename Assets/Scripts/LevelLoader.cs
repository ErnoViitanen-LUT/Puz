using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
   public Text level;
   public Text mode;
   // Start is called before the first frame update

   string nextLevel = "Level1";
   void Start()
   {

      level.text = LevelManager.Instance.levelName();
      nextLevel = LevelManager.Instance.nextLevel;
      if (nextLevel == "MainMenu")
      {
         mode.text = "";
         GameObject player = GameObject.FindGameObjectWithTag("Player");
         if (player) Destroy(player.GetComponent<PlayerController>().gameObject);

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

         Invoke("LoadLevel", 2f);
      }
   }

   void LoadLevel()
   {
      SceneManager.LoadScene(nextLevel);
   }
}
