using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionController : MonoBehaviour
{

   string version = "0.1.0";
   public GameObject text;
   // Start is called before the first frame update
   void Start()
   {
      text.GetComponent<UnityEngine.UI.Text>().text = "v. " + version;
   }

   // Update is called once per frame
   void Update()
   {

   }
}
