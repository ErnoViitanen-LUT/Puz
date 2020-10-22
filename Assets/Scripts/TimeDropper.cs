using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDropper : MonoBehaviour
{
    public int dropIndex;
    // Start is called before the first frame update
    public void drop(){
        
         int r1 = Random.Range(1, 30);
         int r2 = Random.Range(1, 30);
         int r3 = Random.Range(1, 30);

         gameObject.GetComponent<MeshCollider>().isTrigger = false;
         gameObject.GetComponent<MeshCollider>().convex = true;
         
         Rigidbody r = gameObject.GetComponent<Rigidbody>();

         r.isKinematic = false;
         r.useGravity = true;
         r.AddTorque(new Vector3(r1, r2, r3), ForceMode.Impulse);
    }
}
