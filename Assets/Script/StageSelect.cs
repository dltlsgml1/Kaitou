using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageSelect : MonoBehaviour
{
    public bool SelectStageFlag = false;    //ステージ選択決定
    public bool BackTitleFlag = false;      //タイトルに戻る判定
    public bool RightMoveFlag = false;      //右に移動するフラグ
    public bool LeftMoveFlag = false;       //左に移動するフラグ
    public bool TargetFlag = false;         //移動範囲固定用フラグ
    public bool ContinuousMoveFlag = false; //連続して移動する時のフラグ
    public bool FadeOutFlag = false;        //フェードアウトフラグ
    public bool FadeInitOutFlag = false;
    public bool TimeFlag = false;           //このフラグがtrueになったらzoomを止める
    public float Volume = 0.2f;             //サウンドのボリューム
    public Vector3 TargetPos;               //移動先の設定   
    public int StageID = 1;                     //ステージID
    public float DefaultKey = 0.5f;         //このスティック以上倒すとキー入力判定
    public Rigidbody RB;                    //このオブジェクトのRigidbodyを持ってくる用
    private float Distance = 14.0f;             //オブジェクト間の距離
    public Vector3 vector = new Vector3(20, 0, 0);   //移動時のベクトル
    public bool SePlayFlag = false;         //何回も再生しないように
    int StageNum = 30;

    public Camera ZoomIn;

    GameObject MapObject;
    MapScript Map;
    GameObject Fade;
    StageSelectFade FadeFlag;
    public GameObject NowObj;
    public bool IsClearNowObj;
    private FadeImage fadeImage;
    public float LookTime;
    public float InvisibleTime;
    public float FadeTime;
    float time = 0;                             //秒数計算用
    float timeCount = 0;                        //秒数計算用
    public int MaxTime = 1;                     //秒数


    // Use this for initialization
    void Start()
    {
        Fade = GameObject.Find("Panel");
        FadeFlag = Fade.GetComponent<StageSelectFade>();
        FadeFlag.FadeInFlag = true;
        MapObject = GameObject.Find("SS_StageList");
        Map = MapObject.GetComponent<MapScript>();
        Map.enabled = false;
        RB = GetComponent<Rigidbody>();      //このオブジェクトのRigidbodyを入れこむ
        Sound.LoadBgm("bgm", Sound.SearchFilename(Sound.eSoundFilename.SS_StageselectBgm)); //ステージセレクトのBGM
        Sound.LoadSe("Move", Sound.SearchFilename(Sound.eSoundFilename.SS_StageSelect)); //足音のSE
        Sound.LoadSe("StageIn", Sound.SearchFilename(Sound.eSoundFilename.SS_StageIn));     //メインに遷移する時のSE
        Sound.SetVolumeBgm("bgm", Volume);
        Sound.SetVolumeSe("Move", Volume, 0);
        Sound.SetVolumeSe("StageIn", Volume, 1);
        Sound.PlayBgm("bgm");
        Sound.SetLoopFlgSe("Move", true, 0);
        this.transform.position = new Vector3(0, 0, 0);
        StageID = PassStageID.PassStageId();
        this.transform.position = new Vector3(-Distance*(StageID), 0, 0);
        SetNowStagePrefab();
    }

    private void OnEnable()
    {
        StageID = PassStageID.PassStageId();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FadeFlag.FadeOutFlag&&!FadeFlag.FadeInFlag)
        {
            ChangeMapOpen();
            StageSelectMoveFlag();      //ステージ移動フラグを立てる
            StageSelectMove();          //ステージの移動をする
            SelectStage();              //ステージの決定かタイトルに戻るよう
            Transitions();              //遷移
        }
    }

    public void ChangeMapOpen()         //マップに切り替え
    {
        float Decision;                                 //上下を判定用
        Decision = Input.GetAxisRaw("LeftStick Y");     //左スティックを取る
        
        if (Decision < -DefaultKey && !TargetFlag)
        {
            PassStageID.GetStageID(StageID);
            Map.enabled = true;
            this.enabled = false;
        }
    }
    public void StageSelectMoveFlag()   //左スティックでステージの移動
    {
        float Decision;                                 //左右を判定用
        Vector3 pos = this.transform.position;          //オブジェクトのポジションを取る
        Decision = Input.GetAxisRaw("LeftStick X");     //左スティックを取る
        if (Decision != 0)
        {
            if (StageID < StageNum)
            {
                if (Decision > DefaultKey && !TargetFlag)
                {
                    fadeImage.SetMaterialAlpha(0);
                    StageID += 1;                           //左入力でステージナンバーが上がるはずなので上げる
                    LeftMoveFlag = true;
                    ContinuousMoveFlag = false;
                    }
                    if (Decision > DefaultKey && TargetFlag&& LeftMoveFlag)
                    {
                        if (TargetPos.x + 7 > this.transform.position.x)
                        {
                        fadeImage.SetMaterialAlpha(0);
                        StageID += 1;
                            ContinuousMoveFlag = true;
                        }
                    }

                }

            if (StageID != 0)
            {
                if (Decision < -DefaultKey && !TargetFlag)
                    {
                        fadeImage.SetMaterialAlpha(0);
                        StageID -= 1;                           //左入力でステージナンバーが下がるはずなので下げる
                        RightMoveFlag = true;
                    
                    }
                if (Decision < -DefaultKey && TargetFlag&& RightMoveFlag)
                {
                    if (TargetPos.x - 7 < this.transform.position.x)
                    {
                        fadeImage.SetMaterialAlpha(0);
                        StageID -= 1;
                        ContinuousMoveFlag = true;
                    }
                }
             }          
        }
    }
    public void StageSelectMove()   //ステージの移動
    {
        if (LeftMoveFlag && !TargetFlag || RightMoveFlag && !TargetFlag)    //移動範囲の設定
        {
            TargetPos = this.transform.position;
            if (RightMoveFlag)  //右に移動する場合
            {
                TargetPos.x += Distance;
            }
            if (LeftMoveFlag)   //左に移動する場合
            {
                TargetPos.x -= Distance;
            }
            TargetFlag = true;          //移動範囲の設定が何度も起こらないようにフラグをたてて移動中にここに来ないように
            RB.isKinematic = false;
        }
        if (ContinuousMoveFlag&&LeftMoveFlag)
        {
            TargetPos.x -= Distance;
            ContinuousMoveFlag = false;
        }
        if (ContinuousMoveFlag && RightMoveFlag)
        {
            TargetPos.x += Distance;
            ContinuousMoveFlag = false;
        }

        if (LeftMoveFlag)
        {
            if (this.transform.position.x > TargetPos.x)
            {
                if (!SePlayFlag)
                {
                    Sound.PlaySe("Move", 0);                //ToDo　音代わるかも
                    SePlayFlag = true;
                }
                RB.AddForce(-vector);     //右に移動
            }
            else
            {
                Sound.StopSe("Move", 0);                    //ToDo 音代わるかも
                SePlayFlag = false;
                this.transform.position = TargetPos;
                RB.isKinematic = true;
                LeftMoveFlag = false;
                TargetFlag = false;
                SetNowStagePrefab();
            }
        }
        if (RightMoveFlag)
        {
            if (this.transform.position.x < TargetPos.x)
            {
                if (!SePlayFlag)
                {
                    Sound.PlaySe("Move", 0);                    //ToDo　音代わるかも
                    SePlayFlag = true;
                }
                RB.AddForce(vector);        //左に移動

            }
            else
            {
                Sound.StopSe("Move", 0);                        //ToDo 音代わるかも
                SePlayFlag = false;
                this.transform.position = TargetPos;
                RB.isKinematic = true;
                RightMoveFlag = false;
                TargetFlag = false;
                SetNowStagePrefab();
            }
        }

        if(!LeftMoveFlag && !RightMoveFlag && IsClearNowObj)
        {
            //Debug.Log("stop now");
            //Debug.Log("now fading");
            FadeImage();
        }
    }
    public void SelectStage()       //遊ぶステージの決定
    {
        float Decision;
        if (StageID != 0)
        {
            if (Input.GetButtonDown("AButton") && !TargetFlag)
            {
                Sound.PlaySe("StageIn", 1);                     //ToDo 音代わるかも
                Sound.StopBgm();                                //ToDo　音代わるかも
                SelectStageFlag = true;
            }
        }
        Decision = Input.GetAxisRaw("LeftStick X");
        if (Decision < -DefaultKey)
        {
            if (this.transform.position.x >= 0)
            {
                BackTitleFlag = true;
            }
        }
    }
    public void Transitions()       //遷移
    {
        if (SelectStageFlag)
        {
           
            if (time < MaxTime - 0.1f)
            {
                if (!FadeInitOutFlag)
                {
                    FadeFlag.FadeOutFlag = true;
                    FadeInitOutFlag = true;
                }
            }

            else
            {
                time = Mathf.PingPong(timeCount, MaxTime + 0.1f);
                ZoomIn.orthographicSize -= 0.01f;
            }
            
            if (!FadeFlag.FadeOutFlag)
            {
                SelectStageFlag = false;
                PassStageID.GetStageID(StageID);
                PassStageID.GetStageName(CSVData.StageDateList[StageID].StageName);
                Debug.Log(CSVData.StageDateList[StageID].StageName);
                PassStageID.GetPosition((float)CSVData.StageDateList[StageID].Pos_X, (float)CSVData.StageDateList[StageID].Pos_Y, (float)CSVData.StageDateList[StageID].Pos_Z);
                PassStageID.GetRotation((float)CSVData.StageDateList[StageID].Rot_X, (float)CSVData.StageDateList[StageID].Rot_Y, (float)CSVData.StageDateList[StageID].Rot_Z);
                PassStageID.GetUpperCount((int)CSVData.StageDateList[StageID].UpperCunt);
                SceneManager.LoadScene("Gamemain", LoadSceneMode.Single);
            }
        }
        if (BackTitleFlag)
        {
            BackTitleFlag = false;
           // SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }
    }

    private void FadeImage()
    {
        fadeImage.Fade();
    }

    private void SetNowStagePrefab()
    {
        IsClearNowObj = (CSVData.StageDateList[StageID].ClearFlag != 0 && CSVData.StageDateList[StageID].ClearFlag >= CSVData.StageDateList[StageID].MinCunt) ? true : false;
        NowObj = this.transform.Find("Stage" + CastStageId(StageID) + "(Clone)").gameObject;
        NowObj = NowObj.transform.Find("ClearStageSS").gameObject;
        fadeImage = NowObj.GetComponent<FadeImage>();
        fadeImage.Init(NowObj.GetComponent<Renderer>(), LookTime, InvisibleTime, FadeTime);
    }

    private string CastStageId(int stageid)
    {
        string str;

        if (stageid >= 0 && stageid <= 9)
        {
            str = "0" + stageid;
        }
        else
        {
            str = stageid + "";
        }

        return str;
    }
}