using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //キーで切り替え調整中
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space-KeyCode-");
            SceneManager.LoadScene("Title");
        }

        //マウスクリックで任意のシーン切り替え
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameMain");
        }


    }
}
