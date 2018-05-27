using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour {

    public bool is_pause = false;
    //　ポーズした時に表示するUI
    [SerializeField]
    private GameObject pauseUI;



	// Use this for initialization
	void Start () {
       // gameObject.SetActive(true); //活性化
        
            //ゲームタイム維持
	}
	
	// Update is called once per frame
	void Update () {

        //ｑキーでゲームバック
        BackGameMain();


        //ｒキーでリスタート
        if (Input.GetKeyDown("r"))
        {
            Restart();
        }

        //spaceキーでステセレ遷移
        if (Input.GetKeyDown("space"))
        {
            BackStageSelect();
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
   
        if (Input.GetKeyDown("q"))
        {
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
        }

    }





}


