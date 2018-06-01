﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
    public LoadMainStages StageLoader;                      
    public GameObject NowStageObj;
    
    public Camera MainCamera;
    public GameObject Clear;


    GameObject[] Blocks = new GameObject[DefineScript.NUM_BLOCKS];
    GameObject[] CollapsBlocks = new GameObject[DefineScript.NUM_BLOCKS];
    GameObject[] NormalBlocks = new GameObject[DefineScript.NUM_BLOCKS];
    GameObject[,] Plane = new GameObject[DefineScript.NUM_BLOCKS, 6];

    Vector3[,] PlaneVector = new Vector3[DefineScript.NUM_BLOCKS, 6];
    Vector3[,] NormalPlaneVector = new Vector3[DefineScript.NUM_BLOCKS, 6];
    Vector3[,] CollapsPlaneVector = new Vector3[DefineScript.NUM_BLOCKS, 6];

    Vector3[] CollapsBlockPosition = new Vector3[DefineScript.NUM_BLOCKS];
    Vector3[] NormalBlockPosition = new Vector3[DefineScript.NUM_BLOCKS];
    Vector3[] BlockPosition = new Vector3[DefineScript.NUM_BLOCKS];

    Ray ray;

    public int BlocksCount = 0;
    public int CollapsCount = 0;
    public int NormalCount = 0;
    public int NowStageID = 0;
    public bool IsVisibleBlock = false;
    public bool IsVisibleCollaps = false;
    public bool PlaneCollaps = false;
    public int seigen = 4;
  
    public void Restart()
    {
        for(int i=0;i<Blocks.Length;i++)
        {
            if(Blocks[i].GetComponent<Blocks>().BurnFlg==true)
            {
                Blocks[i].GetComponent<Blocks>().BurnFlg = false;
                Blocks[i].GetComponent<MeshRenderer>().material = LoadResources.Mat_Normal;
                Blocks[i].GetComponent<Blocks>().SetFire.gameObject.SetActive(false);
                Blocks[i].GetComponent<Blocks>().StartFlg();
            }
        }
        Clear.gameObject.SetActive(false);
    }

    void Start()
    {
        Blocks = GameObject.FindGameObjectsWithTag("NormalBlock");
        Sound.LoadBgm("gm_bgm", "GM_Bgm");
        Sound.LoadBgm("gm_burn", "GM_Burn");
        Sound.LoadBgm("gm_burnnow", "GM_BurnNow");
        Sound.LoadSe("se_burn", "GM_Burn");
        Sound.LoadSe("se_burnnow", "GM_BurnNow");
        Sound.PlayBgm("gm_bgm");
        Sound.PlaySe("se_burn", 2);
      
    }

    public void SetStage(int NowStage)
    {   
    }

    void Update()
    {
        for (int i = 0; i < Blocks.Length; i++)
        {
            BlockPosition[i] = MainCamera.WorldToScreenPoint(Blocks[i].transform.position);
            BlocksCount++;
            for (int j = 0; j < 6; j++)
            {
                Plane[i, j] = Blocks[i].transform.GetChild(j).gameObject;
                PlaneVector[i, j] = Plane[i, j].transform.position;
                PlaneVector[i, j] = MainCamera.WorldToScreenPoint(PlaneVector[i, j]);
            }
        }

        if(MoveCamera.ResetFlg ==true)
        {
            Restart();
            MoveCamera.ResetFlg = false;
        }
        if (Atari() == true)
        {
          
        }
        
        Debug.Log(CollapsBlocks[0].GetComponent<Blocks>().CollapsPlain[0]);
        Debug.Log(CollapsBlocks[0].GetComponent<Blocks>().CollapsPlain[1]);
        Debug.Log(CollapsBlocks[0].GetComponent<Blocks>().CollapsPlain[2]);
        Debug.Log(CollapsBlocks[0].GetComponent<Blocks>().CollapsPlain[3]);
        Debug.Log(CollapsBlocks[0].GetComponent<Blocks>().CollapsPlain[4]);
        Debug.Log(CollapsBlocks[0].GetComponent<Blocks>().CollapsPlain[5]);
        
            
    }

    bool atari2(int BlockNow, int CollapsNow, int Blockplain, int CollapsPlain, Vector3[] CollapsVertices)
    {

        if (Vector2.Distance(NormalPlaneVector[BlockNow, Blockplain],
                      CollapsPlaneVector[CollapsNow, CollapsPlain])
                      < DefineScript.JUDGE_DISTANCE)
        {

            if (IsVisibleFromCamera(CollapsPlain, CollapsVertices, ray))
            {
                if (NormalBlockPosition[BlockNow].z > CollapsBlockPosition[CollapsNow].z)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (NormalBlockPosition[BlockNow].z < CollapsBlockPosition[CollapsNow].z)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
        return false;

    }

    bool Atari()
    {
        NormalCount = 0;
        CollapsCount = 0;

        for (int i = 0, j = 0, k = 0; i < Blocks.Length; i++)
        {
            if (Blocks[i].GetComponent<Blocks>().BurnFlg == true)
            {

                CollapsBlocks[j] = Blocks[i];
                CollapsBlockPosition[j] = MainCamera.WorldToScreenPoint(Blocks[i].transform.position);
                for (int l = 0; l < 6; l++)
                {
                    CollapsPlaneVector[j, l] = PlaneVector[i, l];
                }
                j++;
                CollapsCount++;
            }
            if (Blocks[i].GetComponent<Blocks>().BurnFlg == false)
            {
                NormalBlocks[k] = Blocks[i];
                NormalBlockPosition[k] = MainCamera.WorldToScreenPoint(Blocks[i].transform.position);
                for (int l = 0; l < 6; l++)
                {
                    NormalPlaneVector[k, l] = PlaneVector[i, l];
                }
                NormalCount++;
                k++;
            }

        }

        

        ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        for (int CollapsNow = 0; CollapsNow < CollapsCount; CollapsNow++)
        {
            Mesh CollapsMesh = CollapsBlocks[CollapsNow].GetComponent<MeshFilter>().mesh;
            Vector3[] CollapsVertices = CollapsMesh.vertices;

           
            for (int BlockNow = 0; BlockNow < NormalCount; BlockNow++)
            {
                CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsPlain[(int)DefineScript.CollisionIndex.Top] = atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Bottom, (int)DefineScript.CollisionIndex.Top,CollapsVertices);
                CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsPlain[(int)DefineScript.CollisionIndex.Bottom] = atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Top, (int)DefineScript.CollisionIndex.Bottom,CollapsVertices);
                CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsPlain[(int)DefineScript.CollisionIndex.Left] = atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Right, (int)DefineScript.CollisionIndex.Left,CollapsVertices);
                CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsPlain[(int)DefineScript.CollisionIndex.Right] = atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Left, (int)DefineScript.CollisionIndex.Right,CollapsVertices);
                CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsPlain[(int)DefineScript.CollisionIndex.Back] = atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Front, (int)DefineScript.CollisionIndex.Back,CollapsVertices);
                CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsPlain[(int)DefineScript.CollisionIndex.Front] = atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Back, (int)DefineScript.CollisionIndex.Front,CollapsVertices);
                
                if (PlaneCollaps == true)
                {
                    NormalBlocks[BlockNow].GetComponent<Blocks>().BurnFlg = true;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().SetBurn();
                    global::Blocks.nowplayingse = false;
                    
                    PlaneCollaps = false;
                }
                else
                {
                    
                    global::Blocks.nowplayingse = false;
                }

            }


            if (NormalCount == 0)
            {
                Clear.gameObject.SetActive(true);
                return true;
            }
        }
        
        return false;
    }
    public bool IsVisibleFromCamera(int i, Vector3[] vertices, Ray CameraRay)
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

    bool Collaps(Vector3[] NormalPlaneVector, Vector3[] CollapsPlainVector)
    {
        if (Vector2.Distance((Vector2)NormalPlaneVector[(int)DefineScript.CollisionIndex.Top], (Vector2)CollapsPlainVector[(int)DefineScript.CollisionIndex.Bottom]) < DefineScript.JUDGE_DISTANCE)
        {
            return true;
        }
        if (Vector2.Distance((Vector2)NormalPlaneVector[(int)DefineScript.CollisionIndex.Bottom], (Vector2)CollapsPlainVector[(int)DefineScript.CollisionIndex.Top]) < DefineScript.JUDGE_DISTANCE)
        {
            return true;
        }
        if (Vector2.Distance((Vector2)NormalPlaneVector[(int)DefineScript.CollisionIndex.Right], (Vector2)CollapsPlainVector[(int)DefineScript.CollisionIndex.Left]) < DefineScript.JUDGE_DISTANCE)
        {
            return true;
        }
        if (Vector2.Distance((Vector2)NormalPlaneVector[(int)DefineScript.CollisionIndex.Left], (Vector2)CollapsPlainVector[(int)DefineScript.CollisionIndex.Right]) < DefineScript.JUDGE_DISTANCE)
        {
            return true;
        }
        if (Vector2.Distance((Vector2)NormalPlaneVector[(int)DefineScript.CollisionIndex.Front], (Vector2)CollapsPlainVector[(int)DefineScript.CollisionIndex.Back]) < DefineScript.JUDGE_DISTANCE)
        {
            return true;
        }
        if (Vector2.Distance((Vector2)NormalPlaneVector[(int)DefineScript.CollisionIndex.Back], (Vector2)CollapsPlainVector[(int)DefineScript.CollisionIndex.Front]) < DefineScript.JUDGE_DISTANCE)
        {
            return true;
        }
        return false;
    }

    private void OnDestroy()
    {
        Sound.StopBgm();
        Sound.StopSe("se_burn", 2);
    }
}
