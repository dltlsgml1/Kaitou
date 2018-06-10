﻿using UnityEngine;
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
    // Use this for initialization
    void Start()
    {
        
    }

    private void Awake()
    {
        CSVData = GameObject.Find("CSVLoad");
        CsvData = CSVData.GetComponent<CsvLoad>();
        SetStagePrefab();
        Debug.Log("ろーど");
        this.enabled = false;
    }

    // Update is called once per frame
    void Update () {
	}

    public void SetStagePrefab()
    {
        Transform parent = this.transform;
        parent.position = new Vector3(0, 0, 0);
        
        for (int i = 0; i < CsvLoad.height-1; i++)
        {
            StagePrefab = (GameObject)Resources.Load("StageSelectPrefab/"+CsvData.StageDateList[i+1].StageName);
            Instantiate(StagePrefab, new Vector3(i*Distance, 0, 0), Quaternion.Euler(-90, 0, 0), parent);
        }
    }
}
