using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class StageLoad : MonoBehaviour {
    

    public GameObject StagePrefab;
    private float Distance = 14.0f;             //オブジェクト間の距離
    public int StageID;
    PassStageID PassID;
    // Use this for initialization
    void Start()
    {
        
    }

    private void Awake()
    {

        SetStagePrefab();
        this.enabled = false;
    }

    // Update is called once per frame
    void Update () {
	}

    public void SetStagePrefab()
    {
        Transform parent = this.transform;
        parent.position = new Vector3(0, 0, 0);
        
        for (int i = 0; i < 29; i++)
        {
            StagePrefab = (GameObject)Resources.Load("StageSelectPrefab/"+CSVData.StageDateList[i].StageName);
            Instantiate(StagePrefab, new Vector3(i*Distance, 0, 0), Quaternion.Euler(-90, 0, 0), parent);
        }
    }
}
