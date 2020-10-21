using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
   public Transform hexPrefab;
   public List<Transform> list;

   public int gridWidth = 11;
   public int gridHeight = 11;
   public bool fragile = true;

   float hexWidth = 1.732f;
   float hexHeight = 2.0f;
   public float gap = 0.0f;

   Vector3 startPos;

   void Start()
   {
      AddGap();
      CalcStartPos();
      CreateGrid();
   }

   void AddGap()
   {
      hexWidth += hexWidth * gap;
      hexHeight += hexHeight * gap;
   }

   void CalcStartPos()
   {
      float offset = 0;
      if (gridHeight / 2 % 2 != 0)
         offset = hexWidth / 2;

      float x = -hexWidth * (gridWidth / 2) - offset;
      float z = hexHeight * 0.75f * (gridHeight / 2);

      startPos = new Vector3(x, 0, z);
   }

   Vector3 CalcWorldPos(Vector2 gridPos)
   {
      float offset = 0;
      if (gridPos.y % 2 != 0)
         offset = hexWidth / 2;

      float x = startPos.x + gridPos.x * hexWidth + offset;
      float z = startPos.z - gridPos.y * hexHeight * 0.75f;

      return new Vector3(x, 0, z);
   }

   void CreateGrid()
   {
      int rY = Random.Range(0,gridHeight);
      int rX = Random.Range(0,gridWidth);

      for (int y = 0; y < gridHeight; y++)
      {
         for (int x = 0; x < gridWidth; x++)
         {
            Transform hex = Instantiate(hexPrefab) as Transform;
            Vector2 gridPos = new Vector2(x, y);
            hex.position = CalcWorldPos(gridPos);
            hex.parent = this.transform;
            hex.name = "Hexagon" + x + "|" + y;
            if(rX == x && rY == y){
               hex.tag = "HexTarget";
            }
            list.Add(hex);
         }
      }      
   }
   private void Update(){
     
        
      if (Input.GetKeyDown(KeyCode.Space))
      {
         Transform obj = list[Random.Range(0,list.Count)];         
         DropHex(obj.gameObject);
/*
         
         //obj.GetComponent<Rigidbody>().useGravity = true;
         int r1 = Random.Range(1,30);
         int r2 = Random.Range(1,30);
         int r3 = Random.Range(1,30);


         obj.GetComponent<MeshCollider>().convex = true;
         Rigidbody r = obj.GetComponent<Rigidbody>();
         r.isKinematic = false;
         r.AddTorque(new Vector3(r1,r2,r3),ForceMode.Impulse);
         Debug.Log("Dropping " + obj.name);
         list.Remove(obj);
         for (int i = 0; i < list.Count; i++)
         {
         //list[i].GetComponent<Rigidbody>().useGravity = true;
         //list[i].GetComponent<Rigidbody>().isKinematic = false;
            //Debug.Log(list[i].name);
         }
         */
      }
        
   } 
   public void DropHex(GameObject hex){

         int r1 = Random.Range(1,30);
         int r2 = Random.Range(1,30);
         int r3 = Random.Range(1,30);

         if(fragile && list.Contains(hex.transform)){
            hex.GetComponent<MeshCollider>().convex = true;
            Rigidbody r = hex.GetComponent<Rigidbody>();
            r.isKinematic = false;
            r.AddTorque(new Vector3(r1,r2,r3),ForceMode.Impulse);
            Debug.Log("Dropping " + hex.name);
            list.Remove(hex.transform);
         }

   }
}
