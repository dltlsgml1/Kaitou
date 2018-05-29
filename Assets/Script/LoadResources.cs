using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadResources : MonoBehaviour {

    public static GameObject[] Stages = new GameObject[DefineScript.NUM_STAGE];          //ステージプレファブ
    public static Sprite[] Backgrounds = new Sprite[DefineScript.NUM_BACKGROUND];         //バックグラウンドの画像

    public static Material Mat_Normal;
    public static Material Mat_Collaps;


    private void Awake()
    {
        LoadSound();
        LoadStage();
        LoadBackGround();
        LoadMaterial();

    }
 
    void LoadSound()
    {

    }

    void LoadStage()
    {
        string TempStr;
        for (int i = 0; i < DefineScript.NUM_STAGE; i++)
        {
            TempStr = i.ToString();
            Stages[i] = Resources.Load(DefineScript.PASS_RESOURCE_STAGE + "Stage" + TempStr) as GameObject;
        }
    }

    void LoadBackGround()
    {
        string TempStr;

        for (int i = 0; i < DefineScript.NUM_BACKGROUND; i++)
        {
            TempStr = i.ToString();
            Backgrounds[i] = Resources.Load<Sprite>(DefineScript.PASS_RESOURCE_BACKGROUNDS + "Background" + TempStr);
        }
    }

    void LoadMaterial()
    {


        Mat_Normal = Resources.Load("GameMain/Materials/GameMain_BlockNomal_01") as Material;
        Mat_Collaps = Resources.Load("GameMain/Materials/GameMain_BlockNomal_02") as Material;
    }
    
}
