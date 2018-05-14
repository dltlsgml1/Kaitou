using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour {
    public Camera camera;
    private Ray ray;
    Vector3 vec;
    // Use this for initialization
    void Start () {
        
    }
  

    void Update()
    {
        //  Debug.Log(camera.transform.position);

        vec = camera.transform.position;
        
        Vector3 zerovec;
        Vector3 scray;
        zerovec.x = 0;
        zerovec.y = 0;
        zerovec.z = 1;

        ray.origin = vec;
        ray.direction = zerovec;
        Debug.Log(vec);
        //Debug.Log(camera.WorldToScreenPoint(ray.origin));
         Debug.DrawRay(ray.origin, ray.direction, Color.red,10);

        //Debug.DrawRay(ray.origin, vec, Color.red);
        //Debug.DrawRay(vec, ray.origin, Color.red);




    }
}
