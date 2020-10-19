using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

   public float boundary = 100f;
   public float maximumSpeed = 10f;
   public Vector3 startPosition = new Vector3(3f, 50f, 0);
   Rigidbody r;
   GameObject hexTarget;
   GameObject hexHidden;

   private bool levelComplete = false;
   void Start()
   {
      r = GetComponent<Rigidbody>();
      hexTarget = GameObject.FindGameObjectWithTag("HexTarget");
      hexHidden = GameObject.FindGameObjectWithTag("HexHidden");
      r.AddForce(Vector3.down * 2000, ForceMode.Impulse);

      //StartCoroutine(Upload());

      /*
      for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
      {
            Debug.Log(i + ": " + SceneManager.GetSceneByBuildIndex(i).name);
      }
      */
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

      float speed = Vector3.Magnitude(r.velocity);  // test current object speed

      if (speed > maximumSpeed && !levelComplete)
      {
         float brakeSpeed = speed - maximumSpeed;  // calculate the speed decrease         
         r.AddForce(Vector3.up * brakeSpeed, ForceMode.Impulse);  // apply opposing brake force               

      }

      if (transform.position.y < boundary * -1)
      {
         if (!levelComplete)
            transform.position = startPosition;
         else
         {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            //Debug.Log(nextScene + " " + SceneManager.sceneCountInBuildSettings);
            if (SceneManager.sceneCountInBuildSettings < nextScene + 1)
            {
               SceneManager.LoadScene(0);
            }
            else SceneManager.LoadScene(nextScene);
         }

         //rigidbody.velocity = Vector3.zero;
         //rigidbody.angularVelocity = Vector3.zero;

      }
   }
   void OnTriggerExit(Collider other)
   {
      Debug.Log("TriggerExit" + gameObject.name + " with " + other.name);

      if (other.tag == hexTarget.tag)
      {
         levelComplete = true;
         r.AddForce(Vector3.down * 2000, ForceMode.Impulse);
      }
      //Destroy(gameObject);
      //Destroy(other);
   }

   void OnTriggerEnter(Collider other)
   {
      if (other.tag == hexHidden.tag)
      {
         Debug.Log("TriggerEnter" + gameObject.name + " with " + other.name);
         int r1 = Random.Range(1, 30);
         int r2 = Random.Range(1, 30);
         int r3 = Random.Range(1, 30);

         other.GetComponent<MeshCollider>().isTrigger = false;
         other.GetComponent<MeshCollider>().convex = true;
         Rigidbody r = other.GetComponent<Rigidbody>();

         r.isKinematic = false;
         r.AddTorque(new Vector3(r1, r2, r3), ForceMode.Impulse);
      }
   }
}
