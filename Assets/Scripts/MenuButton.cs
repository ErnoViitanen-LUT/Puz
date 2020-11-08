using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
   using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
   public MenuController menuController;
   public Animator animator;
   public int thisIndex;
   string url = "https://github.com/ErnoViitanen-LUT/Puz";
   bool submitted = false;

   bool mousePressed = false;

   // Start is called before the first frame update
   void Start()
   {

   }
   public void OnPointerEnter(PointerEventData eventData)
   {
      menuController.index = thisIndex;      
      AudioManager.Instance.PlayMenuSelected();
      animator.SetBool("selected", true);
      animator.SetBool("pressed", false);
   }
   
   public void OnPointerClick(PointerEventData eventData)
   {     
      mousePressed = true;      
   }

   
   void Update()
   {


      if (menuController.index == thisIndex)
      {
         animator.SetBool("selected", true);

         if (Input.GetButtonDown("Submit") || mousePressed){            
            mousePressed = false;
            animator.SetBool("pressed", true);
            if (!submitted)
            {
               submitted = true;
               AudioManager.Instance.PlayMenuPressed();
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
      GameObject pause = GameObject.FindGameObjectWithTag("Pause");
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      
      switch (gameObject.name)
      {
         case "NewGame":
            //SceneManager.LoadScene(1);
            //SceneManager.LoadScene("LevelLoader");
            LevelManager.Instance.StartGame();
            break;
         case "Options":
            menuController.index = 4;
            menuController.mainmenu.gameObject.SetActive(false);
            menuController.options.gameObject.SetActive(true);
            break;
         case "Return":
            menuController.options.gameObject.SetActive(false);
            menuController.mainmenu.gameObject.SetActive(true);            
            menuController.index = 1;
            break;
         case "Credits":
            LevelManager.Instance.Credits();
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
            LevelManager.Instance.MainMenu();
            //SceneManager.LoadScene(0);
            break;
      }
   }
}
