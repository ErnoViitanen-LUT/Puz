using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

   public float boundary = 100f;
   public float maximumSpeed = 1000f;
   public Vector3 startPosition = new Vector3(3f, 50f, 0);
   Rigidbody r;

   private bool levelComplete = false;
   public int gameCompleted = 0;

   public System.Action healthChanged;
   [SerializeField]
   private int _health;
   public int health
   {
      get { return _health; }
      set
      {
         _health = value;
         Debug.Log("health changed to " + _health);
         healthChanged();
      }
   }
   private void Awake()
   {

      Debug.Log("Awake");
   }
   void Start()
   {
      Debug.Log("Start");
      health = 1;
      r = GetComponent<Rigidbody>();
      //audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
      ResetVelocity();

      //audioSource.clip = audioTarget;

      DontDestroyOnLoad(transform.gameObject);

      //StartCoroutine(Upload());

      /*
      for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
      {
            Debug.Log(i + ": " + SceneManager.GetSceneByBuildIndex(i).name);
      }
      */
   }
   public void ResetPosition()
   {
      transform.position = startPosition;
   }
   public void ResetVelocity()
   {
      r.velocity = Vector3.zero;
      transform.gameObject.GetComponent<SimpleCharacterController>().Reset();
      r.AddForce(Vector3.down * 2000, ForceMode.Impulse);
   }

   IEnumerator Upload()
   {
      List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
      formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
      formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

      UnityWebRequest www = UnityWebRequest.Get("https://api.cgiaws.com/test/demo/web/mobile");
      www.SetRequestHeader("Authorization", "APIKEY");
      yield return www.SendWebRequest();

      if (www.isNetworkError || www.isHttpError)
      {
         Debug.Log(www.error);
      }
      else
      {
         Debug.Log(www.responseCode + " " + www.downloadHandler.text);
         Debug.Log("Form upload complete!");
      }
   }

   // Update is called once per frame
   void Update()
   {

      brakeDescend();

      if (transform.position.y < boundary * -1)
      {
         if (!levelComplete)
         {
            Respawn();
         }

         else LoadNextLevel();
      }
   }

   void brakeDescend()
   {
      float speed = Vector3.Magnitude(r.velocity);  // test current object speed
      if (speed > maximumSpeed && !levelComplete)
      {
         float brakeSpeed = speed - maximumSpeed;  // calculate the speed decrease              
         r.AddForce(r.velocity * (-brakeSpeed / 10), ForceMode.Impulse);  // apply opposing brake force   
      }
   }

   void OnTriggerExit(Collider other)
   {
      Debug.Log("TriggerExit" + gameObject.name + " with " + other.name);

      if (other.tag == "HexTarget" && !levelComplete)
      {
         CompleteLevel();
      }
      //Destroy(gameObject);
      //Destroy(other);
   }

   void OnTriggerEnter(Collider other)
   {
      if (other.tag == "HexHidden")
      {
         other.GetComponent<DropController>().drop();
      }
      else if (other.tag == "Player")
      {
         Debug.Log("TriggerEnter" + gameObject.name + " with " + other.name);
         int r1 = Random.Range(1, 30);
         int r2 = Random.Range(1, 30);
         int r3 = Random.Range(1, 30);
         Rigidbody r = other.GetComponent<Rigidbody>();

         r.AddForce(Vector3.forward * 10, ForceMode.Impulse);
      }
   }


   void Respawn()
   {
      health--;
      if (health < 1) EndGame();
      else ResetPosition();
   }
   void CompleteLevel()
   {
      levelComplete = true;
      r.AddForce(Vector3.down * 2000, ForceMode.Impulse);
      AudioManager.Instance.PlayLevelClear();
      health++;
      //Invoke("LoadNextLevel", 2f);
   }

   void LoadNextLevel()
   {
      levelComplete = false;
      int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
      if (SceneManager.sceneCountInBuildSettings < nextScene + 1)
      {
         gameCompleted++;
         gameObject.GetComponent<SimpleCharacterController>().easyMode = false;
         SceneManager.LoadScene(1);
      }
      else SceneManager.LoadScene(nextScene);
   }

   void EndGame()
   {
      Destroy(gameObject);
      //Destroy(AudioManager.Instance);
      SceneManager.LoadScene(0);
   }

}
