using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {
    public bool SelectStageFlag = false;    //ステージ選択決定
    public bool BackTitleFlag = false;      //タイトルに戻る判定
    public bool RightMoveFlag = false;      //右に移動するフラグ
    public bool LeftMoveFlag = false;       //左に移動するフラグ
    public bool TargetFlag = false;         //移動範囲固定用フラグ
    public Vector3 TargetPos;               //移動先の設定   
    public int StageID;                     //ステージID
    public float DefaultKey = 0.5f;         //このスティック以上倒すとキー入力判定
    public Rigidbody RB;                    //このオブジェクトのRigidbodyを持ってくる用
    public float Distance = 15.0f;             //オブジェクト間の距離
    public Vector3 vector = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start () {
        RB = GetComponent<Rigidbody>();      //このオブジェクトのRigidbodyを入れこむ
    }
	
	// Update is called once per frame
	void Update () {
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
        if (Input.GetButton("LeftStick X"))
        {
            if (Decision < -DefaultKey&&!TargetFlag)
            {
                StageID -= 1;                           //左入力でステージナンバーが下がるはずなので下げる
                RightMoveFlag = true;
            }
            if (Decision > DefaultKey && !TargetFlag)
            {
                StageID += 1;                           //左入力でステージナンバーが上がるはずなので上げる
                LeftMoveFlag = true;
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
                RB.AddForce(-vector);     //右に移動
            }
            else
            {
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
                RB.AddForce(vector);        //左に移動

            }
            else
            {
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
        if (Input.GetButtonDown("BButton")&&!TargetFlag)
        {
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
            SceneManager.LoadScene("Gamemain", LoadSceneMode.Single);
        }
        if (BackTitleFlag)
        {
            BackTitleFlag = false;
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }
    }
}
