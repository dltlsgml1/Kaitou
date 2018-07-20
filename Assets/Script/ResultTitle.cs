using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class ResultTitle : MonoBehaviour {
    public Sprite Rtitle;
	// Use this for initialization
	void Start () {
        Rtitle = Resources.Load<Sprite>("Prefabs/Stage/Title/" + PassStageID.PassStageName());
        this.GetComponent<SpriteRenderer>().sprite = Rtitle;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
