using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSceneEnd : MonoBehaviour {

    GameObject line;
    GameObject Cursor;
    private failed fade;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveLine()
    {
        //line.transform = Cursor.GetComponent<Pause>.transform;

    }

    public void Selected()
    {
        //(仮)
        int move = 0;

        //タイトルへ
        if (move == 0)
        {
            if (Input.GetButtonDown("AButton"))
            {
                //SE
                fade.FadeIn_On();
            }
            if (fade.FadeInEnd)
            {
                SceneManager.LoadScene("Title", LoadSceneMode.Single);
            }
        }
        //ゲーム終了
        if (move == 1)
        {
            if (Input.GetButtonDown("AButton"))
            {
                //SE
                fade.FadeIn_On();
            }
            if (fade.FadeInEnd)
            {
                Application.Quit();
            }
        }

    }


}
