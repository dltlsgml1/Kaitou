using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainStages : MonoBehaviour
{
    public GameObject[] Stage = new GameObject[DefineScript.NUM_STAGE];          //ステージプレファブ
    public int NowStage { get; private set; }               //現在ステージのインデックス
    public Sprite Background { get; private set; }          //バックグラウンドの画像
    public Texture2D Screenshot { get; private set; }       //撮影されたスクリーンショットを一時確報

    // Use this for initialization
    void Start()
    {
        Stage[0] = Resources.Load(DefineScript.PASS_STAGE + "Stage_01") as GameObject;
        GameObject NowStage = Instantiate(Stage[0]);
        Debug.Log(Stage[0]);
    }


	// Update is called once per frame
	void Update ()
    {
		
	}
}
