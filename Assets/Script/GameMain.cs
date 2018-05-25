﻿using System.Collections;
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


    public int CollapsBlockCount = 0;
    public int BlocksCount = 0;
    public int NowStage = 0;

	void Start ()
    {
        StageLoader = gameObject.AddComponent<LoadMainStages>();
        StageLoader.LoadStage();
        SetStage(NowStage);
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

    
    
	void Update ()
    {
       
        Atari();
    }

    void Atari()
    {

        bool IsOveraped=false;


        GameObject[] bk = new GameObject[10];
        GameObject[] ck = new GameObject[10];
        Vector3[] bkvec = new Vector3[6];
        Vector3[] ckvec = new Vector3[6];
        Vector3[] blockposition = new Vector3[Block.Length];
        Vector3[] collapsblockposition = new Vector3[CollapsBlock.Length];
        for(int i=0;i<Block.Length;i++)
        {
            blockposition[i] = MainCamera.WorldToScreenPoint(Block[i].transform.position);    
        }
        for(int i=0;i<CollapsBlock.Length; i++)
        {
            collapsblockposition[i] = MainCamera.WorldToScreenPoint(CollapsBlock[i].transform.position);
          
        }

      

        for (int CollapsCount = 0; CollapsCount < CollapsBlock.Length; CollapsCount++) 
        {
            for (int BlockCount = 0; BlockCount < Block.Length; BlockCount++) 
            {
                for (int k=0;k<6;k++)
                {
                    ck[k] = CollapsBlock[CollapsCount].transform.GetChild(k).gameObject;
                    ckvec[k] = MainCamera.WorldToScreenPoint(ck[k].transform.position);
                    
                    for (int l = 0; l < 6;l++)
                    {
                        bk[l] = Block[BlockCount].transform.GetChild(l).gameObject;   
                        bkvec[l] = MainCamera.WorldToScreenPoint(bk[l].transform.position);       
                        if(Vector2.Distance((Vector2)ckvec[k],(Vector2)bkvec[l])<DefineScript.JUDGE_DISTANCE)
                        {
                            for (int m = 0; m < Block.Length; m++)
                            {
                                if (Vector2.Distance((Vector2)blockposition[BlockCount], (Vector2)blockposition[m]) < 1.0f
                                    && BlockCount!=m)
                                {
                                    if (blockposition[BlockCount].z > blockposition[m].z)
                                    {
                                        IsOveraped = true;
                                        break;
                                    }
                                }
                            }

                            if (IsOveraped==true)
                            {
                                IsOveraped = false;
                                continue;
                            }


                            Block[BlockCount].gameObject.GetComponent<Blocks>().BurnFlg = true;
                            Block[BlockCount].gameObject.GetComponent<Blocks>().SetBurn(Block[BlockCount]);
                        }

                    }
                }
            }
           
        }
        


    }

}
