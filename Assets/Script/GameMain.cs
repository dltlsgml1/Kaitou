﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
    public LoadMainStages StageLoader;
    public GameObject NowStageObj;
    public GameObject[] Block = new GameObject[10];
    public Camera MainCamera;
    public GameObject Clear;

    Vector3 side1;
    Vector3 side2;
    GameObject[] CollapsBocks = new GameObject[10];
    GameObject[] NormalBlocks = new GameObject[10];
    GameObject[,] Plane = new GameObject[10, 6];
    GameObject[,] CollapsPlane = new GameObject[10, 6];
    GameObject[,] NormalPlain = new GameObject[10, 6];

    Vector3[,] PlaneVector = new Vector3[10, 6];
    Vector3[,] NormalPlaneVector = new Vector3[10, 6];
    Vector3[,] CollapsPlaneVector = new Vector3[10, 6];

    Vector3[] CollapsBlockPosition = new Vector3[10];
    Vector3[] NormalBlockPosition = new Vector3[10];
    Vector3[] BlockPosition = new Vector3[10];

    public int BlocksCount = 0;
    public int CollapsCount = 0;
    public int NormalCount = 0;
    public int NowStage = 0;
    public bool IsVisibleBlock = false;
    public bool IsVisibleCollaps = false;
    public bool PlaneCollaps = false;
  
    public void Restart()
    {
        for(int i=0;i<Block.Length;i++)
        {
            if(Block[i].GetComponent<Blocks>().BurnFlg==true)
            {
                Block[i].GetComponent<Blocks>().BurnFlg = false;
                Block[i].GetComponent<MeshRenderer>().material = Blocks.Mat_Normal;
                Block[i].GetComponent<Blocks>().SetFire.gameObject.SetActive(false);
                Block[i].GetComponent<Blocks>().StartFlg();
            }
           
        }
        Clear.gameObject.SetActive(false);
    }

    void Start()
    {
        StageLoader = gameObject.AddComponent<LoadMainStages>();
        StageLoader.LoadStage();
        SetStage(NowStage);
        
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
        GameObject TempStage;
        TempStage = StageLoader.GetStage(NowStage);
        NowStageObj = TempStage;
        Block = GameObject.FindGameObjectsWithTag("NormalBlock");
    }

    void Update()
    {
;

        for (int i = 0; i < Block.Length; i++)
        {
            BlockPosition[i] = MainCamera.WorldToScreenPoint(Block[i].transform.position);

                BlocksCount++;

            for (int j = 0; j < 6; j++)
            {
                Plane[i, j] = Block[i].transform.GetChild(j).gameObject;
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
    }

    bool Atari()
    {
        NormalCount = 0;
        CollapsCount = 0;

        for (int i = 0, j = 0, k = 0; i < Block.Length; i++)
        {
            if (Block[i].GetComponent<Blocks>().BurnFlg == true)
            {

                CollapsBocks[j] = Block[i];
                CollapsBlockPosition[j] = MainCamera.WorldToScreenPoint(Block[i].transform.position);
                for (int l = 0; l < 6; l++)
                {
                    CollapsPlaneVector[j, l] = PlaneVector[i, l];
                }
                j++;
                CollapsCount++;
            }
            if (Block[i].GetComponent<Blocks>().BurnFlg == false)
            {
                NormalBlocks[k] = Block[i];
                NormalBlockPosition[k] = MainCamera.WorldToScreenPoint(Block[i].transform.position);
                for (int l = 0; l < 6; l++)
                {
                    NormalPlaneVector[k, l] = PlaneVector[i, l];
                }
                NormalCount++;
                k++;
            }

        }


        Ray ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        for (int CollapsNow = 0; CollapsNow < CollapsCount; CollapsNow++)
        {
            Mesh CollapsMesh = CollapsBocks[CollapsNow].GetComponent<MeshFilter>().mesh;
            Vector3[] CollapsVertices = CollapsMesh.vertices;
            for (int BlockNow = 0; BlockNow < NormalCount; BlockNow++)
            {
                if (Vector2.Distance(NormalPlaneVector[BlockNow, (int)DefineScript.CollisionIndex.Top],
                    CollapsPlaneVector[CollapsNow, (int)DefineScript.CollisionIndex.Bottom])
                    < DefineScript.JUDGE_DISTANCE)
                {

                    if (IsVisibleFromCamera((int)DefineScript.CollisionIndex.Bottom, CollapsVertices, ray))
                    {
                        if (NormalBlockPosition[BlockNow].z > CollapsBlockPosition[CollapsNow].z)
                        {
                            PlaneCollaps = false;
                        }
                        else
                        {
                            PlaneCollaps = NormalBlocks[BlockNow].GetComponent<Blocks>().Burning();
                        }
                    }
                    else
                    {
                        if (NormalBlockPosition[BlockNow].z < CollapsBlockPosition[CollapsNow].z)
                        {
                            PlaneCollaps = false;
                        }
                        else
                        {
                            PlaneCollaps = NormalBlocks[BlockNow].GetComponent<Blocks>().Burning();
                        }
                    }

                }
                if (Vector2.Distance(NormalPlaneVector[BlockNow, (int)DefineScript.CollisionIndex.Bottom],
                    CollapsPlaneVector[CollapsNow, (int)DefineScript.CollisionIndex.Top])
                    < DefineScript.JUDGE_DISTANCE)
                {

                    if (IsVisibleFromCamera((int)DefineScript.CollisionIndex.Top, CollapsVertices, ray))
                    {
                        if (NormalBlockPosition[BlockNow].z > CollapsBlockPosition[CollapsNow].z)
                        {
                            PlaneCollaps = false;
                        }
                        else
                        {
                            PlaneCollaps = NormalBlocks[BlockNow].GetComponent<Blocks>().Burning();
                        }
                    }
                    else
                    {
                        if (NormalBlockPosition[BlockNow].z < CollapsBlockPosition[CollapsNow].z)
                        {
                            PlaneCollaps = false;
                        }
                        else
                        {
                            PlaneCollaps = NormalBlocks[BlockNow].GetComponent<Blocks>().Burning();
                        }
                    }

                }
                if (Vector2.Distance(NormalPlaneVector[BlockNow, (int)DefineScript.CollisionIndex.Left],
                   CollapsPlaneVector[CollapsNow, (int)DefineScript.CollisionIndex.Right])
                   < DefineScript.JUDGE_DISTANCE)
                {

                    if (IsVisibleFromCamera((int)DefineScript.CollisionIndex.Right, CollapsVertices, ray))
                    {
                        if (NormalBlockPosition[BlockNow].z > CollapsBlockPosition[CollapsNow].z)
                        {
                            PlaneCollaps = false;
                        }
                        else
                        {
                            PlaneCollaps = NormalBlocks[BlockNow].GetComponent<Blocks>().Burning();
                        }
                    }
                    else
                    {
                        if (NormalBlockPosition[BlockNow].z < CollapsBlockPosition[CollapsNow].z)
                        {
                            PlaneCollaps = false;
                        }
                        else
                        {
                            PlaneCollaps = NormalBlocks[BlockNow].GetComponent<Blocks>().Burning();
                        }
                    }

                }
                if (Vector2.Distance(NormalPlaneVector[BlockNow, (int)DefineScript.CollisionIndex.Right],
                   CollapsPlaneVector[CollapsNow, (int)DefineScript.CollisionIndex.Left])
                   < DefineScript.JUDGE_DISTANCE)
                {

                    if (IsVisibleFromCamera((int)DefineScript.CollisionIndex.Left, CollapsVertices, ray))
                    {
                        if (NormalBlockPosition[BlockNow].z > CollapsBlockPosition[CollapsNow].z)
                        {
                            PlaneCollaps = false;
                        }
                        else
                        {
                            PlaneCollaps = NormalBlocks[BlockNow].GetComponent<Blocks>().Burning();
                        }
                    }
                    else
                    {
                        if (NormalBlockPosition[BlockNow].z < CollapsBlockPosition[CollapsNow].z)
                        {
                            PlaneCollaps = false;
                        }
                        else
                        {
                            PlaneCollaps = NormalBlocks[BlockNow].GetComponent<Blocks>().Burning();
                        }
                    }
                }
                if (Vector2.Distance(NormalPlaneVector[BlockNow, (int)DefineScript.CollisionIndex.Front],
                   CollapsPlaneVector[CollapsNow, (int)DefineScript.CollisionIndex.Back])
                   < DefineScript.JUDGE_DISTANCE)
                {

                    if (IsVisibleFromCamera((int)DefineScript.CollisionIndex.Back, CollapsVertices, ray))
                    {
                        if (NormalBlockPosition[BlockNow].z > CollapsBlockPosition[CollapsNow].z)
                        {
                            PlaneCollaps = false;
                        }
                        else
                        {
                            PlaneCollaps = NormalBlocks[BlockNow].GetComponent<Blocks>().Burning();
                        }
                    }
                    else
                    {
                        if (NormalBlockPosition[BlockNow].z < CollapsBlockPosition[CollapsNow].z)
                        {
                            PlaneCollaps = false;
                        }
                        else
                        {
                            PlaneCollaps = NormalBlocks[BlockNow].GetComponent<Blocks>().Burning();
                        }
                    }
                }
                if (Vector2.Distance(NormalPlaneVector[BlockNow, (int)DefineScript.CollisionIndex.Back],
                    CollapsPlaneVector[CollapsNow, (int)DefineScript.CollisionIndex.Front])
                    < DefineScript.JUDGE_DISTANCE)
                {

                    if (IsVisibleFromCamera((int)DefineScript.CollisionIndex.Front, CollapsVertices, ray))
                    {
                        if (NormalBlockPosition[BlockNow].z > CollapsBlockPosition[CollapsNow].z)
                        {
                            PlaneCollaps = false;
                        }
                        else
                        {
                            PlaneCollaps = Block[BlockNow].GetComponent<Blocks>().Burning();
                        }
                    }
                    else
                    {
                        if (NormalBlockPosition[BlockNow].z < CollapsBlockPosition[CollapsNow].z)
                        {
                            PlaneCollaps = false;
                        }
                        else
                        {
                            PlaneCollaps = Block[BlockNow].GetComponent<Blocks>().Burning();
                        }
                    }
                }



                if (PlaneCollaps == true)
                {
                    NormalBlocks[BlockNow].GetComponent<Blocks>().BurnFlg = true;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().SetBurn(NormalBlocks[BlockNow]);
                    Blocks.nowplayingse = false;

                    PlaneCollaps = false;
                }
                else
                {
                    Blocks.nowplayingse = false;
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
