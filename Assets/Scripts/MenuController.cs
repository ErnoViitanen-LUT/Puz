﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

   public int index = 0;
   public int maxIndex = 2;
   public bool keyDown;
   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetAxis("Vertical") != 0)
      {
         if (!keyDown)
         {
            if (Input.GetAxis("Vertical") < 0)
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
            else if (Input.GetAxis("Vertical") > 0)
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
            keyDown = true;
         }
      }
      else
      {
         keyDown = false;
      }
   }
}