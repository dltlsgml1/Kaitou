using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PassStageID {

    public static int StageID;
    public static string StageName;

    // シングルトン
    static PassStageID _singleton = null;
    // インスタンス取得
    public static PassStageID GetInstance()
    {
        return _singleton ?? (_singleton = new PassStageID());
    }

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public static void GetStageID(int id)
    {
        StageID = id;
    }

    public static int PassStageId()
    {
        return StageID;
    }

    public static void GetStageName(string Name)
    {
        StageName = Name;
    }

    public static string PassStageName()
    {
        return StageName;
    }

}
