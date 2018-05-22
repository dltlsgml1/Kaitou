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

    public GameObject[] bk = new GameObject[10];
    public GameObject[] ck = new GameObject[10];

    public int CollapsBlockCount = 0;
    public int BlocksCount = 0;
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

        Vector3[] bkvec = new Vector3[6];
        Vector3[] ckvec = new Vector3[6];
        for (int i = 0; i < CollapsBlock.Length; i++)
        {      
            for (int j=0;j<Block.Length;j++)
            {
                for (int k=0;k<6;k++)
                {
                    ck[k] = CollapsBlock[i].transform.GetChild(k).gameObject;
                    ckvec[k] = MainCamera.WorldToScreenPoint(ck[k].transform.position);
                    for (int l = 0; l < 6;l++)
                    {
                        bk[l] = Block[j].transform.GetChild(l).gameObject;   
                        bkvec[l] = MainCamera.WorldToScreenPoint(bk[l].transform.position);
                        if(Vector3.Distance(ckvec[k],bkvec[l])<5.0f)
                        {
                            Block[j].gameObject.GetComponent<Blocks>().BurnFlg = true;
                            Block[j].gameObject.GetComponent<Blocks>().SetBurn(Block[0]);
                            CollapsBlock[i].gameObject.GetComponent<CollapseBlock>().BurnFlg = true;
                            CollapsBlock[i].gameObject.GetComponent<CollapseBlock>().SetBurn(CollapsBlock[0]);
                        }

                    }
                }
            }
           
        }


        //if (Vector3.Distance(bkvec[(int)DefineScript.CollisionIndex.Right],ckvec[(int)DefineScript.CollisionIndex.Left])<5.0f)
        //{

        //        Block[0].gameObject.GetComponent<Blocks>().BurnFlg = true;
        //        Block[0].gameObject.GetComponent<Blocks>().SetBurn(Block[0]);

        //        CollapsBlock[0].gameObject.GetComponent<CollapseBlock>().BurnFlg = true;
        //        CollapsBlock[0].gameObject.GetComponent<CollapseBlock>().SetBurn(CollapsBlock[0]);

        //}

        Debug.Log(bkvec[(int)DefineScript.CollisionIndex.Top]);
        Debug.Log(ckvec[(int)DefineScript.CollisionIndex.Top]);



    }

}
