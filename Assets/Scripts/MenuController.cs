using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

   public int index = 0;
   public int maxIndex = 2;
   public bool keyDown;
   public GameObject audioManagerPrefab;
   public GameObject LevelManagerPrefab;

   // Start is called before the first frame update
   void Start()
   {
      GameObject manager = GameObject.FindGameObjectWithTag("AudioManager");
      if (!manager)
         manager = Instantiate(audioManagerPrefab);

      // always instantiate level manager
      if (LevelManagerPrefab)
         Instantiate(LevelManagerPrefab);
   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetButtonDown("Submit"))
      {
         AudioManager.Instance.PlayMenuPressed();
      }

      if (Input.GetButtonDown("Vertical"))
      {
         AudioManager.Instance.PlayMenuSelected();

         if (Input.GetAxisRaw("Vertical") < 0)
         {
            if (index < maxIndex)
            {
               index++;
            }
            else
            {
               index = 0;
            }
         }
         else if (Input.GetAxisRaw("Vertical") > 0)
         {
            if (index > 0)
            {
               index--;
            }
            else
            {
               index = maxIndex;
            }
         }
      }
   }
}
