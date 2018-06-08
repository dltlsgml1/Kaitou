using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour {

    GameObject StageSelectObject;
    StageSelect StageEnable;
    private int StageID;

	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
    {
        StageSelectObject = GameObject.Find("StagePrefab");
        StageEnable=StageSelectObject.GetComponent<StageSelect>();
        StageID = PassStageID.PassStageId();
    }

    // Update is called once per frame
    void Update () {
        StageSelect();
	}

    public void MapOpen()
    {

    }

    public void MapMove()
    {

    }

    public void StageSelect()
    {

        if (Input.GetButtonDown("BBotton"))
        {
            StageEnable.enabled = true;
            PassStageID.GetStageID(StageID);
            this.enabled = false;
        }

    }



}
