using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour {
    Vector3 test;
    Vector3 vectest;
	// Use this for initialization
	void Start () {
        
	}
    //Quaternion test;
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
		if(Input.GetKeyDown(KeyCode.Return))
        {
            test = GameObject.Find("Main Camera").transform.position;
            vectest = GameObject.Find("1920x1080test2").transform.position;
            //test = GameObject.Find("GameObject").transform.rotation;
            Vector3 test2;
            Vector3 vectest2;
            float distance = 100;
            float duration = 10;
            Vector3 pos = this.transform.position;
            Vector3 pos2 = transform.InverseTransformPoint(this.transform.position);     // ワールド座標をスクリーン座標に変更
            //Quaternion pos2 = this.transform.rotation;
            Ray ray = new Ray();
            test2 = test - pos2;
            vectest2 = vectest - test2;
            
            Physics.Raycast(pos, test2, out hit, Mathf.Infinity);
            Debug.DrawRay(pos, test2, Color.red, duration, false);
            test2.Normalize();
            vectest2.Normalize();
            float val = Vector3.Dot(test2, vectest2);
            Debug.Log(val);
            

        }
    }
}
