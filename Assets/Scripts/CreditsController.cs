using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
   public Animator animator;
   public GameObject audioManagerPrefab;
   public GameObject LevelManagerPrefab;
   // Start is called before the first frame update
   void Start()
   {

      GameObject manager = GameObject.FindGameObjectWithTag("AudioManager");

      if (!manager)
         manager = Instantiate(audioManagerPrefab);

      if (LevelManagerPrefab)
         Instantiate(LevelManagerPrefab);
   }

   // Update is called once per frame
   void Update()
   {
      if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && LevelManager.Instance.gameCompleted < 3)
      {
         animator.speed++;
      }

      if (Input.GetKeyDown(KeyCode.DownArrow) && LevelManager.Instance.gameCompleted < 3)
      {
         animator.speed--;
      }

      if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
      {
         Destroy(LevelManager.Instance.gameObject);
         Instantiate(LevelManagerPrefab);
         LevelManager.Instance.MainMenu();
      }
   }
}
