﻿using System.Collections;
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
    public float Volume = 0.2f;             //サウンドのボリューム
    public Vector3 TargetPos;               //移動先の設定   
    public int StageID = 1;                     //ステージID
    public float DefaultKey = 0.5f;         //このスティック以上倒すとキー入力判定
    public Rigidbody RB;                    //このオブジェクトのRigidbodyを持ってくる用
    private float Distance = 14.0f;             //オブジェクト間の距離
    public Vector3 vector = new Vector3(5, 0, 0);   //移動時のベクトル
    public int StageNum = 31;                //ステージの数(仮置き)
    public bool SePlayFlag = false;         //何回も再生しないように
    GameObject StageLoadObject;
    StageLoad StageLoad;
    Sound Sound;
    PassStageID PassID;
    private static GameObject CSVData;
    private static CsvLoad CsvData;


    // Use this for initialization
    void Start()
    {
        StageLoadObject = GameObject.Find("StagePrefab");
        StageLoad = StageLoadObject.GetComponent<StageLoad>();
        RB = GetComponent<Rigidbody>();      //このオブジェクトのRigidbodyを入れこむ
        Sound.LoadBgm("bgm", Sound.SearchFilename(Sound.eSoundFilename.SS_StageselectBgm)); //ステージセレクトのBGM
        Sound.LoadSe("Move", Sound.SearchFilename(Sound.eSoundFilename.SS_StageSelect)); //足音のSE
        Sound.LoadSe("StageIn", Sound.SearchFilename(Sound.eSoundFilename.SS_StageIn));     //メインに遷移する時のSE
        Sound.SetVolumeBgm("bgm", Volume);
        Sound.SetVolumeSe("Move", Volume, 0);
        Sound.SetVolumeSe("StageIn", Volume, 1);

        Sound.PlayBgm("bgm");
        Sound.SetLoopFlgSe("Move", true, 0);

        CSVData = GameObject.Find("CSVLoad");
        CsvData = CSVData.GetComponent<CsvLoad>();
    }

    // Update is called once per frame
    void Update()
    {
        StageSelectMoveFlag();      //ステージ移動フラグを立てる
        StageSelectMove();          //ステージの移動をする
        SelectStage();              //ステージの決定かタイトルに戻るよう
        Transitions();              //遷移
    }

    public void StageSelectMoveFlag()   //左スティックでステージの移動
    {
        float Decision;                                 //左右を判定用
        Vector3 pos = this.transform.position;          //オブジェクトのポジションを取る
        Decision = Input.GetAxisRaw("LeftStick X");     //左スティックを取る
        if (Decision != 0)
        {
            if (StageID < StageNum+1)
            {

                if (Decision > DefaultKey && !TargetFlag)
                {
                    StageID += 1;                           //左入力でステージナンバーが上がるはずなので上げる
                    LeftMoveFlag = true;
                }
            }
            if (Decision < -DefaultKey && !TargetFlag)
            {
                StageID -= 1;                           //左入力でステージナンバーが下がるはずなので下げる
                RightMoveFlag = true;
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

        if (LeftMoveFlag)
        {
            if (this.transform.position.x > TargetPos.x)
            {
                if (!SePlayFlag)
                {
                    Sound.PlaySe("Move", 0);
                    SePlayFlag = true;
                }
                RB.AddForce(-vector);     //右に移動
            }
            else
            {
                Sound.StopSe("Move", 0);
                SePlayFlag = false;
                this.transform.position = TargetPos;
                RB.isKinematic = true;
                LeftMoveFlag = false;
                TargetFlag = false;
            }
        }
        if (RightMoveFlag)
        {
            if (this.transform.position.x < TargetPos.x)
            {
                if (!SePlayFlag)
                {
                    Sound.PlaySe("Move", 0);
                    SePlayFlag = true;
                }
                RB.AddForce(vector);        //左に移動

            }
            else
            {
                Sound.StopSe("Move", 0);
                SePlayFlag = false;
                this.transform.position = TargetPos;
                RB.isKinematic = true;
                RightMoveFlag = false;
                TargetFlag = false;
            }
        }
    }
    public void SelectStage()       //遊ぶステージの決定
    {
        float Decision;
        if (Input.GetButtonDown("BButton") && !TargetFlag)
        {
            Sound.PlaySe("StageIn", 1);
            Sound.StopBgm();
            SelectStageFlag = true;
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
            SelectStageFlag = false;
            PassStageID.GetStageID(StageID);
            PassStageID.GetStageName(CsvData.StageDateList[StageID].StageName);
            PassStageID.GetPosition((float)CsvData.StageDateList[StageID].Pos_X, (float)CsvData.StageDateList[StageID].Pos_Y, (float)CsvData.StageDateList[StageID].Pos_Z);
            PassStageID.GetRotation((float)CsvData.StageDateList[StageID].Rot_X, (float)CsvData.StageDateList[StageID].Rot_Y, (float)CsvData.StageDateList[StageID].Rot_Z);
            SceneManager.LoadScene("Gamemain", LoadSceneMode.Single);
        }
        if (BackTitleFlag)
        {
            BackTitleFlag = false;
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }
    }
}