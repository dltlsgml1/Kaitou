using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{                  
    public GameObject NowStageObj;
    
    public Camera MainCamera;
    public Camera BackGroundCamera;
    public GameObject CameraObj;
    public GameObject Clear;
    public GameObject Fail;
    public Text targettext;

    GameObject[] Blocks = new GameObject[DefineScript.NUM_BLOCKS];
    GameObject[] CollapsBocks = new GameObject[DefineScript.NUM_BLOCKS];
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
    public bool IsVisibleBlock = false;
    public bool IsVisibleCollaps = false;
    public bool PlaneCollaps = false;
    public int seigen;
    public bool minusseigen = false;
    public bool ClearFlg = false;
    public bool keka = false;
  
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
        Fail.gameObject.SetActive(false);
        keka = false;
        ClearFlg = false;
        seigen = PassStageID.PassUpperCount();

        MoveCamera mvcamera = GameObject.Find("GameObject").GetComponent<MoveCamera>();
        mvcamera.Position = PassStageID.PassPosition();
        mvcamera.Rotation = PassStageID.PassRotation();
    }

    public int Getseigen()
    {
        return seigen;
    }

    void Start()
    {
        Blocks = GameObject.FindGameObjectsWithTag("NormalBlock");
        Clear.gameObject.SetActive(false);
        Fail.gameObject.SetActive(false);
        Sound.LoadBgm("gm_bgm", "GM_Bgm");
        Sound.LoadBgm("gm_burn", "GM_Burn");
        Sound.LoadBgm("gm_burnnow", "GM_BurnNow");
        Sound.LoadSe("se_burn", "GM_Burn");
        Sound.LoadSe("se_burnnow", "GM_BurnNow");
        Sound.PlayBgm("gm_bgm");
        Sound.PlaySe("se_burn", 2);
        seigen = PassStageID.PassUpperCount();
      
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
    }

    void atari2(int BlockNow, int CollapsNow, int Blockplain, int CollapsPlain, Vector3[] CollapsVertices)
    {

        if (Vector2.Distance(NormalPlaneVector[BlockNow, Blockplain],
                      CollapsPlaneVector[CollapsNow, CollapsPlain])
                      < DefineScript.JUDGE_DISTANCE)
        {

            if (IsVisibleFromCamera(CollapsPlain, CollapsVertices, ray))
            {
                if (NormalBlockPosition[BlockNow].z > CollapsBlockPosition[CollapsNow].z)
                {
                    PlaneCollaps = false;
                    NormalBlocks[BlockNow].GetComponent<Blocks>().canburn = false;
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
                    NormalBlocks[BlockNow].GetComponent<Blocks>().canburn = false;

                }
                else
                {
                    PlaneCollaps = NormalBlocks[BlockNow].GetComponent<Blocks>().Burning();
                }
            }

        }

    }

    bool Atari()
    {
        NormalCount = 0;
        CollapsCount = 0;
        minusseigen = false;
        targettext.text = seigen.ToString();
        for (int i = 0, j = 0, k = 0; i < Blocks.Length; i++)
        {
            if (Blocks[i].GetComponent<Blocks>().BurnFlg == true)
            {

                CollapsBocks[j] = Blocks[i];
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

        if (seigen == 0 && NormalCount != 0)
        {
            if (Fail.gameObject.activeSelf == false)
            {
                Fail.gameObject.SetActive(true);
            }
        }

        ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        for (int CollapsNow = 0; CollapsNow < CollapsCount; CollapsNow++)
        {
            Mesh CollapsMesh = CollapsBocks[CollapsNow].GetComponent<MeshFilter>().mesh;
            Vector3[] CollapsVertices = CollapsMesh.vertices;


            for (int BlockNow = 0; BlockNow < NormalCount; BlockNow++)
            {
                atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Bottom, (int)DefineScript.CollisionIndex.Top, CollapsVertices);
                atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Top, (int)DefineScript.CollisionIndex.Bottom, CollapsVertices);
                atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Right, (int)DefineScript.CollisionIndex.Left, CollapsVertices);
                atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Left, (int)DefineScript.CollisionIndex.Right, CollapsVertices);
                atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Front, (int)DefineScript.CollisionIndex.Back, CollapsVertices);
                atari2(BlockNow, CollapsNow, (int)DefineScript.CollisionIndex.Back, (int)DefineScript.CollisionIndex.Front, CollapsVertices);
                
                //if (PlaneCollaps == true && Input.GetKeyDown(KeyCode.Space))
                //{
                //    NormalBlocks[BlockNow].GetComponent<Blocks>().BurnFlg = true;
                //    NormalBlocks[BlockNow].GetComponent<Blocks>().SetBurn();
                //    NormalBlocks[BlockNow].GetComponent<Blocks>().SetBurnMaterial();
                //    global::Blocks.nowplayingse = false;
                //    seigen--;
                //    PlaneCollaps = false;
                //}
                //else
                //{
                //    global::Blocks.nowplayingse = false;
                //}

            }



        }
        if (Input.GetButtonDown("AButton"))
        {
            for(int i=0;i<NormalCount;i++)
            {
                if(NormalBlocks[i].GetComponent<Blocks>().canburn==true)
                {
                    NormalBlocks[i].GetComponent<Blocks>().BurnFlg = true;
                    NormalBlocks[i].GetComponent<Blocks>().SetBurn();
                    NormalBlocks[i].GetComponent<Blocks>().SetBurnMaterial();
                    global::Blocks.BurningCnt = 0.0f;
                    if(minusseigen==false)
                    {
                        seigen--;
                        minusseigen = true;
                    }
                      
                }
            }


        }

        

        if (NormalCount == 0)
        {
            if (Clear.gameObject.activeSelf == false)
            {
                Clear.gameObject.SetActive(true);
            }
            ClearFlg = true;
            return true;
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
    
    private void OnDestroy()
    {
        Sound.StopBgm();
        Sound.StopSe("se_burn", 2);
    }
}
