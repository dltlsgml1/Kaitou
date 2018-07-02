using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StageTitle : MonoBehaviour {
    public Sprite test;
	// Use this for initialization
	void Start () {
        test = Resources.Load<Sprite>("Prefabs/Stage/Title/" + PassStageID.PassStageName());
        this.GetComponent<SpriteRenderer>().sprite = test;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
