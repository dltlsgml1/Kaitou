using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class StageLoad : MonoBehaviour {
    
    private static GameObject CSVData;
    private static CsvLoad CsvData;
    public GameObject StagePrefab;
    private float Distance = 14.0f;             //オブジェクト間の距離
    public int StageID;
    PassStageID PassID;
    ScreenShot ScreenShot;

    // Use this for initialization
    void Start()
    {
        CSVData = GameObject.Find("CSVLoad");
        CsvData = CSVData.GetComponent<CsvLoad>();
        ScreenShot = GetComponent<ScreenShot>();
        ScreenShot.Init("Kaitou", "Stage", "ClearStageSS", "ClearImage");
        SetStagePrefab();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void SetStagePrefab()
    {
        Transform parent = this.transform;
        for (int i = 0; i < CsvLoad.height-1; i++)
        {
            StagePrefab = (GameObject)Resources.Load("StageSelectPrefab/"+CsvData.StageDateList[i+1].StageName);
            Instantiate(StagePrefab, new Vector3(i*Distance, 0, 0), Quaternion.Euler(-90, 0, 0), parent);
            ScreenShot.SearchToSetClearImage(i);
        }
    }
}
