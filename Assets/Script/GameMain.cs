using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class GameMain : MonoBehaviour
{

    //以下、ブロックに関する情報
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

<<<<<<< HEAD
=======
    public Blocks[] NBlock = new Blocks[30];
    public Blocks[] CBlock = new Blocks[30];

>>>>>>> Dev
    Ray ray;
    public MoveCamera mvcamera;
    public Camera MainCamera;               //カメラオブジェクト

<<<<<<< HEAD
=======
    public static int ClearLimitBlockNum;
>>>>>>> Dev
    public int BlocksCount = 0;             //現在ブロックの数
    public int CollapsCount = 0;            //現在燃やすブロックの数
    public int NormalCount = 0;             //現在普通ブロックの数
    public int Limit;                       //現在の上限回数
    public int ClearedLimitNum = 0;         //クリアしたときの上限回数
    public int ClearLimit = 0;
    public int FailLimitNum = 0;            //残りの上限回数
<<<<<<< HEAD
=======
    public int NowPitch = 0;
>>>>>>> Dev
    public bool TutorialFlg = true;
    public bool ClearFlg = false;           //ステージクリアフラグ
    public bool FailFlg = false;            //ステージ失敗フラグ
    public bool Collapsing = false;         //現在燃え移り判定が成立しているかどうかのフラグ
    public bool UnsetCollapsing = false;    //上のフラグを解除するときに使うフラグ
<<<<<<< HEAD
    public bool Nowcol = false;
    public bool Burned = false;
    public bool buttonup = false;
    public bool FadeEnd = false;
    public bool PlayedSE = false;
    public bool PlayedSE2 = false;
    public bool NowCol2 = false;
=======
    public bool FadeEnd = false;
    public bool PlayedSE = false;
    public bool PlayedSE2 = false;

    public bool NowCollapsing = false;
    public bool NowCantBurn = false;
    public bool NowCanBurn = false;
>>>>>>> Dev
    public bool TutorialAtari = false;
    public bool limitminus = false;

    float angle90 = 90.0f;
    float angle180 = 180.0f;
    float angle270 = 270.0f;

    public void Restart()
    {
        for (int i = 0; i < Blocks.Length; i++)
        {
            Blocks[i].transform.Find("FlashCubeParticle").GetComponent<flashcube>().oneroot = true;
            if (Blocks[i].GetComponent<Blocks>().BurnFlg == true)
            {
                if (Blocks[i].GetComponent<Blocks>().StartBlockFlg == false)
                {
                    Blocks[i].GetComponent<Blocks>().BurnFlg = false;
<<<<<<< HEAD
=======
                    Blocks[i].GetComponent<Blocks>().UnsetCollapsFlag();

                    Blocks[i].transform.Find("GlassBlock").GetComponent<Emission>().Init();
>>>>>>> Dev
                    Blocks[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f));
                }
            }
            
           
        }
        FailFlg = false;
        ClearFlg = false;
<<<<<<< HEAD
        Limit = ClearedLimitNum = PassStageID.PassUpperCount();


        
=======
        ClearedLimitNum = 0;         //クリアしたときの上限回数
        ClearLimit = 0;
        FailLimitNum = 0;
        Limit = ClearedLimitNum = PassStageID.PassUpperCount();
>>>>>>> Dev
        mvcamera.Position = PassStageID.PassPosition();
        mvcamera.Rotation = PassStageID.PassRotation();
    }


    void Start()
    {
<<<<<<< HEAD
        mvcamera = GameObject.Find("GameObject").GetComponent<MoveCamera>();
        Blocks = GameObject.FindGameObjectsWithTag("NormalBlock");
        Sound.LoadBgm("GM_BGM", "GameMain/GM_Bgm");
        Sound.LoadSe("SE_STAR", "GameMain/GM_Star");
        Sound.LoadSe("SE_CLEAR", "GameMain/GM_Clear");
        Sound.LoadSe("SE_FAIL", "GameMain/GM_Failed");
        Sound.LoadSe("SE_INFO", "GameMain/GM_Information");
=======
       
        mvcamera = GameObject.Find("GameObject").GetComponent<MoveCamera>();
        Blocks = GameObject.FindGameObjectsWithTag("NormalBlock");
        Sound.LoadBgm("GM_BGM", "GameMain/GM_Bgm");
        Sound.LoadSe("SE_STAR1", "GameMain/GM_Star1");
        Sound.LoadSe("SE_STAR2", "GameMain/GM_Star2");
        Sound.LoadSe("SE_STAR3", "GameMain/GM_Star3");
        Sound.LoadSe("SE_CLEAR", "GameMain/GM_Clear");
        Sound.LoadSe("SE_FAIL", "GameMain/GM_Failed");
        Sound.LoadSe("SE_INFO", "GameMain/GM_Information");
        Sound.LoadSe("SE_INFO_CANT", "GameMain/GM_Information_Fail");
>>>>>>> Dev
        Sound.PlayBgm("GM_BGM");
        Sound.SetLoopFlgSe("SE_INFO", true, 4);
        Sound.SetVolumeSe("SE_INFO", 0.5f, 4);
        if (TutorialFlg == false)
        {
            Limit = ClearedLimitNum = PassStageID.PassUpperCount();
        }
        else
        {
            Limit = 5;
        }
<<<<<<< HEAD

=======
        
>>>>>>> Dev
    }


    void Update()
    {
        
        BlocksCount = 0;
        NormalCount = 0;
        CollapsCount = 0;
        UnsetCollapsing = true;
<<<<<<< HEAD
        Nowcol = false;
        NowCol2 = false;
=======
        NowCollapsing = false;
        NowCantBurn = false;
        NowCanBurn = false;
        if(Collapsing==true)
        {
            Controller.InputFlag = true;
        }
>>>>>>> Dev
        for (int i = 0; i < Blocks.Length; i++)
        {
            BlockPosition[i] = MainCamera.WorldToScreenPoint(Blocks[i].transform.position);
            Blocks[i].GetComponent<Blocks>().UnsetCollapsFlag();
            BlocksCount++;
            for (int j = 0; j < 6; j++)
            {
                Plane[i, j] = Blocks[i].transform.GetChild(j).gameObject;
                PlaneVector[i, j] = Plane[i, j].transform.position;
                PlaneVector[i, j] = MainCamera.WorldToScreenPoint(PlaneVector[i, j]);
            }
        }

        for (int i = 0, j = 0, k = 0; i < Blocks.Length; i++)
        {
            if (Blocks[i].GetComponent<Blocks>().BurnFlg == true)
            {
<<<<<<< HEAD
                
=======
                CBlock[j] = Blocks[i].GetComponent<Blocks>();
>>>>>>> Dev
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
<<<<<<< HEAD
=======
                NBlock[k] = Blocks[i].GetComponent<Blocks>();
>>>>>>> Dev
                NormalBlockPosition[k] = MainCamera.WorldToScreenPoint(Blocks[i].transform.position);
                for (int l = 0; l < 6; l++)
                {
                    NormalPlaneVector[k, l] = PlaneVector[i, l];
                }
                NormalCount++;
                k++;
            }

        }

        
        if (Atari() == true)
        {
            MainCamera.GetComponentInParent<MoveCamera>().StopCameraOn();
            if (TutorialFlg == false)
            {
                if (FadeEnd == true)
                {
<<<<<<< HEAD
                    SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
=======
                    if (FailFlg)//失敗時→ステセレ
                    {
                        SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
                    }
                    if (ClearFlg)//成功時→リザルト
                    {
                        ClearLimitBlockNum = ClearLimit;
                        SceneManager.LoadScene("Result", LoadSceneMode.Single);
                    }
>>>>>>> Dev
                }
            }

        }
    }


    bool Atari()
    {
        if (mvcamera.Rotation.y > 360.0f)
            mvcamera.Rotation.y = 0.0f;
        if (mvcamera.Rotation.y < -360.0f)
            mvcamera.Rotation.y = 0.0f;
        mvcamera.StopCameraOff();
        ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        for (int CollapsNow = 0; CollapsNow < CollapsCount; CollapsNow++)
        {
            Mesh CollapsMesh = CollapsBlocks[CollapsNow].GetComponent<MeshFilter>().mesh;
            Vector3[] CollapsVertices = CollapsMesh.vertices;

            for (int BlockNow = 0; BlockNow < NormalCount; BlockNow++)
            {
                if (TutorialAtari == true)
                    continue;
                if(MainCamera.gameObject.GetComponentInParent<MoveCamera>().MoveFlag==false)
                {
<<<<<<< HEAD
                    if (CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsTop == true &&
                    NormalBlocks[BlockNow].GetComponent<Blocks>().CollapsBottom == true)
                    {
                        atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Bottom, (int)DefineScript.CollisionIndex.Top, CollapsVertices);
                    }
                    if (CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsBottom == true &&
                        NormalBlocks[BlockNow].GetComponent<Blocks>().CollapsTop == true)
                    {
                        atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Top, (int)DefineScript.CollisionIndex.Bottom, CollapsVertices);
                    }
                    if (CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsLeft == true &&
                        NormalBlocks[BlockNow].GetComponent<Blocks>().CollapsRight == true)
                    {
                        atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Right, (int)DefineScript.CollisionIndex.Left, CollapsVertices);

                    }
                    if (CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsRight == true &&
                        NormalBlocks[BlockNow].GetComponent<Blocks>().CollapsLeft == true)
                    {
                        atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Left, (int)DefineScript.CollisionIndex.Right, CollapsVertices);

                    }
                    if (CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsBack == true &&
                        NormalBlocks[BlockNow].GetComponent<Blocks>().CollapsFront == true)
                    {
                        atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Front, (int)DefineScript.CollisionIndex.Back, CollapsVertices);
                    }

                    if (CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsFront == true &&
                        NormalBlocks[BlockNow].GetComponent<Blocks>().CollapsBack == true)
                    {
                        atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Back, (int)DefineScript.CollisionIndex.Front, CollapsVertices);
                    }

                    if (NormalBlocks[BlockNow].GetComponent<Blocks>().NormalNowcol == true)
                    {
                        NowCol2 = true;
=======
                    atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Bottom, (int)DefineScript.CollisionIndex.Top, CollapsVertices);
                    atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Top, (int)DefineScript.CollisionIndex.Bottom, CollapsVertices);
                    atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Right, (int)DefineScript.CollisionIndex.Left, CollapsVertices);
                    atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Left, (int)DefineScript.CollisionIndex.Right, CollapsVertices);
                    atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Front, (int)DefineScript.CollisionIndex.Back, CollapsVertices);
                    atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Back, (int)DefineScript.CollisionIndex.Front, CollapsVertices);

                    if (NBlock[BlockNow].NormalNowcol == true)
                    {
                        NowCollapsing = true;
                    }
                    if(NBlock[BlockNow].CanBurn ==true)
                    {
                        NowCanBurn = true;
                        NBlock[BlockNow].CantBurn = false;
>>>>>>> Dev
                    }
                }
            }

        }
