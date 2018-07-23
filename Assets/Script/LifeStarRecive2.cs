using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStarRecive2 : MonoBehaviour {
    public int ReceiveLimitNum;
    public int GoldLimit;
    public int SilverLimit;
    public bool clearflg;
    //CSVData GetCSV;
    GameMain GetMain;
    
	// Use this for initialization
	void Start () {
        GetMain = GameObject.Find("MainSceneScript").GetComponent<GameMain>();
<<<<<<< HEAD
        GoldLimit=(int)CSVData.StageDateList[PassStageID.PassStageId()].GoldCunt;
        SilverLimit = (int)CSVData.StageDateList[PassStageID.PassStageId()].SilverCunt;
=======
        if(GetMain.TutorialFlg==false)
        {
            GoldLimit = (int)CSVData.StageDateList[PassStageID.PassStageId()].GoldCunt;
            SilverLimit = (int)CSVData.StageDateList[PassStageID.PassStageId()].SilverCunt;
        }
       
>>>>>>> Dev
       
        ReceiveLimitNum = GetMain.Limit;
	}
	
	// Update is called once per frame
	void Update () {
        


        clearflg = GetMain.ClearFlg;
        ReceiveLimitNum = GetMain.Limit;
        
	}
}
