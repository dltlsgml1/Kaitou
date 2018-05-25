using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainStages : MonoBehaviour
{
    public GameObject[] Stages = new GameObject[DefineScript.NUM_STAGE];          //ステージプレファブ
    public Sprite[] Backgrounds = new Sprite[DefineScript.NUM_BACKGROUND];          //バックグラウンドの画像
    public Texture2D[] Screenshot = new Texture2D[DefineScript.NUM_STAGE];      //撮影されたスクリーンショットを一時確報

    // Use this for initialization
    public void LoadStage()
    {
        Backgrounds[0] = Resources.Load<Sprite>("images");
        //Stages[0] = Resources.Load(DefineScript.PASS_STAGE + "Stage02") as GameObject;
       // Instantiate(Stages[0]);
    }

    public GameObject GetStage(int StageNum)
    {
        return Stages[StageNum];
    }
	
}
