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
   public GameObject canvasPrefab;
   GameObject player;
   CameraController cameraController;
   GameObject canvas;


   public float startTimeToDrop = 5f;
   public float nextTimeToDrop = 1f;

   void Start()
   {
      if (!GameObject.FindGameObjectWithTag("AudioManager"))
         Instantiate(audioManagerPrefab);

      canvas = Instantiate(canvasPrefab);

      cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

      player = GameObject.FindGameObjectWithTag("Player");

      if (!player)
      {
         player = Instantiate(playerPrefab);
      }

      TimeController timeController = gameObject.AddComponent<TimeController>();
      timeController.startTimeToDrop = startTimeToDrop;
      timeController.nextTimeToDrop = nextTimeToDrop;
      timeController.text = canvas.GetComponentInChildren<UnityEngine.UI.Text>().gameObject;

      cameraController.player = player;

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
