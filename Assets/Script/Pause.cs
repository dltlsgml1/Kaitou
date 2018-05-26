using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour {

    enum PouseState { Back, Restart, Stageselect };
    PouseState state;
    int move = 0;
    float outside = 20.0f;
    //GameObject Cursor = GameObject.Find("Pause_Cursor");
    //GameObject Cursor = GameObject.Find("/Pouse/Pause_Cursor");
    //GameObject.Find("/UI Root/MyPanel/Label") as GameObject;
    GameObject resetscript;
    GameMain Gamescript;
    Vector3 vec_Cursor;//= Cursor.transform.localPosition;    
    GameObject Cursor;
    // Use this for initialization
    void Start () {
        //ポーズ画面初期値へ持ってくる
        transform.localPosition = transform.up / outside;
        //vec_Cursor = GameObject.Find("Pause_Cursor").transform.position;
        //GameObject Cursor = GameObject.Find("/Pause_Cursor");
        //vec_Cursor = Cursor.transform.localPosition;
        Cursor = GameObject.FindGameObjectWithTag("Pause_Cursor");        
    }
	
	// Update is called once per frame
	void Update () {
        //確認用start-----------------------------
        if (Input.GetKey(KeyCode.E))    //仮置き
        {
            BackGameMain();
        }
        if (Input.GetKey(KeyCode.R))    //仮置き
        {
            Restart();	
        }




        //確認用end----------------------------

        MoveSelect();//移動処理

        switch (state)
        {
            case PouseState.Back:
                BackGameMain();
                break;

            case PouseState.Restart:
                Restart();
                //確認用消す予定
                if (Input.GetKey(KeyCode.I))    //仮置き
                {
                    transform.localPosition = transform.up / outside;
                    Debug.Log("position :" + transform.position);
                    vec_Cursor = Cursor.transform.localPosition;
                }
                break;

            case PouseState.Stageselect:
                BackStageSelect();
                // state = Title;
                break;
            default:
               
                break;
        }
        Debug.Log("position :" + transform.position);
        //確認用消す予定
        if (Input.GetKey(KeyCode.I))    //仮置き
        {
            transform.localPosition = transform.up / outside;
            Debug.Log("position :" + transform.position);
            vec_Cursor = Cursor.transform.localPosition;
        }


    }

    private void MoveSelect()
    {
        //ある座標に向かって移動アニメーション追加予定
        //Debug.Log("移動：MoveSelect");

        //移動先
        //if (Input.GetKey(KeyCode.PageUp))
        if(Input.GetKeyDown("up"))
        {
            //SceneManager.LoadSceneAsync("StageSelect");
            move -= 1;
            Debug.Log("上");
        }
        //if (Input.GetKey(KeyCode.PageDown))
        if(Input.GetKeyDown("down"))
        {
            //SceneManager.LoadSceneAsync("StageSelect");
            move += 1;
            Debug.Log("下");
        }
        if (move>2)//Pause選択数分超えないようにループ
        {
            move = 0;
        }

        //Debug.Log("y:" +vec_Cursor);
        //Debug.Log(transform.position);
        //float backposition = 0.0f;

 
        //GameObject Cursor = GameObject.Find("Pause_Cursor");
       
        vec_Cursor = Cursor.transform.localPosition;
        //Pause画面セレクト指移動
        switch (move) {
            case 0://バック位置
                //指定
                // transform.position.y = backposition;//1.6f;     //仮置き
                vec_Cursor.y = 1.5f;     //仮置き
                //Cursor.transform.localPosition = vec_Cursor;

                //transform.position = transform.up * 1.6f;
                //Debug.Log(transform.position);
                break;
            case 1://リスタート位置
                vec_Cursor.y = 0f;     //仮置き
                //Cursor.transform.localPosition = vec_Cursor;

                //transform.position = transform.up * 0;
                break;
            case 2://ステセレ位置
                vec_Cursor.y = -1.0f;     //仮置き
                //Cursor.transform.localPosition = vec_Cursor;
                //transform.position = transform.up * -1.0f;
                break;

        }

        Cursor.transform.localPosition = vec_Cursor;

        //Debug.Log("y2:" + vec_Cursor);
        //Debug.Log(Cursor.transform.localPosition);
        if (Input.GetKey(KeyCode.Q))
        {
            //state = (PouseState)move;
            Debug.Log("move:" + move);
            Debug.Log("state:" + state);

        }

        //確認用消す予定
        if (Input.GetKey(KeyCode.I))    //仮置き
        {
            transform.localPosition = transform.up * -outside;
            Debug.Log("position :" + transform.position);
            vec_Cursor = Cursor.transform.localPosition;
        }

    }

    private void Restart()      //ステージリスタート
    {
        //アニメーション追加予定


        //初期化
        //カメラ//ステージデータ//数値
        //初期化データ呼び出し

        //リスタート処理
        //resetscript.GetComponent<GameMain>();
        //Gamescript.SetStage(resetscript.Newstage);

        //ポーズ画面画面外移動
        transform.localPosition = transform.up * outside;

        //scene入ってリセット(仮)
        SceneManager.LoadSceneAsync("GameMain");
    }

    private void BackStageSelect()
    {
        //アニメーション追加予定

        //セレクトへ遷移処理
        SceneManager.LoadSceneAsync("StageSelect");
    }

    private void BackGameMain()
    {
        //アニメーション追加予定

        //ポーズ画面を消す処理
        //ポーズ画面画面外移動
        transform.localPosition = transform.up * outside;

        
    }

}
