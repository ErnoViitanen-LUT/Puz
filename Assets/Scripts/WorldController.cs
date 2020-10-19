using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    
    public Material targetMaterial;
    public Material hiddenMaterial;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("HexTarget");
     
        // assign the material to the renderer
        gameObject.GetComponent<Renderer>().material = targetMaterial;   
        MeshCollider collider = gameObject.GetComponent<MeshCollider>();
        collider.convex = true;
        collider.isTrigger = true;

        foreach (var item in GameObject.FindGameObjectsWithTag("HexHidden"))
        {
            item.GetComponent<Renderer>().material = hiddenMaterial;   
            MeshCollider coll = item.GetComponent<MeshCollider>();
            coll.convex = true;
            coll.isTrigger = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
