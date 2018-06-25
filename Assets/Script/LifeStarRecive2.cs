using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStarRecive2 : MonoBehaviour {
    public int ReceiveLimitNum;
    public int GoldLimit;
    public int SilverLimit;
    public bool clearflg;

    GameMain GetMain;
    //GameObject GetMainObj;
    
	// Use this for initialization
	void Start () {
        //GetMainObj = GameObject.Find("MainSceneScript");
        GetMain = GameObject.Find("MainSceneScript").GetComponent<GameMain>();
        //ReceiveLimitNum = PassStageID.PassUpperCount();
        ReceiveLimitNum = GetMain.Limit;
	}
	
	// Update is called once per frame
	void Update () {
        clearflg = GetMain.ClearFlg;
        ReceiveLimitNum = GetMain.Limit;
        //ReceiveLimitNum = GetMain.NormalCount;
	}
}
