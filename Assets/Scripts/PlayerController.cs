using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

   public float boundary = 100f;
   public float fallBoundary = 5f;
   public float maximumSpeed = 1000f;
   public Vector3 startPosition = new Vector3(3f, 50f, 0);
   Rigidbody r;

   public bool levelComplete = false;
   private bool aboutToDie = false;

   public GameObject pausePrefab;
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
      //StartPosition();

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
   public void StartPosition()
   {
      levelComplete = false;
      ResetPosition(true);
      StartVelocity();
      UpdateGameStatus();

   }

   public void StartVelocity()
   {

      if (r)
      {
         r.velocity = Vector3.zero;

         transform.gameObject.GetComponent<SimpleCharacterController>().Reset();
         r.AddForce(Vector3.down * 2000, ForceMode.Impulse);
      }
   }
   public void ResetPosition(bool playAudio)
   {
      if (playAudio)
         AudioManager.Instance.PlayReset();
      transform.position = startPosition;
   }
   public void ResetVelocity()
   {
      if (r)
      {
         r.velocity = Vector3.zero;
         transform.gameObject.GetComponent<SimpleCharacterController>().Reset();
         r.AddForce(Vector3.down * 2000, ForceMode.Impulse);
      }

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
      if (!LevelManager.Instance.betweenLevels)
      {
         if (Input.GetButtonDown("Cancel"))
         {
            GameObject pause = GameObject.FindGameObjectWithTag("Pause");
            if (pause)
            {
               Time.timeScale = 1;
               Destroy(pause);
            }
            else
            {
               Time.timeScale = 0;
               Instantiate(pausePrefab);
            }

         }
         if (Time.timeScale == 1f)
         {
            brakeDescend();
         }

         if (transform.position.y < fallBoundary * -1 && !aboutToDie && !levelComplete)
         {
            aboutToDie = true;
            AudioManager.Instance.PlayFall();
         }
         if (transform.position.y < boundary * -1)
         {
            if (!levelComplete)
            {
               Respawn();
            }

            else LoadNextLevel();
         }
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

         //AudioManager.Instance.PlayFall();
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
      aboutToDie = false;
      health--;
      if (health < 1) GameOver();
      else ResetPosition(true);
   }
   void CompleteLevel()
   {
      levelComplete = true;
      r.AddForce(Vector3.down * 2000, ForceMode.Impulse);
      AudioManager.Instance.PlayLevelClear();
      health++;

   }
   public void Reset()
   {
      levelComplete = false;
   }
   void GameOver()
   {
      Destroy(gameObject);
      LevelManager.Instance.GameOver();
   }
   void LoadNextLevel()
   {

      LevelManager.Instance.LoadNextLevel();

      /*int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

      if (SceneManager.sceneCountInBuildSettings < nextScene + 1)
      {
         gameCompleted++;
         
         SceneManager.LoadScene(1);
      }
      else SceneManager.LoadScene(nextScene);*/
   }
   void UpdateGameStatus()
   {
      if (LevelManager.Instance)
         gameObject.GetComponent<SimpleCharacterController>().easyMode = LevelManager.Instance.easyMode;
   }

}
