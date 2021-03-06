﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

   private static LevelManager _instance;

   public static LevelManager Instance { get { return _instance; } }
   public int level;
   public string nextLevel;
   public bool levelComplete = false;
   public int gameCompleted = 0;
   public bool easyMode = true;
   public bool tutorialCompleted = false;

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
         easyMode = true;
         levels.Add("MainMenu");
         for (int i = 1; i < 7 + 1; i++)
         {
            levels.Add("Level" + i);
         }
         SetCurrentLevel();
         DontDestroyOnLoad(transform.gameObject);

      }

   }

   // Update is called once per frame
   void Update()
   {

   }

   public void StartGame()
   {
      level = 1;
      nextLevel = levels[level];
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
      nextLevel = levels[level];

      if (gameCompleted > 2)
      {
         level = 0;
         nextLevel = "Credits";
      }

      SceneManager.LoadScene("LevelLoader");
   }
   public void GameOver()
   {
      level = -1;
      nextLevel = levels[0];
      SceneManager.LoadScene("LevelLoader");
   }   
   public void Credits()
   {
      SceneManager.LoadScene("Credits");
   }
   public void MainMenu()
   {
      SceneManager.LoadScene("MainMenu");
   }

   void SetCurrentLevel()
   {
      level = SceneManager.GetActiveScene().buildIndex - 3;
      if (level < 0) level = 1;

   }

   public string levelName()
   {
      if (level < 0)
      {
         return "game over";
      }
      else if (level == 0)
      {
         return "the end";
      }
      else return "level " + level;
   }
}
