using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

   private static LevelManager _instance;

   public static LevelManager Instance { get { return _instance; } }
   public int level = 1;
   public string levelName;
   public string nextLevel;
   public bool levelComplete = false;
   public int gameCompleted = 0;
   public bool easyMode = true;

   public bool betweenLevels = false;
   public List<string> levels;
   // Start is called before the first frame update
   void Start()
   {
      if (_instance != null && _instance != this)
      {
         Destroy(this.gameObject);
      }
      else
      {
         _instance = this;
         levels = new List<string>();
         levels.Add("MainMenu");
         for (int i = 1; i < 7 + 1; i++)
         {
            levels.Add("Level" + i);
         }
         levelName = levels[level];
         DontDestroyOnLoad(transform.gameObject);
         /*
         if (SceneManager.GetActiveScene().name == "LevelLoader")
         {
            Invoke("LoadNextLevel", 2f);
         }*/

      }

   }

   // Update is called once per frame
   void Update()
   {

   }

   public void StartGame()
   {
      level = 1;
      levelName = "Level " + level;
      nextLevel = levels[level];
      Debug.Log("LevelManager load " + levels[level]);
      SceneManager.LoadScene("LevelLoader");
   }
   public void LoadNextLevel()
   {
      betweenLevels = true;
      level++;
      if (level > 7)
      {
         level = 1;
         gameCompleted++;
         easyMode = false;

      }
      levelName = "Level " + level;
      nextLevel = levels[level];
      SceneManager.LoadScene("LevelLoader");
   }
   public void GameOver()
   {
      level = 0;
      levelName = "Game Over";
      nextLevel = levels[level];
      SceneManager.LoadScene("LevelLoader");
   }

   /*   void LoadNextLevel1()
      {
         levelComplete = false;
         int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

         if (SceneManager.sceneCountInBuildSettings < nextScene + 1)
         {
            gameCompleted++;
            gameObject.GetComponent<SimpleCharacterController>().easyMode = false;
            SceneManager.LoadScene(1);
         }
         else SceneManager.LoadScene(nextScene);
      }

      void EndGame()
      {
         Destroy(gameObject);
         //Destroy(AudioManager.Instance);
         SceneManager.LoadScene(0);
      }
   */
}
