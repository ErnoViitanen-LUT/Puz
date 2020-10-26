using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
   public Animator animator;
   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
      {
         SceneManager.LoadScene("MainMenu");
      }
   }
}
