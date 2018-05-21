using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour {
    public LoadMainStages StageLoader;
    public GameObject NowStageObj;
    public GameObject[] Block = new GameObject[10];
    public GameObject[] CollapsBlock = new GameObject[10];
    public GameObject[] WetBlock = new GameObject[10];
    public Camera MainCamera;
    public GameObject Box;
    Ray ray1, ray2;

    public int BlocksCount =0;

    public int NowStage = 0;
	void Start ()
    {
        StageLoader = gameObject.AddComponent<LoadMainStages>();
        StageLoader.LoadStage();
        SetStage(NowStage);
        //for(int i=0;i<5;i++)
        //{
        //    if(Block[i]!=null)
        //    {
        //        BlocksCount++;
        //    }
        //}
	}
	
    public void SetStage(int NowStage)
    {
        GameObject TempStage;
        TempStage = StageLoader.GetStage(NowStage);
        NowStageObj = TempStage;
        Block = GameObject.FindGameObjectsWithTag("NormalBlock");
        CollapsBlock = GameObject.FindGameObjectsWithTag("CollapseBlock");
        WetBlock = GameObject.FindGameObjectsWithTag("WetBlock");

    }

    

	// Update is called once per frame
	void Update ()
    {

        

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Block[0].gameObject.GetComponent<Blocks>().BurnFlg = true;
        //    Block[0].gameObject.GetComponent<Blocks>().SetBurn(Block[0]);

        //    CollapsBlock[0].gameObject.GetComponent<CollapseBlock>().BurnFlg = true;
        //    CollapsBlock[0].gameObject.GetComponent<CollapseBlock>().SetBurn(CollapsBlock[0]);


        //}


        Atari();
    }

    void Atari()
    {
        

        

    }

}
