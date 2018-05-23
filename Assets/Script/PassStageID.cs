using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PassStageID : MonoBehaviour {

    public GameObject StageIDObject;
    public int StageID;
    private bool CheckFlag = false;
	// Use this for initialization
	void Start () {

       
        
    }
	
	// Update is called once per frame
	void Update () {
        StageIDObject = GameObject.Find("StageSelectObject");
        if (StageIDObject != null)
        {
            StageSelect stageSelect = StageIDObject.GetComponent<StageSelect>();
            StageID = stageSelect.StageID;
           
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene("Title"); ;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
