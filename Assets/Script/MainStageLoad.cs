using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class MainStageLoad : MonoBehaviour
{

    private static GameObject CSVData;
    private static CsvLoad CsvData;
    public GameObject MainStagePrefab;
    private float Distance = 14.0f;             //オブジェクト間の距離
    public int StageID;
    public GameObject MainObject;
    PassStageID PassID;
    MoveCamera Transform;
    // Use this for initialization
    void Start()
    {
        MainObject= GameObject.Find("GameObject");
        Transform=MainObject.GetComponent<MoveCamera>();
        MainStageSet();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MainStageSet()
    {
        StageID = PassStageID.PassStageId();
        Transform parent = this.transform;

        MainStagePrefab = (GameObject)Resources.Load("Prefab/Stage/" + PassStageID.PassStageName());
        Debug.Log(MainStagePrefab);
        Instantiate(MainStagePrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0),parent);
        Transform.Position = PassStageID.PassPosition();
        Debug.Log(Transform.Position);
        Transform.Rotation = PassStageID.PassRotation();
        Debug.Log(Transform.Rotation);


    }
}
