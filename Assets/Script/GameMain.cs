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
        bool z = false;
       
        Vector3[] bkvec = new Vector3[6];
        Vector3[] ckvec = new Vector3[6];
        Vector2[] bkvec2 = new Vector2[6];
        Vector2[] ckvec2 = new Vector2[6];
        Vector2 v1;
        Vector2 v2;

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
        v1 = (Vector2)blockposition[0];
        v2 = (Vector2)collapsblockposition[0];
        Debug.Log(Vector3.Distance(blockposition[0], collapsblockposition[0]));
        Debug.Log(Vector2.Distance(v1, v2));

        for (int CollapsCount = 0; CollapsCount < CollapsBlock.Length; CollapsCount++) 
        {
            for (int BlockCount = 0; BlockCount < Block.Length; BlockCount++) 
            {
                for (int k=0;k<6;k++)
                {
                    ck[k] = CollapsBlock[CollapsCount].transform.GetChild(k).gameObject;
                    ckvec[k] = MainCamera.WorldToScreenPoint(ck[k].transform.position);
                    ckvec2[k] = (Vector2)ckvec[k];
                    for (int l = 0; l < 6;l++)
                    {
                        bk[l] = Block[BlockCount].transform.GetChild(l).gameObject;   
                        bkvec[l] = MainCamera.WorldToScreenPoint(bk[l].transform.position);
                        bkvec2[l] = (Vector2)bkvec2[l];
                        
                        if(Vector3.Distance(ckvec[k],bkvec[l])<DefineScript.JUDGE_DISTANCE)
                        {
        
                            Block[BlockCount].gameObject.GetComponent<Blocks>().BurnFlg = true;
                            Block[BlockCount].gameObject.GetComponent<Blocks>().SetBurn(Block[BlockCount]);
                        }

                    }
                }
            }
           
        }
        


    }

}
//for (int m = 0; m < Block.Length; m++)
//{
//    if (bkvec[l].z >= bkvec[m].z &&
//       Vector3.Distance(blockposition[BlockCount],blockposition[l]) < 1.0f 
//       )
//    {
//        z = true;
//        break;
//    }
//}
//if (z == true)
//{
//    break;
//}