using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class StageLoad : MonoBehaviour {
    
    private static GameObject CSVData;
    private static CsvLoad CsvData;
    public GameObject StagePrefab;
    public float Distance = 15.0f;             //オブジェクト間の距離
    private int StageID = 1;

    // Use this for initialization
    void Start()
    {
        CSVData = GameObject.Find("CSVLoad");
        CsvData = CSVData.GetComponent<CsvLoad>();
    }
	
	// Update is called once per frame
	void Update () {
		       
	}

    public void SetStagePrefab()
    {
        StagePrefab = (GameObject)Resources.Load(CsvData.StageDateList[1].StageName);
        Instantiate(StagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
