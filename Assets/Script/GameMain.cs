using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //マウスクリックで任意のシーン切り替え
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("StageSelect");
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    SceneManager.LoadScene("Pause");
        //}

    }
}
