using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StageTitle : MonoBehaviour {

	Renderer Render_test;
	// Use this for initialization
	void Start () {
		this.GetComponent<Renderer>().material.mainTexture = Resources.Load ("Prefabs/Stage/" + PassStageID.PassStageName ()) as Texture;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
