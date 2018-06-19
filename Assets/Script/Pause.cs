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
    public GameObject fade;
    public GameObject movepause;
    public GameObject MainScript;
    bool StickFlag = false;
    bool moved = false;
    int count = 0;

    //public enum PouseState { Back, Restart, Stageselect };
    //PouseState state;
    public int move;    
    public int move_Max; //
    private int movepause_Oncount = 0;
    private int fade_count = 0;
    public static bool fade_outflg = false;
    public static bool BackStageSelect_flg = false;
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
        {
            is_pause = true;
        }

        if (is_pause==true)
        {            
            SetPause();
            MoveSelect();
        }

        if(is_pause==true && Input.GetButtonDown("BButton"))//Input.GetKeyDown("w")
        {
            OffPause();
            is_pause = false;
        }

        if (Input.GetButtonDown("AButton") && is_pause)
        {
            is_pause = false;
            if(MainScript.GetComponent<GameMain>().TutorialFlg==true)
            {
                OffPause();
                return;
            }
            switch (move)
            {
                case 0:
                    OffPause();
                    break;
                case 1:
                    RestartLoad();
                    break;
                case 2:
                    FedeIn();
                    BackStageSelect_flg = true;
                    //BackStageSelect();
                    break;
                default:
                    OffPause();
                    break;
            }
        }
        //if (fade_outflg == true && fade_count>30)
        //{
        //    //fade_outflg = false;
        //    fade_count = 0;
        //    FedeOut();
        //}
        //else
        //{
        //    fade_count++;
        //}

        FedeOut();  
        BackStageSelect();
        
        
        //gameObject.SetActive(false); //非活性化

    }

    private void RestartLoad()//リスタート
    {

        FedeIn();
        gameObject.GetComponent<GameMain>().Restart();
        OffPause();    
        
    }

    private void BackStageSelect()
    {

        //FedeIn();
        
        if (fade_outflg == true && fade_count > 20 && BackStageSelect_flg == true)
        {
            BackStageSelect_flg = false;
            OffPause();
            //セレクトへ遷移処理
            SceneManager.LoadSceneAsync("StageSelect");
        }


    }

    private void SetPause()
    {
        //アニメーション追加予定
        //　ポーズUIのアクティブ、非アクティブを切り替え
        
        if (pauseUI.gameObject.activeSelf == false) 
        {
            pauseUI.SetActive(true);
            Sound.PlaySe("se_paper", 7);
            movepause_Oncount = 0;
        }
        //アニメーションカウント
        if (movepause_Oncount < (1 / movepause.GetComponent<MovePose>().SlideSpeed))
        {
            movepause_Oncount++;
            movepause.GetComponent<MovePose>().SlideOn_Off = true;
        }

        Time.timeScale = 0f;
    }


    void OffPause()
    {
        Sound.PlaySe("se_cancel", 5);
        //アニメーションカウント
        if (movepause_Oncount >= (1 / movepause.GetComponent<MovePose>().SlideSpeed))
        {
            movepause_Oncount = 0;
        }
        if (movepause_Oncount < (1 / movepause.GetComponent<MovePose>().SlideSpeed))
        {
            movepause_Oncount++;
            movepause.GetComponent<MovePose>().SlideOn_Off = true;
        }
        //ポーズ画面非アクティブ化
        if (pauseUI.gameObject.activeSelf==true&& movepause_Oncount == (1 / movepause.GetComponent<MovePose>().SlideSpeed))
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

            if (Distance < -0.5f || Distance > 0.5f)
            {          
                StickFlag = true;
            }
            else
            {
                StickFlag = false;
                moved = false;
            }

            if(StickFlag == true && moved == false && Distance < -0.5f || Input.GetKeyDown("down"))
            {
                move += 1;             
                moved = true;
            }

            if (StickFlag == true && moved == false && Distance > 0.5f || Input.GetKeyDown("up"))
            {
                move -= 1;              
                moved = true;
            }

            Sound.PlaySe("se_select", 6);
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
        

        vec_Cursor = Cursor.transform.localPosition;
        //Pause画面セレクト指移動
        switch (move)//位置仮置き
        {
            case 0://バック位置
                vec_Cursor.x = -7.17f;
                vec_Cursor.y = 0.37f;
                break;
            case 1://リスタート位置
                vec_Cursor.x = -7.17f;
                vec_Cursor.y = -1.16f;     
                break;
            case 2://ステセレ位置
                vec_Cursor.x = -7.17f;
                vec_Cursor.y = -3f;   
                break;
            //case 3://before位置
            //    vec_Cursor.x = -6.5f;
            //    vec_Cursor.y = -4.5f;     
            //    break;
            //case 4://next位置
            //    vec_Cursor.x = 2.0f;
            //    vec_Cursor.y = -4.5f;
            //    break;
        }


        Cursor.transform.localPosition = vec_Cursor;        
    }


    //維持
    //if (Pause.is_pause){return;}


    public void FedeIn()
    {
        //fade.GetComponent<failed>().In = true;
        //if (fade_count >= (1 / fade.GetComponent<failed>().FadeSpeed))
        //{
        //    //fade_count = 0;
        //    fade_outflg = true;
        //    return true;
        //}
        //fade_count++;
        //return false;

        fade.GetComponent<failed>().In = true;
        fade_outflg = true;
        fade_count++;
    }

    public void FedeOut()
    {
        //fade.GetComponent<failed>().Out = true;
        //if (fade_count > (1 / fade.GetComponent<failed>().FadeSpeed))
        //{
        //    fade_count = 0;
        //    fade_outflg = false;
        //    return true;
        //}
        //fade_count++;
        //return false;

        if (fade_outflg == true)
        {
            fade_count++;
        }

        if (fade_count > 30)
        {
            fade_count = 0;
            fade_outflg = false;
            fade.GetComponent<failed>().Out = true;
            //カーソル位置初期化
            vec_Cursor.x = -7.17f;
            vec_Cursor.y = 0.37f;
            Cursor.transform.localPosition = vec_Cursor;
            move = 0;
        }
    }


}