<<<<<<< HEAD
        if (NowCol2 == true)
=======
        if (NowCanBurn == true && Collapsing == false)
>>>>>>> Dev
        {
            if (PlayedSE2 == false)
            {
                Sound.PlaySe("SE_INFO", 4);
                PlayedSE2 = true;
            }
        }
        else
        {
            Sound.StopSe("SE_INFO", 4);
            PlayedSE2 = false;
        }

<<<<<<< HEAD
        if (Input.GetButton("AButton"))
        {
            for (int i = 0; i < NormalCount; i++)
            {
                if (NormalBlocks[i].GetComponent<Blocks>().NormalNowcol == true)
                {
                    if (Collapsing == false)
                    {
                        NormalBlocks[i].GetComponent<Blocks>().BurnCnt += DefineScript.JUDGE_BNSPEED_BUTTON;
                    }
                    if (NormalBlocks[i].GetComponent<Blocks>().BurnCnt >= DefineScript.JUDGE_BNTIME )
                    {
                        NormalBlocks[i].GetComponent<Blocks>().canburn = true;

                        Collapsing = true;
                        UnsetCollapsing = false;
                        limitminus = true;
                        NormalBlocks[i].GetComponent<Blocks>().BurnCnt = 0.0f;
                    }
                    Nowcol = true;

                }
                else
                {
                    NormalBlocks[i].GetComponent<Blocks>().canburn = false;
                    NormalBlocks[i].GetComponent<Blocks>().BurnCnt = 0.0f;
                }
            }

        }


