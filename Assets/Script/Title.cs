﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //マウスクリックで任意のシーン切り替え
        if (Input.GetButtonDown("BButton"))
        {
            SceneManager.LoadScene("StageSelect");
        }

    }
}
