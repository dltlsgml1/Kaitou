using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
    public GameObject tt_Faield;
    private failed cs_failed;

	// Use this for initialization
	void Start () {
        cs_failed = tt_Faield.GetComponent<failed>();


    }
	
	// Update is called once per frame
	void Update () {
        //マウスクリックで任意のシーン切り替え
        if (Input.GetButtonDown("AButton"))
        {
            cs_failed.FadeIn_On();
           
        }
        if (cs_failed.FadeInEnd)
        {
            SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
        }

    }
}
