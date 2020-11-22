using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
   public Light sun;
   public Light flashlight;
   public float secondsInFullDay = 240f;
   [Range(0, 1)]
   public float currentTimeOfDay = 0;
   [HideInInspector]
   public float timeMultiplier = 1f;

   bool isFlashlightOn = false;

   float sunInitialIntensity;
   // Start is called before the first frame update
   void Start()
   {
      sunInitialIntensity = sun.intensity;
   }

   // Update is called once per frame
   void Update()
   {
      UpdateSun();

      currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

      if (currentTimeOfDay >= 1)
      {
         currentTimeOfDay = 0;
      }
   }
   void UpdateSun()
   {
      //sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

      float intensityMultiplier = 1;

      if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
      {
         intensityMultiplier = 0;
         if (!isFlashlightOn)
         {
            isFlashlightOn = true;
            Invoke("FlashlightOn", 1);
         }
      }
      else if (currentTimeOfDay <= 0.25f) // day is coming
      {
         intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
      }
      else if (currentTimeOfDay >= 0.73f) // night is coming
      {
         intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
      }

      sun.intensity = sunInitialIntensity * intensityMultiplier;
      if (isFlashlightOn && sun.intensity == 1)
      {
         isFlashlightOn = false;
         Invoke("FlashlightOff", 1);
      }
   }
   void FlashlightOn()
   {
      flashlight.intensity = 1f;
      AudioManager.Instance.PlayFlashlight();
   }
   void FlashlightOff()
   {
      flashlight.intensity = 0f;
      AudioManager.Instance.PlayFlashlight();
   }

   public void SwitchDayNightMode(){
      if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.73f) // night time
      {
         currentTimeOfDay = 0.23f;
      }
      else // day time
      {
         currentTimeOfDay = 0.73f;

      }
   }
}
