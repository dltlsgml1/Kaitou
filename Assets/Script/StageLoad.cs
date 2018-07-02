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
    StageRank StageRank;

    // Use this for initialization
    void Start()
    {
        CSVData = GameObject.Find("CSVLoad");
        CsvData = CSVData.GetComponent<CsvLoad>();
        ScreenShot = GetComponent<ScreenShot>();
        StageRank = GetComponent<StageRank>();
        ScreenShot.Init("Stage", "ClearStageSS", "ClearImage");
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

            StageRank.CheckRank(i);
            SetClearFrame(parent.Find(CsvData.StageDateList[i + 1].StageName + "(Clone)").gameObject, StageRank.GetRank());
            SetClearStarMaterial(parent.Find(CsvData.StageDateList[i + 1].StageName + "(Clone)/Star").gameObject, StageRank.GetRank());

            string stagename = CsvData.StageDateList[i + 1].StageName;
            int stageId = (int)CsvData.StageDateList[i + 1].StageID;
            ScreenShot.SearchToSetClearImage(stageId, Int32.Parse(stagename.Substring(5)));
        }
    }

    private void SetClearFrame(GameObject obj, StageRank.RANK rank)
    {
        switch (rank)
        {
            case StageRank.RANK.NORMAL:
                obj.transform.Find("NormalFrame").gameObject.SetActive(true);
                obj.transform.Find("BronzeFrame").gameObject.SetActive(false);
                obj.transform.Find("SilverFrame").gameObject.SetActive(false);
                obj.transform.Find("GoldFrame").gameObject.SetActive(false);
                break;

            case StageRank.RANK.BRONZE:
                obj.transform.Find("NormalFrame").gameObject.SetActive(false);
                obj.transform.Find("BronzeFrame").gameObject.SetActive(true);
                obj.transform.Find("SilverFrame").gameObject.SetActive(false);
                obj.transform.Find("GoldFrame").gameObject.SetActive(false);
                break;

            case StageRank.RANK.SILVER:
                obj.transform.Find("NormalFrame").gameObject.SetActive(false);
                obj.transform.Find("BronzeFrame").gameObject.SetActive(false);
                obj.transform.Find("SilverFrame").gameObject.SetActive(true);
                obj.transform.Find("GoldFrame").gameObject.SetActive(false);
                break;

            case StageRank.RANK.GOLD:
                obj.transform.Find("NormalFrame").gameObject.SetActive(false);
                obj.transform.Find("BronzeFrame").gameObject.SetActive(false);
                obj.transform.Find("SilverFrame").gameObject.SetActive(false);
                obj.transform.Find("GoldFrame").gameObject.SetActive(true);
                break;

        }
    }

    private void SetClearStarMaterial(GameObject obj, StageRank.RANK rank)
    {
        switch (rank)
        {
            case StageRank.RANK.NORMAL:
                obj.transform.Find("LeftStar/Cone").GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
                obj.transform.Find("CenterStar/Cone").GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
                obj.transform.Find("RightStar/Cone").GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
                break;

            case StageRank.RANK.BRONZE:
                obj.transform.Find("LeftStar/Cone").GetComponent<Renderer>().material.color = new Color(1, 1, 0, 1);
                obj.transform.Find("CenterStar/Cone").GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
                obj.transform.Find("RightStar/Cone").GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
                break;

            case StageRank.RANK.SILVER:
                obj.transform.Find("LeftStar/Cone").GetComponent<Renderer>().material.color = new Color(1, 1, 0, 1);
                obj.transform.Find("CenterStar/Cone").GetComponent<Renderer>().material.color = new Color(1, 1, 0, 1);
                obj.transform.Find("RightStar/Cone").GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
                break;

            case StageRank.RANK.GOLD:
                obj.transform.Find("LeftStar/Cone").GetComponent<Renderer>().material.color = new Color(1, 1, 0, 1);
                obj.transform.Find("CenterStar/Cone").GetComponent<Renderer>().material.color = new Color(1, 1, 0, 1);
                obj.transform.Find("RightStar/Cone").GetComponent<Renderer>().material.color = new Color(1, 1, 0, 1);
                break;

        }
    }


    private string CastStageId(int stageid)
    {
        string str;

        if (stageid >= 0 && stageid <= 9)
        {
            str = "0" + stageid;
        }
        else
        {
            str = stageid + "";
        }

        return str;
    }

}
