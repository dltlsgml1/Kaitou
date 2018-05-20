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
        Mesh CollapsMesh = CollapsBlock[0].GetComponent<MeshFilter>().mesh;
        Mesh BlockMesh = Block[0].GetComponent<MeshFilter>().mesh;

        Vector3[] CollapsVertices = CollapsMesh.vertices;
        Vector3[] BlockVertices = BlockMesh.vertices;


        for(int i=0;i<8;i++)
        {
            CollapsVertices[i] = MainCamera.WorldToScreenPoint(CollapsVertices[i]);
            BlockVertices[i] = MainCamera.WorldToScreenPoint(BlockVertices[i]);

        }

        if(
            Vector3.Distance(CollapsVertices[3],BlockVertices[2])<15.0f &&
            Vector3.Distance(CollapsVertices[5],BlockVertices[4])<15.0f &&
            Vector3.Distance(CollapsVertices[7],BlockVertices[6])<15.0f &&
            Vector3.Distance(CollapsVertices[1],BlockVertices[0])<15.0f 
            )
        {
            Block[0].gameObject.GetComponent<Blocks>().BurnFlg = true;
            Block[0].gameObject.GetComponent<Blocks>().SetBurn(Block[0]);

            CollapsBlock[0].gameObject.GetComponent<CollapseBlock>().BurnFlg = true;
            CollapsBlock[0].gameObject.GetComponent<CollapseBlock>().SetBurn(CollapsBlock[0]);
        }
        if (
            Vector3.Distance(CollapsVertices[2], BlockVertices[3]) < 15.0f &&
            Vector3.Distance(CollapsVertices[4], BlockVertices[5]) < 15.0f &&
            Vector3.Distance(CollapsVertices[6], BlockVertices[7]) < 15.0f &&
            Vector3.Distance(CollapsVertices[0], BlockVertices[1]) < 15.0f
            )
        {
            Block[0].gameObject.GetComponent<Blocks>().BurnFlg = true;
            Block[0].gameObject.GetComponent<Blocks>().SetBurn(Block[0]);

            CollapsBlock[0].gameObject.GetComponent<CollapseBlock>().BurnFlg = true;
            CollapsBlock[0].gameObject.GetComponent<CollapseBlock>().SetBurn(CollapsBlock[0]);
        }

        //if (
        //   Vector3.Distance(CollapsVertices[3], BlockVertices[5]) < 1.0f &&
        //   Vector3.Distance(CollapsVertices[2], BlockVertices[4]) < 1.0f &&
        //   Vector3.Distance(CollapsVertices[1], BlockVertices[7]) < 1.0f &&
        //   Vector3.Distance(CollapsVertices[0], BlockVertices[6]) < 1.0f
        //   )
        //{
        //    Block[0].gameObject.GetComponent<Blocks>().BurnFlg = true;
        //    Block[0].gameObject.GetComponent<Blocks>().SetBurn(Block[0]);

        //    CollapsBlock[0].gameObject.GetComponent<CollapseBlock>().BurnFlg = true;
        //    CollapsBlock[0].gameObject.GetComponent<CollapseBlock>().SetBurn(CollapsBlock[0]);


        //}
        //if (
        //   Vector3.Distance(CollapsVertices[3], BlockVertices[1]) < 1.0f &&
        //   Vector3.Distance(CollapsVertices[2], BlockVertices[0]) < 1.0f &&
        //   Vector3.Distance(CollapsVertices[5], BlockVertices[7]) < 1.0f &&
        //   Vector3.Distance(CollapsVertices[4], BlockVertices[6]) < 1.0f
        //   )
        //{
        //    Block[0].gameObject.GetComponent<Blocks>().BurnFlg = true;
        //    Block[0].gameObject.GetComponent<Blocks>().SetBurn(Block[0]);

        //    CollapsBlock[0].gameObject.GetComponent<CollapseBlock>().BurnFlg = true;
        //    CollapsBlock[0].gameObject.GetComponent<CollapseBlock>().SetBurn(CollapsBlock[0]);


        //}

        Debug.Log(Vector3.Distance(CollapsVertices[4], BlockVertices[4]));
        Debug.Log(CollapsVertices[4]);
        Debug.Log(BlockVertices[4]);
        Debug.Log(CollapsBlock[0].transform.position);
        Debug.Log(Block[0].transform.position);
        Debug.Log(MainCamera.WorldToScreenPoint(CollapsBlock[0].transform.position));
        Debug.Log(MainCamera.WorldToScreenPoint(Block[0].transform.position));

    }

}
