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


    //public enum PouseState { Back, Restart, Stageselect };
    //PouseState state;
    public int move;    
    public int move_Max; //

    Vector3 vec_Cursor;//= Cursor.transform.localPosition;  

    // Use this for initialization
    void Start () {
        move = 0;
        move_Max = 2;
        Sound.LoadSe("se_cancel", Sound.SearchFilename(Sound.eSoundFilename.PS_Cancel));
        Sound.LoadSe("se_enter", Sound.SearchFilename(Sound.eSoundFilename.PS_Enter));
        Sound.LoadSe("se_paper", Sound.SearchFilename(Sound.eSoundFilename.PS_Paper));
        Sound.LoadSe("se_select", Sound.SearchFilename(Sound.eSoundFilename.PS_Select));

    }
	
	// Update is called once per frame
	void Update () {


        ////ｑキーでゲームバック
        if (Input.GetKeyDown("q") || Input.GetButtonDown("StartButton"))
        //if(Input.GetButtonDown("STARTButton"))
        {
            is_pause = true;
        }


        if(is_pause==true)
        {            
            SetPause();
            MoveSelect();
        }

        if(is_pause==true && Input.GetButtonDown("AButton"))//Input.GetKeyDown("w")
        {
            OffPause();
            is_pause = false;
        }

        if (Input.GetButtonDown("BButton") && is_pause)
        //    if (Input.GetKeyDown("space") && is_pause)  //決定キーに差し替え予定
        {
            is_pause = false;
            switch (move)
            {
                case 0:
                    OffPause();
                    break;
                case 1:
                    RestartLoad();
                    break;
                case 2:
                    BackStageSelect();
                    break;
                default:
                    OffPause();
                    break;
            }
        }

        //gameObject.SetActive(false); //非活性化

	}

    private void RestartLoad()      //リスタート
    {
        //RestartLoad
        //アニメーション追加予定
        //Sound.PlaySe("se_enter", 4);

        OffPause();

        //リスタート初期化関数追加予定

    }

    private void BackStageSelect()
    {
        //アニメーション追加予定
        //Sound.PlaySe("se_enter", 4);
        OffPause();
        //セレクトへ遷移処理
        SceneManager.LoadSceneAsync("StageSelect");
    }

    private void SetPause()
    {
        //アニメーション追加予定
        //　ポーズUIのアクティブ、非アクティブを切り替え

        //Debug.Log("im in Setpause");
        if (pauseUI.gameObject.activeSelf == false) 
        {
            pauseUI.SetActive(true);
            Sound.PlaySe("se_paper", 7);
        }
       
        Time.timeScale = 0f;
    }


    void OffPause()
    {
        Sound.PlaySe("se_cancel", 5);
        if (pauseUI.gameObject.activeSelf==true)
        {
            pauseUI.SetActive(false);           
        }
        is_pause = false;
        Time.timeScale = 1f;
    }

    private void MoveSelect()
    {
        //ある座標に向かって移動アニメーション追加予定
        float Distance = Input.GetAxisRaw("LeftStick Y");

        //移動先
        if (Distance != 0) {
            if(Distance > -0.5)
            {
                if(Input.GetButtonDown("LeftStick Y"))
                {
                    move -= 1;
                    //SE追加
                    Sound.PlaySe("se_select", 6);
                }
            }
            if (Distance < 0.5)
            {
                if (Input.GetButtonDown("LeftStick Y"))
                {
                    move += 1;
                    //SE追加
                    Sound.PlaySe("se_select", 6);                
                }
            }

        }


        /*if (Input.GetKeyDown("up"))
        {
            move -= 1;
            //SE追加
            Sound.PlaySe("se_select", 6);
        }
        if (Input.GetKeyDown("down"))
        {
            move += 1;
            //SE追加
            Sound.PlaySe("se_select", 6);
        }*/
        //Pause選択数分超えないようにループ
        if (move > move_Max)
        {
            move = 0;
        }
        if (move < 0)
        {
            move = move_Max;
        }

        Debug.Log("move" + move);

        vec_Cursor = Cursor.transform.localPosition;
        //Pause画面セレクト指移動
        switch (move)//位置仮置き
        {
            case 0://バック位置
                vec_Cursor.x = -7.7f;
                vec_Cursor.y = 1.5f;
                break;
            case 1://リスタート位置
                vec_Cursor.x = -7.7f;
                vec_Cursor.y = 0f;     
                break;
            case 2://ステセレ位置
                vec_Cursor.x = -7.7f;
                vec_Cursor.y = -1.0f;   
                break;
            case 3://before位置
                vec_Cursor.x = -6.5f;
                vec_Cursor.y = -4.5f;     
                break;
            case 4://next位置
                vec_Cursor.x = 2.0f;
                vec_Cursor.y = -4.5f;
                break;
        }

        //移動アニメーション
        //MoveAnimation();

        Cursor.transform.localPosition = vec_Cursor;        
    }

    //public void MoveAnimation()
    //{
    //    float beforeposition=0;
    //    float afterposition=0;
        
    //    if (Animation_count== 0)
    //    {
    //        vec_Cursor.x+=(afterposition - beforeposition) / 10.0f;
    //    }

    //}
        

    //維持関数
    public static void StopMode()
    {
        if (is_pause)
        { 

            Debug.Log("im in_StopMode");
            return ;
           
        }
        Debug.Log("im Out_StopMode");

    }
    //Pause.StopMode();
    //if (Pause.is_pause){return;}
}


