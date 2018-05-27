using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour {

    public bool is_pause = false;
    //　ポーズした時に表示するUI
    [SerializeField]
    private GameObject pauseUI;
    GameObject Cursor;
    PauseSelect pauseselect;


	// Use this for initialization
	void Start () {
   
	}
	
	// Update is called once per frame
	void Update () {

        ////ｑキーでゲームバック
        if (Input.GetKeyDown("q"))
        {
             BackGameMain();
        }

        if (Input.GetKeyDown("space") && is_pause)  //決定キーに差し替え予定
        {
            this.Cursor = GameObject.Find("CameraObejct/Pause/Pause_Cursor");
            this.pauseselect = Cursor.GetComponent<PauseSelect>();

            switch (pauseselect.move)
            {
                case 0:
                    //SE追加予定
                    BackGameMain();
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

    private void BackGameMain()
    {
        //アニメーション追加予定
        //SE追加予定
   
        //if (Input.GetKeyDown("q"))
        //{
            //　ポーズUIのアクティブ、非アクティブを切り替え
            pauseUI.SetActive(!pauseUI.activeSelf);

            //　ポーズUIが表示されてる時は停止
            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;
                is_pause = true;
                //　ポーズUIが表示されてなければ通常通り進行
            }
            else
            {
                Time.timeScale = 1f;
                is_pause = false;
            }
        //}

    }





}


