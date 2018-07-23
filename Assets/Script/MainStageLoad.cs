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
    public int StageID;
    public GameObject MainObject;
    PassStageID PassID;
    MoveCamera Transform;
    public int UpperCount;
    // Use this for initialization
    void Awake()
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
<<<<<<< HEAD
=======
        
>>>>>>> Dev
        StageID = PassStageID.PassStageId();
        Transform parent = this.transform;
        MainStagePrefab = (GameObject)Resources.Load("Prefabs/Stage/" + PassStageID.PassStageName());
        Instantiate(MainStagePrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0),parent);
        Transform.Position = PassStageID.PassPosition();
        Transform.Rotation = PassStageID.PassRotation();
        UpperCount = PassStageID.PassUpperCount();
<<<<<<< HEAD

=======
>>>>>>> Dev
    }
}
