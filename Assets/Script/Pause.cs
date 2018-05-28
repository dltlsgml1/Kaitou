using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour {

    public static bool is_pause = false;
    //　ポーズした時に表示するUI
    [SerializeField]
    private GameObject pauseUI;
    public GameObject Cursor;


    public enum PouseState { Back, Restart, Stageselect };
    PouseState state;
    public int move = 0;
    public float outside = 20.0f;

    Vector3 vec_Cursor;//= Cursor.transform.localPosition;  

	// Use this for initialization
	void Start () {
   
	}
	
	// Update is called once per frame
	void Update () {

        
        ////ｑキーでゲームバック
        if (Input.GetKeyDown("q"))
        {
            is_pause = true;
           
        }


        if(is_pause==true)
        {
            
            SetPause();
            MoveSelect();
        }

        if(is_pause==true && Input.GetKeyDown("w"))
        {
            OffPause();
            is_pause = false;
        }


        if (Input.GetKeyDown("space") && is_pause)  //決定キーに差し替え予定
        {
            
            switch (move)
            {
                case 0:
                    //SE追加予定
                    SetPause();
                    break;
                case 1:
                    //SE追加予定
                    Restart();
                    break;
                case 2:
                    //SE追加予定
                    BackStageSelect();
                    break;

            }
        }

        //gameObject.SetActive(false); //非活性化

	}

    private void Restart()      //リスタート
    {
        //アニメーション追加予定
        //SE追加予定

        //初期化
        //カメラ//ステージデータ//数値
        //初期化データ呼び出し

        //リスタート処理
        //resetscript.GetComponent<GameMain>();
        //Gamescript.SetStage(resetscript.Newstage);


        //scene入ってリセット(仮)
        SceneManager.LoadSceneAsync("GameMain");
    }

    private void BackStageSelect()
    {
        //アニメーション追加予定
        //SE追加予定

        //セレクトへ遷移処理
        SceneManager.LoadSceneAsync("StageSelect");
    }

    private void SetPause()
    {
        //アニメーション追加予定
        //SE追加予定
        //　ポーズUIのアクティブ、非アクティブを切り替え

        Debug.Log("im in Setpause");
        if (pauseUI.gameObject.activeSelf == false) 
        {
            pauseUI.SetActive(true);
        }
       
        Time.timeScale = 0f;
 

    }


    void OffPause()
    {
        if(pauseUI.gameObject.activeSelf==true)
        {
            pauseUI.SetActive(false);
           
        }
        
        Time.timeScale = 1f;
    }

    private void MoveSelect()
    {
        //ある座標に向かって移動アニメーション追加予定


        //移動先
        if (Input.GetKeyDown("up"))
        {
            move -= 1;
            //SE追加
        }
        if (Input.GetKeyDown("down"))
        {
            move += 1;
            //SE追加
        }
        //Pause選択数分超えないようにループ
        if (move > 2)
        {
            move = 0;
        }
        if (move < 0)
        {
            move = 2;
        }



        vec_Cursor = Cursor.transform.localPosition;
        //Pause画面セレクト指移動
        switch (move)
        {
            case 0://バック位置
                //指定
                vec_Cursor.y = 1.5f;     //仮置き
                //selectFlg = ture;
                break;
            case 1://リスタート位置
                vec_Cursor.y = 0f;     //仮置き
                //selectFlg = ture;
                break;
            case 2://ステセレ位置
                vec_Cursor.y = -1.0f;     //仮置き
                //selectFlg = ture;
                break;
        }

        Cursor.transform.localPosition = vec_Cursor;
        
    }

}


