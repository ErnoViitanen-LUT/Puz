using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   public Vector3 offset = new Vector3(0, 12, -7);

   [HideInInspector]
   public GameObject player;
   // Start is called before the first frame update
   void Start()
   {
      //player = GameObject.FindGameObjectWithTag("Player");
   }

   // Update is called once per frame
   void Update()
   {
      if (player)
         transform.position = player.transform.position + offset;
   }
}
