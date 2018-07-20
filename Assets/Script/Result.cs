
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Result : MonoBehaviour
{

    public GameObject SetImage;
    public GameObject SetCursol;
    public GameObject[] StarObj = new GameObject[3];
    public GameObject[] Select = new GameObject[4];
    public GameObject SetStarEmpty;
    public GameObject SetStarObj;

    public Material[] starmat = new Material[3];
    public bool fadeInflg;
    public bool fadeOutflg;
    public bool resultAnimationStart;
    public bool result_ok;
    public bool bronzflg;
    public bool silverflg;
    public bool goldflg;
    bool OneSeFlg;
    bool StarOneSeFlg1;
    bool StarOneSeFlg2;
    bool StarOneSeFlg3;
    public LineAnimetion SelectLine;

    //-------test----------------
    //StageRank ResultRankCheck;
    //StageRank.RANK rank;
    //----------------------

    Image fade;

    Transform KeyPosition;
    Transform[] SetStar = new Transform[3];
    Vector3[] InitSize = new Vector3[3];
    SpriteRenderer[] Selectfade = new SpriteRenderer[4];
    Vector3 PositionPlus;


    float[] size = new float[3];
    int S_ID;
    int Cursolnum;
    float fade_color_A;
    float select_color_A;


    //test
    int GLimit;
    int SLimit;
    GameMain GetMain;
    int Limit;
    //
    float SetCursol_x = 3.15f;
    //星サイズ　900
    const float SetCursol_x_Start = 1.56f;
    const float SetCursol_x_End = 4.15f;
    const float SetCursol_z = 1.5f;
    const float Cursol1 = 1.2f;
    const float Cursol2 = -0.8f;
    const float Cursol3 = -3;
    const float fadespeed = 0.05f;

    const float DefaultKeyPos = 0.8f;
    const int LastStageID = 30;

    // スクショ用
    private ScreenShot SS;





    // Use this for initialization
    void Start()
    {

        SelectLine.InitLineAnimation();
        
        S_ID = PassStageID.PassStageId();
        
        fadeInflg = false;
        fadeOutflg = true;
        resultAnimationStart = false;
        result_ok = false;
        OneSeFlg = false;
        StarOneSeFlg1 = false;
        StarOneSeFlg2 = false;
        StarOneSeFlg3 = false;
        //最後のステージは「次へ」無し
        if (LastStageID == S_ID)
        {
            Cursolnum = 1;
        }
        else
        {
            Cursolnum = 0;
        }

        fade_color_A = 1;
        select_color_A = 0;
        size[0] = size[1] = 0; size[2] = 0;

        SetStarObj.transform.localPosition = new Vector3(SetStarEmpty.transform.localPosition.x,
                                                        SetStarEmpty.transform.localPosition.y,
                                                        SetStarEmpty.transform.localPosition.z + 1.5f);

        SetStar[0] = StarObj[0].GetComponent<Transform>();
        SetStar[1] = StarObj[1].GetComponent<Transform>();
        SetStar[2] = StarObj[2].GetComponent<Transform>();
        //ResultRankCheck = GetComponent<StageRank>();


        //フレームカラー初期化
        for (int i = 0; i < 4; i++)
        {
            GameObject.Find("Frame").transform.GetChild(i).gameObject.SetActive(false);
        }
        //ゲームメイン評価確認
        Limit = GameObject.Find("SaveData").GetComponent<ExportCsvScript>().GetClearData(PassStageID.PassStageId());
        GLimit = (int)CSVData.StageDateList[PassStageID.PassStageId()].GoldCunt;
        SLimit = (int)CSVData.StageDateList[PassStageID.PassStageId()].SilverCunt;



        if (Limit <= GLimit)
        {
            GameObject.Find("Frame").transform.GetChild(3).gameObject.SetActive(true);
            bronzflg = false;
            silverflg = false;
            goldflg = true;
        }
        else if (Limit <= SLimit)
        {
            GameObject.Find("Frame").transform.GetChild(2).gameObject.SetActive(true);
            bronzflg = false;
            silverflg = true;
            goldflg = false;
        }
        else
        {
            GameObject.Find("Frame").transform.GetChild(1).gameObject.SetActive(true);
            bronzflg = true;
            silverflg = false;
            goldflg = false;
        }




        //初期星位置保存
        for (int i = 0; i < 3; i++)
        {
            InitSize[i] = new Vector3(SetStar[i].localPosition.x, SetStar[i].localPosition.y, SetStar[i].localPosition.z);
        }


        //マテリアルセット
        for (int i = 0; i < 4; i++)
        {
            Selectfade[i] = Select[i].GetComponent<SpriteRenderer>();
        }

        //Todo:確認 ステセレは星の色が金で一定だが評価の場合はどうするのか？-------------------

        //↓評価星色変更Ver
        //if (bronzflg)
        //{
        //    SetStar[0].GetComponent<MeshRenderer>().material = starmat[0];
        //    SetStar[1].GetComponent<MeshRenderer>().material = starmat[0];
        //    SetStar[2].GetComponent<MeshRenderer>().material = starmat[0];
        //    GameObject.Find("Frame").transform.GetChild(1).gameObject.SetActive(true);
        //}
        //else if (silverflg)
        //{
        //    SetStar[0].GetComponent<MeshRenderer>().material = starmat[1];
        //    SetStar[1].GetComponent<MeshRenderer>().material = starmat[1];
        //    SetStar[2].GetComponent<MeshRenderer>().material = starmat[1];
        //    GameObject.Find("Frame").transform.GetChild(2).gameObject.SetActive(true);
        //}
        //else if (goldflg)
        //{
        //    SetStar[0].GetComponent<MeshRenderer>().material = starmat[2];
        //    SetStar[1].GetComponent<MeshRenderer>().material = starmat[2];
        //    SetStar[2].GetComponent<MeshRenderer>().material = starmat[2];
        //    GameObject.Find("Frame").transform.GetChild(3).gameObject.SetActive(true);
        //} 


        //↓評価星色一定Ver
        SetStar[0].GetComponent<MeshRenderer>().material = starmat[2];
        SetStar[1].GetComponent<MeshRenderer>().material = starmat[2];
        SetStar[2].GetComponent<MeshRenderer>().material = starmat[2];
        if (bronzflg)
        {
            GameObject.Find("Frame").transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (silverflg)
        {
            GameObject.Find("Frame").transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (goldflg)
        {
            GameObject.Find("Frame").transform.GetChild(3).gameObject.SetActive(true);
        }

        //---------------------------------------------------------------------------------------------



        fade = SetImage.GetComponent<Image>();


        //初期フェードカラー
        fade.color = new Color(1, 1, 1, fade_color_A);

        //初期カーソルポジション
        SelectLine.LineObj.transform.localPosition = new Vector3(SetCursol_x, Cursol1, SetCursol_z);

        // スクリーンショット初期化(Todo：クリアSSが貼れてない)
        SS = this.GetComponent<ScreenShot>();
        //SS.SetImage(GameObject.Find("ResultCanvas/Stage/Frame/ClearStageSS").GetComponent<MeshRenderer>(), "ClearImage" + SS.IdToString(PassStageID.StageID));
        SS.SetImage(GameObject.Find("ResultCanvas/Stage/Frame/ClearStageSS").GetComponent<MeshRenderer>(), "ResultImage");

        //リザルトBGM
        //Sound.LoadBgm("GM_BGM", "GameMain/GM_Bgm");
        //カーソル移動SE
        //Sound.LoadSe("SE_STAR", "GameMain/GM_Star");
        //決定SE
        //Sound.LoadSe("SE_CLEAR", "GameMain/GM_Clear");
        //星評価SE
        //Sound.LoadSe("SE_CLEAR", "GameMain/GM_Clear");



        //Sound.PlayBgm("GM_BGM");

        //Sound.SetVolumeSe("SE_INFO", 0.5f, 4);
    }






    // Update is called once per frame
    void Update()
    {

        //フェードアウト終了完了（操作可能区間）
        if (result_ok)
        {

            //入力処理
            InputCollection();


            //カーソル位置更新
            KeyCheck();
            //ShowKey();

            //選択肢フェードイン
            SelectFadeIn();

        }
        //フェード更新
        ResultFade();

        //リザルトアニメーション更新
        ResultAnimation();

    }


    //入力関係の処理
    void InputCollection()
    {
        //SESTOP(入力関係)
        if (OneSeFlg)
        {
            //Sound.StopSe("SE_INFO", 4);
            OneSeFlg = true;
        }
        //フェードイン・アウト中の操作切断
        if (fadeInflg == false && fadeOutflg == false)
        {
            float Decision;                                 //上下を判定用
            Decision = Input.GetAxisRaw("LeftStick Y");     //左スティックを取る

            //カーソル移動
            if (Decision > DefaultKeyPos)
            {
                //if (Input.GetKeyDown(KeyCode.UpArrow))
                //{
                if (SelectLine.tmpTime > (SelectLine.AnimTime / 2))
                {
                    //最終ステージ処理
                    if (LastStageID == S_ID)
                    {
                        if (Cursolnum < 2)
                        {
                            Cursolnum = 2;
                        }
                        else
                        {
                            Cursolnum--;
                        }
                    }
                    else
                    {
                        if (Cursolnum < 1)
                        {
                            Cursolnum = 2;
                        }
                        else
                        {
                            Cursolnum--;
                        }
                    }
                    SelectLine.InitLineAnimation();
                    //SE再生（カーソル移動）
                    if (!OneSeFlg)
                    {
                        //Sound.PlaySe("SE_INFO", 4);
                        OneSeFlg = true;
                    }

                }
            }
            if (Decision < -DefaultKeyPos)
            {
                //if (Input.GetKeyDown(KeyCode.DownArrow))
                //{
                if (SelectLine.tmpTime > (SelectLine.AnimTime / 2))
                {
                    if (Cursolnum > 1)
                    {
                        //最終ステージ処理
                        if (LastStageID == S_ID)
                        {
                            Cursolnum = 1;
                        }
                        else
                        {
                            Cursolnum = 0;
                        }
                    }
                    else
                    {
                        Cursolnum++;
                    }
                    SelectLine.InitLineAnimation();
                    //SE再生（カーソル移動）
                    if (!OneSeFlg)
                    {
                        //Sound.PlaySe("SE_INFO", 4);
                        OneSeFlg = true;
                    }
                }
            }

            //ラインアニメーション移動
            MoveCursol();


            //決定
            if (Input.GetButtonDown("AButton"))
            {
                GlobalCoroutine.Go(SS.DeleteScreenshot("ResultImage"));
                //if (Input.GetKeyDown(KeyCode.Space))
                //{
                fadeInflg = true;
                //SE再生(決定)
                if (!OneSeFlg)
                {
                    //Sound.PlaySe("SE_INFO", 4);
                    OneSeFlg = true;
                }

            }
        }
    }




    void MoveCursol()
    {
        SetCursol_x = SetCursol_x_Start + (SetCursol_x_End - SetCursol_x_Start) * (SelectLine.tmpTime - 0.0f) / (SelectLine.AnimTime - 0.0f);
    }



    //シーン遷移先確認
    void StageCheck()
    {
        switch (Cursolnum)
        {
            case 0://次へ

                PassStageID.GetStageID(S_ID+1);
                PassStageID.GetStageName(CSVData.StageDateList[S_ID+1].StageName);
                PassStageID.GetPosition((float)CSVData.StageDateList[S_ID + 1].Pos_X, (float)CSVData.StageDateList[S_ID + 1].Pos_Y, (float)CSVData.StageDateList[S_ID + 1].Pos_Z);
                PassStageID.GetRotation((float)CSVData.StageDateList[S_ID + 1].Rot_X, (float)CSVData.StageDateList[S_ID + 1].Rot_Y, (float)CSVData.StageDateList[S_ID + 1].Rot_Z);
                PassStageID.GetUpperCount((int)CSVData.StageDateList[S_ID + 1].UpperCunt);
                SceneManager.LoadScene("GameMain", LoadSceneMode.Single);
                
                break;
            case 1://もう一度
                
                SceneManager.LoadScene("GameMain", LoadSceneMode.Single);

                break;
            case 2://ステセレ
                
                SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);

                break;
        }
        ResultInit();//初期化
    }





    //-1.2,10.9,62.1
    //28.538,5.403,14.047
    //カーソル位置確認処理
    void KeyCheck()
    {
        switch (Cursolnum)
        {
            case 0:
                SelectLine.LineObj.transform.localPosition = new Vector3(SetCursol_x, Cursol1, SetCursol_z);
                //KeyPosition.gameObject.transform.localPosition = new Vector3(SetCursol_x, Cursol1, SetCursol_z);
                break;
            case 1:
                SelectLine.LineObj.transform.localPosition = new Vector3(SetCursol_x, Cursol2, SetCursol_z);
                //KeyPosition.gameObject.transform.localPosition = new Vector3(SetCursol_x, Cursol2, SetCursol_z);
                break;
            case 2:
                SelectLine.LineObj.transform.localPosition = new Vector3(SetCursol_x, Cursol3, SetCursol_z);
                //KeyPosition.gameObject.transform.localPosition = new Vector3(SetCursol_x, Cursol3, SetCursol_z);
                break;
        }

    }







    //フェードイン・アウト処理
    void ResultFade()
    {
        if (fadeInflg)
        {
            if (fade_color_A > 1)
            {
                fadeInflg = false;
                StageCheck();//シーン遷移
            }
            fade_color_A += fadespeed;
        }
        else if (fadeOutflg)
        {
            if (fade_color_A < 0)
            {
                fadeOutflg = false;
                resultAnimationStart = true;//評価アニメーションスタート
            }
            fade_color_A -= fadespeed;

        }
        fade.color = new Color(1, 1, 1, fade_color_A);
    }







    //選択項目フェードイン処理
    void SelectFadeIn()
    {
        if (select_color_A < 255)
        {
            select_color_A += fadespeed;
            for (int i = 0; i < 4; i++)
            {
                if (LastStageID == S_ID && i == 0) { }//最終ステージのみ
                else
                {
                    Selectfade[i].color = new Color(255, 255, 255, select_color_A);
                }
            }
        }
    }







    //評価アニメーション処理
    void ResultAnimation()
    {
        if (resultAnimationStart)
        {
            //-7
            //SetStarEmpty
            if (SetStar[0].transform.localPosition.z >= InitSize[0].z - 15)
            {
                //size[0] -= 0.5f;
                PositionPlus = SetStar[0].transform.localPosition;
                PositionPlus.z -= 2.0f;
                SetStar[0].transform.localPosition = PositionPlus;
                //SetStar[0].transform.localPosition.z = size[0];
            }
            else if (SetStar[0].transform.localPosition.z <= InitSize[1].z - 15 && (silverflg == true || goldflg == true) && SetStar[1].transform.localPosition.z >= InitSize[1].z - 15)
            {
                //星評価SE1
                if (!StarOneSeFlg1)
                {
                    //Sound.PlaySe("SE_INFO", 4);
                    StarOneSeFlg1 = true;
                }
                else
                {
                    //Sound.StopSe("SE_INFO", 4);
                }
                //size[1] -= 1.0f;
                PositionPlus = SetStar[1].transform.localPosition;
                PositionPlus.z -= 3.0f;
                SetStar[1].transform.localPosition = PositionPlus;
            }
            else if (SetStar[1].transform.localPosition.z <= InitSize[2].z - 15 && goldflg == true && SetStar[2].transform.localPosition.z >= InitSize[2].z - 15)
            {
                //星評価SE2
                if (!StarOneSeFlg2)
                {
                    //Sound.PlaySe("SE_INFO", 4);
                    StarOneSeFlg2 = true;
                }
                else
                {
                    //Sound.StopSe("SE_INFO", 4);
                }
                //size[2] -= 1.0f;
                PositionPlus = SetStar[2].transform.localPosition;
                PositionPlus.z -= 3.0f;
                SetStar[2].transform.localPosition = PositionPlus;
            }
            else
            {
                //星評価SE3
                if (!StarOneSeFlg3)
                {
                    //Sound.PlaySe("SE_INFO", 4);
                    StarOneSeFlg3 = true;
                }
                else
                {
                    //Sound.StopSe("SE_INFO", 4);
                }
                resultAnimationStart = false;
                result_ok = true;
            }

        }
    }








    //初期化処理
    void ResultInit()
    {
        fadeInflg = false;
        fadeOutflg = true;
        resultAnimationStart = false;
        result_ok = false;
        StarOneSeFlg1 = StarOneSeFlg2 = StarOneSeFlg3 = false;

        Cursolnum = 0;
        fade_color_A = 1;
        select_color_A = 0;
        size[0] = size[1] = 0; size[2] = 0;

        //初期位置
        for (int i = 0; i < 3; i++)
        {
            SetStar[i].transform.localPosition = new Vector3(InitSize[i].x, InitSize[i].y, InitSize[i].z);
        }

        //初期フェードカラー
        fade.color = new Color(1, 1, 1, fade_color_A);

        //フレームカラー初期化
        for (int i = 0; i < 4; i++)
        {
            GameObject.Find("Frame").transform.GetChild(i).gameObject.SetActive(false);
        }

        SelectLine.LineObj.transform.localPosition = new Vector3(SetCursol_x, Cursol1, SetCursol_z);
        //初期選択項目フェード
        for (int i = 0; i < 4; i++)
        {
            Selectfade[i].color = new Color(255, 255, 255, select_color_A);
        }
    }

}
