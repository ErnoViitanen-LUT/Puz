using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
   public MenuController menuController;
   public Animator animator;
   public int thisIndex;
   string url = "https://github.com/ErnoViitanen-LUT/Puz";
   bool submitted = false;

   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      if (menuController.index == thisIndex)
      {
         animator.SetBool("selected", true);

         if (Input.GetAxisRaw("Submit") == 1)
         {
            animator.SetBool("pressed", true);
            if (!submitted)
            {
               submitted = true;
               //Invoke("ButtonPress", 0.25f);
            }
         }
         else if (animator.GetBool("pressed"))
         {
            animator.SetBool("pressed", false);
         }
      }
      else
      {
         animator.SetBool("selected", false);
      }

      if (submitted && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
      {
         submitted = false;
         ButtonPress();
      }

   }
   void ButtonPress()
   {
      Debug.Log("pressed " + thisIndex + " " + gameObject.name);

      GameObject pause = GameObject.FindGameObjectWithTag("Pause");
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      switch (gameObject.name)
      {
         case "NewGame":
            SceneManager.LoadScene(1);
            break;
         case "Options":
            break;
         case "Quit":
            Time.timeScale = 1f;
            Application.OpenURL(url);
            break;
         case "Continue":
            Time.timeScale = 1f;
            Destroy(pause);
            break;
         case "MainMenu":
            Time.timeScale = 1f;
            Destroy(pause);
            Destroy(player);
            SceneManager.LoadScene(0);
            break;
      }
   }
}
