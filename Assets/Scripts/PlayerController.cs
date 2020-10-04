using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

   private float speed = 4.0f;
   private float turnSpeed = 200f;
   private float horizontalInput;
   private float forwardInput;
   // Start is called before the first frame update

   Rigidbody r;
   void Start()
   {
      r = GetComponent<Rigidbody>();

   }

   // Update is called once per frame
   void Update()
   {

      horizontalInput = Input.GetAxis("Horizontal");
      forwardInput = Input.GetAxis("Vertical");

      transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

      //     PlayerController.ApplyForceToReachVelocity(r,transform.forward * 10,1000);

      transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
   }
}
