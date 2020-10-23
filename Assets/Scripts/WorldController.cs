using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
   PlayerController playerController;
   CameraController cameraController;
   GameObject canvas;
   CanvasController canvasController;


   public float startTimeToDrop = 5f;
   public float nextTimeToDrop = 1f;

   void Start()
   {
      if (!GameObject.FindGameObjectWithTag("AudioManager"))
         Instantiate(audioManagerPrefab);

      canvas = Instantiate(canvasPrefab);
      canvasController = canvas.GetComponent<CanvasController>();
      cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

      player = GameObject.FindGameObjectWithTag("Player");

      if (!player)
      {
         player = Instantiate(playerPrefab);
      }
      playerController = player.GetComponent<PlayerController>();
      playerController.healthChanged = new System.Action(SetHealth);
      SetHealth();
      SetLevel();
      playerController.ResetPosition();

      TimeController timeController = gameObject.AddComponent<TimeController>();
      timeController.startTimeToDrop = startTimeToDrop;
      timeController.nextTimeToDrop = nextTimeToDrop;

      timeController.timeControl = canvasController.time;

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

         playerController.ResetVelocity();
      }
   }

   public void SetHealth()
   {
      string healthText = "";
      if (playerController.health > 5)
      {
         healthText = "O " + playerController.health;
      }
      else
      {
         for (int i = 0; i < playerController.health; i++)
         {
            healthText += "O ";
         }
      }
      canvasController.health.text = healthText;
   }
   public void SetLevel()
   {
      string levelText = "";
      int currentScene = SceneManager.GetActiveScene().buildIndex + (SceneManager.sceneCountInBuildSettings - 1) * playerController.gameCompleted;
      /*for (int i = 0; i < currentScene; i++)
      {
         levelText += "W ";
      }*/

      levelText = "W " + currentScene;
      canvasController.level.text = levelText;

   }

}
