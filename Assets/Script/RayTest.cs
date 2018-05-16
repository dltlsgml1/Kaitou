using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour {
    public Camera maincamera;
    RaycastHit rayhit;
    Ray ray;
    Vector3 vec;
    // Use this for initialization
    void Start () {
        
    }
  

    void Update()
    {
     
        ray = maincamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out rayhit, 1000.0f))
        {
            Debug.Log(rayhit);
        }

        Debug.DrawRay(ray.origin,ray.direction*100,Color.red);



    }
}
