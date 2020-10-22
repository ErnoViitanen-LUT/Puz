using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{

   public Material targetMaterial;
   public Material hiddenMaterial;
   // Start is called before the first frame update
   GameObject target;
   public GameObject playerPrefab;
   public GameObject audioManagerPrefab;
   GameObject player;
   CameraController cameraController;
   void Start()
   {

      Instantiate(audioManagerPrefab);
      cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
      cameraController.player = Instantiate(playerPrefab);

      // assign the material to the renderer

   }

   // Update is called once per frame
   void Update()
   {
      if (!target)
      {
         GameObject[] targets = GameObject.FindGameObjectsWithTag("HexTarget");

         Debug.Log(targets.Length);
         target = targets[Random.Range(0, targets.Length)];

         //target = GameObject.FindGameObjectWithTag("HexTarget");
         //Debug.Log(target.name);

         target.GetComponent<Renderer>().material = targetMaterial;
         MeshCollider collider = target.GetComponent<MeshCollider>();
         collider.convex = true;
         collider.isTrigger = true;

         foreach (var item in GameObject.FindGameObjectsWithTag("HexHidden"))
         {
            item.GetComponent<Renderer>().material = hiddenMaterial;
            MeshCollider coll = item.GetComponent<MeshCollider>();
            coll.convex = true;
            coll.isTrigger = true;
         }
         PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
         player.Reset();
      }
   }
}
