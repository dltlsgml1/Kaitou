using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour {
    public LoadMainStages StageLoader;
    public GameObject NowStageObj;
    public GameObject[] Block = new GameObject[10];
    public Camera MainCamera;
    Vector3 side1;
    Vector3 side2;
    GameObject[] CollapsBocks = new GameObject[10];
    GameObject[,] CollapsPlane = new GameObject[10,6];


    public int CollapsBlockCount = 0;
    public int BlocksCount = 0;
    public int CollapsCount = 0;
    public int NowStage = 0;
    public bool IsVisibleBlock = false;
    public bool IsVisibleCollaps = false;

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
        

        for(int i=0;i<Block.Length;i++)
        {
            if (Block[i].GetComponent<Blocks>().BurnFlg == false)
            {
                BlocksCount++;
            }
            for(int j=0;j<6;j++)
            {
                CollapsPlane[i, j] = Block[i].transform.GetChild(j).gameObject;
            }
            
        }
      
    }

    
    
	void Update ()
    {
        if (Atari() == true)
        {
            Debug.Log("Clear");
        }
    }

    bool Atari()
    {
        for(int i=0,j=0;i<Block.Length;i++)
        {
            if (Block[i].GetComponent<Blocks>().BurnFlg == true &&
                Block[i].GetComponent<Blocks>().BurnChecked == false)
            {
                Block[i].GetComponent<Blocks>().BurnChecked = true;
                CollapsBocks[j] = Block[i];
                j++;
                CollapsCount++;
            }
        }
        


        bool IsOveraped=false;
        GameObject[] bk = new GameObject[10];
        Vector3[] bkvec = new Vector3[6];
        Vector3[] ckvec = new Vector3[6];
        Vector3[] blockposition = new Vector3[Block.Length];
        for(int i=0;i<Block.Length;i++)
        {
            blockposition[i] = MainCamera.WorldToScreenPoint(Block[i].transform.position);    
        }
       
        Ray ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

       for(int CollapsCount = 0; )
            for (int BlockCount = 0; BlockCount < Block.Length; BlockCount++) 
            {
                Mesh BlockMesh = Block[BlockCount].GetComponent<MeshFilter>().mesh;
                Vector3[] BlockVertices = BlockMesh.vertices;  
                for (int k=0;k<6;k++)
                {
                    for (int l = 0; l < 6;l++)
                    {
                        bk[l] = Block[BlockCount].transform.GetChild(l).gameObject;   
                        bkvec[l] = MainCamera.WorldToScreenPoint(bk[l].transform.position); 
                        IsVisibleBlock = IsVisibleFromCamera(l, BlockVertices, ray);
                       

                        if (Vector2.Distance((Vector2)ckvec[k],(Vector2)bkvec[l])<DefineScript.JUDGE_DISTANCE)
                        {
                           

                            //for (int m = 0; m < Block.Length; m++)
                            //{

                            //    if (Vector2.Distance((Vector2)blockposition[BlockCount], (Vector2)blockposition[m]) < 1.0f
                            //        && BlockCount!=m)
                            //    {
                            //        if (blockposition[BlockCount].z > blockposition[m].z)
                            //        {
                            //            IsOveraped = true;
                            //            break;
                            //        }
                            //    }
                            //}
                            //if (IsOveraped==true)
                            //{
                            //    IsOveraped = false;
                            //    continue;
                            //}
                            if (Block[BlockCount].gameObject.GetComponent<Blocks>().BurnFlg == false) 
                            {
                                BlocksCount--;
                            }
                            Block[BlockCount].gameObject.GetComponent<Blocks>().BurnFlg = true;
                            Block[BlockCount].gameObject.GetComponent<Blocks>().SetBurn(Block[BlockCount]);
                          
                        }

                    }
                }
            }
           
        
        

        if(BlocksCount==0)
        {
            return true;
        }
        return false;
    }
    public bool IsVisibleFromCamera(int i,Vector3[] vertices,Ray CameraRay)
    {
        Vector3 side1 = new Vector3(0, 0, 0);
        Vector3 side2 = new Vector3(0, 0, 0);
        Vector3 normal = new Vector3(0, 0, 0);
        switch (i)
        {
            case (int)DefineScript.CollisionIndex.Top:
                side1 = vertices[2] - vertices[3];
                side2 = vertices[5] - vertices[3];
                break;
            case (int)DefineScript.CollisionIndex.Left:
                side1 = vertices[3] - vertices[1];
                side2 = vertices[5] - vertices[1];

                break;
            case (int)DefineScript.CollisionIndex.Back:
                 side1 = vertices[3] - vertices[2];
                 side2 = vertices[1] - vertices[2];
                break;
            case (int)DefineScript.CollisionIndex.Bottom:
                side1 = vertices[7] - vertices[1];
                side2 = vertices[6] - vertices[1];
                
                break;
            case (int)DefineScript.CollisionIndex.Front:
                side1 = vertices[5] - vertices[7];
                side2 = vertices[4] - vertices[7];
                
                break;
            case (int)DefineScript.CollisionIndex.Right:
                side1 = vertices[4] - vertices[0];
                side2 = vertices[2] - vertices[0];
              
                break;
        }


        
        Vector3 side3 = CameraRay.direction - CameraRay.origin;

        normal = Vector3.Cross(side1, side2);
        normal = Vector3.Normalize(normal);
        float dot = Vector3.Dot(normal, side3);
        if (dot < 0.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
