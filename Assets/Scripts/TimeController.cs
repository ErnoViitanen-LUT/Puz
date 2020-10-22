using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public GameObject text;
    UnityEngine.UI.Text time;

    float timer;
    bool timeStarted = true;

    public float startTimeToDrop = 5f;
    float timeToDrop;
    public float nextTimeToDrop = 1f;
    
    WorldController world;
    List<TimeDropper> sortedGrid;
 
    // Start is called before the first frame update
    void Start()
   {
      world = gameObject.GetComponent<WorldController>();
      time = text.GetComponent<UnityEngine.UI.Text>();
      Debug.Log(gameObject.transform.childCount);
      sortedGrid = new List<TimeDropper>();
      timeToDrop = startTimeToDrop;
      foreach (Transform item in gameObject.transform)
      {
         TimeDropper dropper = item.GetComponent<TimeDropper>();
         if(dropper.dropIndex != 0){
            sortedGrid.Add(dropper);               
         }
      }
      sortedGrid.Sort((p1,p2)=>p1.dropIndex.CompareTo(p2.dropIndex));      
   }

    // Update is called once per frame
    void Update()
    {        
        if (timeStarted == true) 
        {
           for (int i = 0; i < sortedGrid.Count; i++)
           {
               Debug.Log(sortedGrid[i].name + " " + sortedGrid[i].dropIndex);
           }
            timer += Time.deltaTime;
            timeToDrop -= Time.deltaTime;
            if(timeToDrop < 0 && sortedGrid.Count > 0){
               timeToDrop = nextTimeToDrop;
               sortedGrid[0].drop();
               sortedGrid.RemoveAt(0);
            }

            ShowOnGui();
            
        }       
    }
    void ShowOnGui()
    {
        
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");
    
        //GUI.Label(new Rect(10,10,250,100), minutes + ":" + Mathf.RoundToInt(seconds));
        time.text = FloatToTime(timer,"#0:00.0");
    }
    public string FloatToTime (float toConvert, string format){
         switch (format){
             case "00.0":
                 return string.Format("{0:00}:{1:0}", 
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*10) % 10));//miliseconds
             case "#0.0":
                 return string.Format("{0:#0}:{1:0}", 
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*10) % 10));//miliseconds
             
             case "00.00":
                 return string.Format("{0:00}:{1:00}", 
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*100) % 100));//miliseconds
             case "00.000":
                 return string.Format("{0:00}:{1:000}", 
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*1000) % 1000));//miliseconds
             case "#00.000":
                 return string.Format("{0:#00}:{1:000}", 
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*1000) % 1000));//miliseconds
             case "#0:00":
                 return string.Format("{0:#0}:{1:00}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60);//seconds
             case "#00:00":
                 return string.Format("{0:#00}:{1:00}", 
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60);//seconds
             case "0:00.0":
                 return string.Format("{0:0}:{1:00}.{2:0}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*10) % 10));//miliseconds
             case "#0:00.0":
                 return string.Format("{0:#0}:{1:00}.{2:0}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*10) % 10));//miliseconds
             case "0:00.00":
                 return string.Format("{0:0}:{1:00}.{2:00}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*100) % 100));//miliseconds
             case "#0:00.00":
                 return string.Format("{0:#0}:{1:00}.{2:00}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*100) % 100));//miliseconds
             case "0:00.000":
                 return string.Format("{0:0}:{1:00}.{2:000}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*1000) % 1000));//miliseconds
             case "#0:00.000":
                 return string.Format("{0:#0}:{1:00}.{2:000}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*1000) % 1000));//miliseconds
             
         }
         return "error";
     }

}
