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
   void Start()
   {

      level.text = LevelManager.Instance.levelName;
      if (LevelManager.Instance.nextLevel == "MainMenu")
      {
         mode.text = "";
         Invoke("LoadLevel", 5f);
      }
      else
      {
         if (LevelManager.Instance.easyMode)
            mode.text = "Normal";
         else mode.text = "Hard";

         Invoke("LoadLevel", 2f);
      }
   }

   void LoadLevel()
   {
      SceneManager.LoadScene(LevelManager.Instance.nextLevel);
   }
}
