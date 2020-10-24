﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

   public int index = 0;
   public int maxIndex = 2;
   public bool keyDown;
   public GameObject audioManagerPrefab;
   // Start is called before the first frame update
   AudioManager audioManager;

   void Start()
   {
      GameObject manager = GameObject.FindGameObjectWithTag("AudioManager");
      if (!manager)
         manager = Instantiate(audioManagerPrefab);
      audioManager = manager.GetComponent<AudioManager>();
   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetButtonDown("Submit"))
      {
         audioManager.PlayMenuPressed();
      }

      if (Input.GetButtonDown("Vertical"))
      {
         audioManager.PlayMenuSelected();

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