=======
        if (Controller.GetButtonDown("AButton")&&!Pause.Restart_flg&&!Pause.is_pause)
        {
            if (NowCanBurn == false)
            {
                Sound.PlaySe("SE_INFO_CANT", 5);
            }
        }
        if(Controller.GetButtonDown("AButton") && !Pause.Restart_flg&&!Pause.is_pause)
        {
            for (int i = 0; i < NormalCount; i++)
            {
                if(NBlock[i].CanBurn==true)
                {
                    Collapsing = true;
                    UnsetCollapsing = false;
                    limitminus = true;
                    NBlock[i].BurnOK = true;
                    Sound.PlaySe("SE_STAR1", 6);
                }
            }
        }
        
>>>>>>> Dev
        if (Collapsing == true)
        {
            mvcamera.StopCameraOn();
            for (int i = 0; i < NormalCount; i++)
            {
<<<<<<< HEAD
                if (NormalBlocks[i].GetComponent<Blocks>().NormalNowcol == true)
                {
                    UnsetCollapsing = false;
                    NormalBlocks[i].GetComponent<Blocks>().BurnCnt2 += DefineScript.JUDGE_BNSPEED_NONBUTTON;
                    if (NormalBlocks[i].GetComponent<Blocks>().BurnCnt2 >= DefineScript.JUDGE_BNTIME)
                    {
                        NormalBlocks[i].GetComponent<Blocks>().canburn = true;
=======
                if (NBlock[i].CanBurn == true)
                {
                    UnsetCollapsing = false;
                    NBlock[i].BurnCnt += DefineScript.JUDGE_BNSPEED_NONBUTTON;
                    if (NBlock[i].BurnCnt >= DefineScript.JUDGE_BNTIME)
                    {
                        NBlock[i].BurnOK = true;
                        
                        switch (NowPitch)
                        {
                            case 0:
                                Sound.PlaySe("SE_STAR2", 7);
                                break;
                            case 1:
                                Sound.PlaySe("SE_STAR3", 8);
                                break;
                            default:
                                Sound.PlaySe("SE_STAR3", 8);
                                break;

                        }
                        NowPitch++;
>>>>>>> Dev
                    }

                }
                else
                {
<<<<<<< HEAD
                    NormalBlocks[i].GetComponent<Blocks>().canburn = false;
                    NormalBlocks[i].GetComponent<Blocks>().BurnCnt = 0.0f;
                    NormalBlocks[i].GetComponent<Blocks>().BurnCnt2 = 0.0f;
=======
                    NBlock[i].BurnOK = false;
                    NBlock[i].BurnCnt = 0.0f;
>>>>>>> Dev
                }
            }

            for (int i = 0; i < NormalCount; i++)
            {
<<<<<<< HEAD
                if (NormalBlocks[i].GetComponent<Blocks>().canburn == true)
                {
                    NormalBlocks[i].GetComponent<Blocks>().BurnFlg = true;
                    NormalBlocks[i].GetComponent<Blocks>().BurnCnt = 0.0f;
                    NormalBlocks[i].GetComponent<Blocks>().BurnCnt2 = 0.0f;
=======
                if (NBlock[i].BurnOK == true)
                {
                    NBlock[i].BurnFlg = true;
                    NBlock[i].BurnCnt = 0.0f;
>>>>>>> Dev
                    
                }
            }
        }

        if (UnsetCollapsing == true)
        {
            Collapsing = false;
<<<<<<< HEAD
            Burned = false;
=======
            NowPitch = 0;
>>>>>>> Dev
        }



        
<<<<<<< HEAD
        if (NowCol2==true && limitminus==true)
=======
        if (NowCollapsing==true && limitminus==true)
>>>>>>> Dev
        {
            Limit--;
            ClearLimit++;
            limitminus = false;
<<<<<<< HEAD
            Sound.PlaySe("SE_STAR", 3);

=======
>>>>>>> Dev
        }
        
        if (NormalCount == 0 && TutorialFlg == false)
        {
            ClearFlg = true;
<<<<<<< HEAD


            ClearedLimitNum = Limit;
            FailLimitNum = PassStageID.PassUpperCount() - Limit;

            return true;
        }
        if (Limit == 0 && NormalCount != 0 &&NowCol2==false && TutorialFlg == false)
=======
            ClearedLimitNum = Limit;
            FailLimitNum = PassStageID.PassUpperCount() - Limit;
            return true;
        }
        if (Limit == 0 && NormalCount != 0 &&NowCollapsing==false && TutorialFlg == false)
>>>>>>> Dev
        {
            FailFlg = true;
            if (PlayedSE == false)
            {
                Sound.PlaySe("SE_FAIL", 1);
                PlayedSE = true;
            }
            return true;
        }
        
        return false;
    }

    void atari2(int BlockNow, int CollapsNow, int Blockplain, int CollapsPlain, Vector3[] CollapsVertices)
    {

       
        float distance = Vector2.Distance((Vector2)CollapsBlockPosition[CollapsNow], (Vector2)NormalBlockPosition[BlockNow]);
        bool temp = false;
        if (Vector2.Distance(NormalPlaneVector[BlockNow, Blockplain],
                      CollapsPlaneVector[CollapsNow, CollapsPlain])
                      < DefineScript.JUDGE_DISTANCE)
        {
            if (IsVisibleFromCamera(CollapsPlain, CollapsVertices, ray))
            {
                
                if (NormalBlockPosition[BlockNow].z < CollapsBlockPosition[CollapsNow].z)
                {
<<<<<<< HEAD
                    CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsNowcol = true;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().NormalNowcol = true;
=======
                    CBlock[CollapsNow].CollapsNowcol = true;
                    NBlock[BlockNow].NormalNowcol = true;
>>>>>>> Dev
                    temp = true;

                }
                else
                {
                    if ((mvcamera.Rotation.x >= -DefineScript.JUDGE_ANGLE && mvcamera.Rotation.x <= DefineScript.JUDGE_ANGLE)||
                        (mvcamera.Rotation.x >= DefineScript.JUDGE_ANGLE2 && mvcamera.Rotation.x <= -DefineScript.JUDGE_ANGLE2))
                    {
                        if(distance >= DefineScript.JUDGE_DISTANCE3 - 10.0f && distance <= DefineScript.JUDGE_DISTANCE3 + 10.0f)
                        {
<<<<<<< HEAD
                                CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsNowcol = true;
                                NormalBlocks[BlockNow].GetComponent<Blocks>().NormalNowcol = true;
                                temp = true;
                        }
                    }

=======
                                CBlock[CollapsNow].CollapsNowcol = true;
                                NBlock[BlockNow].NormalNowcol = true;
                                temp = true;
                        }
                    }
                    
>>>>>>> Dev
                    if ((mvcamera.Rotation.y >= -2.0f && mvcamera.Rotation.y <= 2.0f) ||
                        (mvcamera.Rotation.y >= angle90 - 2.0f && mvcamera.Rotation.y <= angle90 + 2.0f) ||
                        (mvcamera.Rotation.y >= angle180 - 2.0f && mvcamera.Rotation.y <= angle180 + 2.0f) ||
                        (mvcamera.Rotation.y >= angle270 - 2.0f && mvcamera.Rotation.y <= angle270 + 2.0f) ||
                        (mvcamera.Rotation.y >= -angle90 - 2.0f && mvcamera.Rotation.y <= -angle90 + 2.0f) ||
                        (mvcamera.Rotation.y >= -angle180 - 2.0f && mvcamera.Rotation.y <= -angle180 + 2.0f) ||
                        (mvcamera.Rotation.y >= -angle270 - 2.0f && mvcamera.Rotation.y <= -angle270 + 2.0f) ||
                        mvcamera.Rotation.y >= 358.0f ||
                        mvcamera.Rotation.y <= -358.0f)
                    {
<<<<<<< HEAD
                        if (distance >= DefineScript.JUDGE_DISTANCE3 - 10.0f && distance <= DefineScript.JUDGE_DISTANCE3 + 10.0f)
                        {
                            CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsNowcol = true;
                            NormalBlocks[BlockNow].GetComponent<Blocks>().NormalNowcol = true;
=======
                        if (distance >= DefineScript.JUDGE_DISTANCE3 - 1.0f && distance <= DefineScript.JUDGE_DISTANCE3 + 1.0f)
                        {
                            CBlock[CollapsNow].CollapsNowcol = true;
                            NBlock[BlockNow].NormalNowcol = true;
>>>>>>> Dev
                            temp = true;
                        }
                    }


                }
            }
            else
            {
                if (NormalBlockPosition[BlockNow].z > CollapsBlockPosition[CollapsNow].z)
                {
<<<<<<< HEAD
                    CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsNowcol = true;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().NormalNowcol = true;
=======
                    CBlock[CollapsNow].CollapsNowcol = true;
                    NBlock[BlockNow].NormalNowcol = true;
>>>>>>> Dev
                    temp = true;
                }
                else
                {
                    if ((mvcamera.Rotation.x >= -DefineScript.JUDGE_ANGLE && mvcamera.Rotation.x <= DefineScript.JUDGE_ANGLE) ||
                        (mvcamera.Rotation.x >= DefineScript.JUDGE_ANGLE2 && mvcamera.Rotation.x <= -DefineScript.JUDGE_ANGLE2))
                    {
                        if (distance >= DefineScript.JUDGE_DISTANCE3 - 10.0f && distance <= DefineScript.JUDGE_DISTANCE3 + 10.0f)
                        {
<<<<<<< HEAD
                            CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsNowcol = true;
                            NormalBlocks[BlockNow].GetComponent<Blocks>().NormalNowcol = true;
=======
                            CBlock[CollapsNow].CollapsNowcol = true;
                            NBlock[BlockNow].NormalNowcol = true;
>>>>>>> Dev
                            temp = true;
                        }
                    }

                    if ((mvcamera.Rotation.y >= -2.0f && mvcamera.Rotation.y <= 2.0f) ||
                        (mvcamera.Rotation.y >= angle90 - 2.0f && mvcamera.Rotation.y <= angle90 + 2.0f) ||
                        (mvcamera.Rotation.y >= angle180 - 2.0f && mvcamera.Rotation.y <= angle180 + 2.0f) ||
                        (mvcamera.Rotation.y >= angle270 - 2.0f && mvcamera.Rotation.y <= angle270 + 2.0f) ||
                        (mvcamera.Rotation.y >= -angle90 - 2.0f && mvcamera.Rotation.y <= -angle90 + 2.0f) ||
                        (mvcamera.Rotation.y >= -angle180 - 2.0f && mvcamera.Rotation.y <= -angle180 + 2.0f) ||
                        (mvcamera.Rotation.y >= -angle270 - 2.0f && mvcamera.Rotation.y <= -angle270 + 2.0f) ||
                        mvcamera.Rotation.y >= 358.0f ||
                        mvcamera.Rotation.y <= -358.0f)
                    {
<<<<<<< HEAD
                        if (distance >= DefineScript.JUDGE_DISTANCE3 - 10.0f && distance <= DefineScript.JUDGE_DISTANCE3 + 10.0f)
                        {
                            CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsNowcol = true;
                            NormalBlocks[BlockNow].GetComponent<Blocks>().NormalNowcol = true;
=======
                        if (distance >= DefineScript.JUDGE_DISTANCE3 - 1.0f && distance <= DefineScript.JUDGE_DISTANCE3 + 1.0f)
                        {
                            CBlock[CollapsNow].CollapsNowcol = true;
                            NBlock[BlockNow].NormalNowcol = true;
>>>>>>> Dev
                            temp = true;
                        }
                    }
                }
            }

        }

        if (temp == true)
        {
            switch (CollapsPlain)
            {
                case (int)DefineScript.CollisionIndex.Top:
<<<<<<< HEAD
                    CollapsBlocks[CollapsNow].GetComponent<Blocks>().IsTopCollapsed = true;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().IsBottomCollapsed = true;
                    break;
                case (int)DefineScript.CollisionIndex.Bottom:
                    CollapsBlocks[CollapsNow].GetComponent<Blocks>().IsBottomCollapsed = true;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().IsTopCollapsed = true;
                    break;
                case (int)DefineScript.CollisionIndex.Left:
                    CollapsBlocks[CollapsNow].GetComponent<Blocks>().IsLeftCollapsed = true;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().IsRightCollapsed = true;
                    break;
                case (int)DefineScript.CollisionIndex.Right:
                    CollapsBlocks[CollapsNow].GetComponent<Blocks>().IsRightCollapsed = true;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().IsLeftCollapsed = true;
                    break;
                case (int)DefineScript.CollisionIndex.Front:
                    CollapsBlocks[CollapsNow].GetComponent<Blocks>().IsFrontCollapsed = true;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().IsBackCollapsed = true;
                    break;
                case (int)DefineScript.CollisionIndex.Back:
                    CollapsBlocks[CollapsNow].GetComponent<Blocks>().IsBackCollapsed = true;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().IsFrontCollapsed = true;
                    break;

=======
                    if(CBlock[CollapsNow].CollapsTop == true && 
                       NBlock[BlockNow].CollapsBottom == true)
                    {
                        CBlock[CollapsNow].IsTopCollapsed = true;
                        NBlock[BlockNow].IsBottomCollapsed = true;
                        NBlock[BlockNow].CanBurn  = true;
                    }
                    else
                    {
                        NBlock[BlockNow].CantBurn = true;
                        NowCantBurn = true;
                    }
                    break;
                case (int)DefineScript.CollisionIndex.Bottom:
                    if (CBlock[CollapsNow].CollapsBottom == true &&
                      NBlock[BlockNow].CollapsTop == true)
                    {
                        CBlock[CollapsNow].IsBottomCollapsed = true;
                        NBlock[BlockNow].IsTopCollapsed = true;
                        NBlock[BlockNow].CanBurn = true;

                    }
                    else
                    {
                        NBlock[BlockNow].CantBurn = true;
                        NowCantBurn = true;
                    }
                    break;
                case (int)DefineScript.CollisionIndex.Left:
                    if (CBlock[CollapsNow].CollapsLeft == true &&
                       NBlock[BlockNow].CollapsRight == true)
                    {
                        CBlock[CollapsNow].IsLeftCollapsed = true;
                        NBlock[BlockNow].IsRightCollapsed = true;
                        NBlock[BlockNow].CanBurn = true;

                    }
                    else
                    {
                        NBlock[BlockNow].CantBurn = true;
                        NowCantBurn = true;
                    }

                    break;
                case (int)DefineScript.CollisionIndex.Right:
                    if (CBlock[CollapsNow].CollapsRight == true &&
                       NBlock[BlockNow].CollapsLeft == true)
                    {
                        CBlock[CollapsNow].IsRightCollapsed = true;
                        NBlock[BlockNow].IsLeftCollapsed = true;
                        NBlock[BlockNow].CanBurn = true;

                    }
                    else
                    {
                        NBlock[BlockNow].CantBurn = true;
                        NowCantBurn = true;
                    }
                    break;
                case (int)DefineScript.CollisionIndex.Front:
                    if (CBlock[CollapsNow].CollapsFront == true &&
                      NBlock[BlockNow].CollapsBack == true)
                    {
                        CBlock[CollapsNow].IsFrontCollapsed = true;
                        NBlock[BlockNow].IsBackCollapsed = true;
                        NBlock[BlockNow].CanBurn = true;

                    }
                    else
                    {
                        NBlock[BlockNow].CantBurn = true;
                        NowCantBurn = true;
                    }
                    break;
                case (int)DefineScript.CollisionIndex.Back:
                    if (CBlock[CollapsNow].CollapsBack == true &&
                      NBlock[BlockNow].CollapsFront == true)
                    {
                        CBlock[CollapsNow].IsBackCollapsed = true;
                        NBlock[BlockNow].IsFrontCollapsed = true;
                        NBlock[BlockNow].CanBurn = true;

                    }
                    else
                    {
                        NBlock[BlockNow].CantBurn = true;
                        NowCantBurn = true;
                    }
                    break;
>>>>>>> Dev
            }
        }


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

    public void SetBlock()
    {
        Blocks = GameObject.FindGameObjectsWithTag("NormalBlock");
    }


}
