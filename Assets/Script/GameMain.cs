using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    Ray ray;


    public Camera MainCamera;               //カメラオブジェクト
    public GameObject Clear;                //クリアのときのオブジェクト
    public GameObject Fail;                 //失敗のときのオブジェクト


    public int BlocksCount = 0;             //現在ブロックの数
    public int CollapsCount = 0;            //現在燃やすブロックの数
    public int OldCollapsCount = 0;         //上限回数をへらすときに使う
    public int NormalCount = 0;             //現在普通ブロックの数
    public int Limit;                       //現在の上限回数
    public int ClearedLimitNum = 0;         //クリアしたときの上限回数
    public int ClearLimit = 0;
    public int FailLimitNum = 0;            //残りの上限回数
    public bool TutorialFlg = false;
    public bool ClearFlg = false;           //ステージクリアフラグ
    public bool FailFlg = false;            //ステージ失敗フラグ
    public bool Collapsing = false;         //現在燃え移り判定が成立しているかどうかのフラグ
    public bool UnsetCollapsing = false;    //上のフラグを解除するときに使うフラグ
    public bool Nowcol = false;
    public bool Burned = false;
    public bool buttonup = false;

    public void Restart()
    {
        for(int i=0;i<Blocks.Length;i++)
        {
            if(Blocks[i].GetComponent<Blocks>().BurnFlg==true)
            {
                Blocks[i].GetComponent<Blocks>().BurnFlg = false;
                Blocks[i].GetComponent<MeshRenderer>().material = LoadResources.Mat_Normal;

                Blocks[i].GetComponent<Blocks>().StartFlg();


            }
        }
        Clear.gameObject.SetActive(false);
        Fail.gameObject.SetActive(false);
        FailFlg = false;
        ClearFlg = false;
        Limit  = ClearedLimitNum = PassStageID.PassUpperCount();
        

        MoveCamera mvcamera = GameObject.Find("GameObject").GetComponent<MoveCamera>();
        mvcamera.Position = PassStageID.PassPosition();
        mvcamera.Rotation = PassStageID.PassRotation();
    }
    

    void Start()
    {
        Blocks = GameObject.FindGameObjectsWithTag("NormalBlock");
       
        Sound.LoadBgm("gm_bgm", "GM_Bgm");
        Sound.LoadBgm("gm_burn", "GM_Burn");
        Sound.LoadBgm("gm_burnnow", "GM_BurnNow");
        Sound.LoadSe("se_burn", "GM_Burn");
        Sound.LoadSe("se_burnnow", "GM_BurnNow");
        if(TutorialFlg==false)
        {
            Sound.PlayBgm("gm_bgm");
            Sound.PlaySe("se_burn", 2);
            Clear.gameObject.SetActive(false);
            Fail.gameObject.SetActive(false);
            Limit = ClearedLimitNum = PassStageID.PassUpperCount();
        }
        else
        {
            Limit = 1;
        }
    }

    void Update()
    {
        BlocksCount = 0;
        NormalCount = 0;
        CollapsCount = 0;
        UnsetCollapsing = true;
        Nowcol = false;

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
        

        if (MoveCamera.ResetFlg ==true)
        {
            Restart();
            MoveCamera.ResetFlg = false;
        }
        
        if (Atari() == true)
        {
            if (TutorialFlg == false)
            {
                if (Input.GetButtonDown("AButton") || Input.GetButtonDown("BButton"))
                {
                    SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
                }
            }
            
        }
    }

   
    bool Atari()
    {

        if (Limit == 0 && NormalCount != 0 && TutorialFlg == false)
        {
            if (Fail.gameObject.activeSelf == false)
            {
                Fail.gameObject.SetActive(true);
                
                FailFlg = true;
            }
            return true;
        }

        ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        for (int CollapsNow = 0; CollapsNow < CollapsCount; CollapsNow++)
        {
            Mesh CollapsMesh = CollapsBlocks[CollapsNow].GetComponent<MeshFilter>().mesh;
            Vector3[] CollapsVertices = CollapsMesh.vertices;

            for (int BlockNow = 0; BlockNow < NormalCount; BlockNow++)
            {
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
            }
        }

        
        if (Input.GetButton("AButton"))
        {
            for (int i = 0; i < NormalCount; i++)
            {
                if (NormalBlocks[i].GetComponent<Blocks>().NormalNowcol == true)
                {
                    NormalBlocks[i].GetComponent<Blocks>().BurnCnt += DefineScript.JUDGE_BNSPEED_BUTTON;
                    if (NormalBlocks[i].GetComponent<Blocks>().BurnCnt >= DefineScript.JUDGE_BNTIME)
                    {
                        NormalBlocks[i].GetComponent<Blocks>().canburn = true;
                        Collapsing = true;
                        UnsetCollapsing = false;
                        Burned = true;
                        
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
        if(Input.GetButtonUp("AButton"))
        {
            buttonup = true;
        }
        

        if (Collapsing == true)
        {
            for (int i = 0; i < NormalCount; i++)
            {
                if (NormalBlocks[i].GetComponent<Blocks>().NormalNowcol == true)
                {
                    UnsetCollapsing = false;
                    NormalBlocks[i].GetComponent<Blocks>().BurnCnt += DefineScript.JUDGE_BNSPEED_NONBUTTON;
                    if (NormalBlocks[i].GetComponent<Blocks>().BurnCnt >= DefineScript.JUDGE_BNTIME)
                    {
                        NormalBlocks[i].GetComponent<Blocks>().canburn = true;                       
                    }

                }
                else
                {
                    NormalBlocks[i].GetComponent<Blocks>().canburn = false;
                    NormalBlocks[i].GetComponent<Blocks>().BurnCnt = 0.0f;
                }
            }

            for (int i = 0; i < NormalCount; i++)
            {
                if (NormalBlocks[i].GetComponent<Blocks>().canburn == true)
                {
                    NormalBlocks[i].GetComponent<Blocks>().BurnFlg = true;
                    NormalBlocks[i].GetComponent<Blocks>().SetBurn();
                    NormalBlocks[i].GetComponent<Blocks>().SetBurnMaterial();
                    NormalBlocks[i].GetComponent<Blocks>().BurnCnt = 0.0f;
                    Burned = true;
                }
            }
        }

        if(UnsetCollapsing == true )
        {
            Collapsing = false;
        }



        if (NormalCount == 0 && TutorialFlg == false)
        {
            if (Clear.gameObject.activeSelf == false)
            {
                Clear.gameObject.SetActive(true);
            }
            ClearFlg = true;
            
            ClearedLimitNum = Limit;
            FailLimitNum = PassStageID.PassUpperCount() - Limit;
            //Todo: ここでスクショ撮影処理。
            //注意：このif分中は毎フレーム入ります。よって毎回取ることになってしまうことに注意。
            return true;
        }
        if (buttonup == true && Burned==true&&Nowcol == false) 
        {
            Limit--;
            Burned = false;
            buttonup = false;
            
        }

        return false;
    }

    void atari2(int BlockNow, int CollapsNow, int Blockplain, int CollapsPlain, Vector3[] CollapsVertices)
    {
        bool temp = false;
        if (Vector2.Distance(NormalPlaneVector[BlockNow, Blockplain],
                      CollapsPlaneVector[CollapsNow, CollapsPlain])
                      < DefineScript.JUDGE_DISTANCE)
        {
            if (IsVisibleFromCamera(CollapsPlain, CollapsVertices, ray))
            {
                if (NormalBlockPosition[BlockNow].z < CollapsBlockPosition[CollapsNow].z)
                {
                    CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsNowcol = true;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().NormalNowcol = true;
                    temp = true;
                }
            }
            else
            {
                if (NormalBlockPosition[BlockNow].z > CollapsBlockPosition[CollapsNow].z)
                {
                    CollapsBlocks[CollapsNow].GetComponent<Blocks>().CollapsNowcol = true;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().NormalNowcol = true;
                    temp = true;
                }
            }
            
        }
        if(temp==true)
        {
            switch (CollapsPlain)
            {
                case (int)DefineScript.CollisionIndex.Top:
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
    
    private void OnDestroy()
    {
        Sound.StopBgm();
        Sound.StopSe("se_burn", 2);
    }
}
