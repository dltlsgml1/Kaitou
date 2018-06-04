using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PassStageID {

    public static int StageID = 1;
    public static string StageName;
    public static Vector3 CameraRotation;
    public static Vector3 CameraPosition;
    public static int UpperCount;
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
    public static void GetRotation(float x, float y, float z)
    {
        CameraRotation.x = x;
        CameraRotation.y = y;
        CameraRotation.z = z;
    }

    public static Vector3 PassRotation()
    {
        return CameraRotation;
    }

    public static void GetPosition(float x, float y, float z)
    {
        CameraPosition.x = x;
        CameraPosition.y = y;
        CameraPosition.z = z;
    }

    public static Vector3 PassPosition()
    {
        return CameraPosition;
    }

    public static void GetUpperCount(int Count)
    {
        UpperCount = Count;
    }
    public static int PassUpperCount()
    {
        return UpperCount;
    }
}
