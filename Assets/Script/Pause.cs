using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{

    public static bool is_pause = false;
    //　ポーズした時に表示するUI
    [SerializeField]
    private GameObject pauseUI;
    public GameObject Cursor;
    public GameObject fade;
    public GameObject movepause;
    public GameObject MainScript;
    public MapScript mapScript;
    public StageSelect StageSelectScript;
    public TitleFade TitleFadeScript;
    //public StageSelectFade StageSelectFadeFlg;
    bool StickFlag = false;
    bool KeyUpFlag = false;
    bool KeyDownFlag = false;
    bool moved = false;
    // int count = 0;  使わない変数はとりあえずコメント化　--6/25 李--

    //public enum PouseState { Back, Restart, Stageselect };
    //PouseState state;
    private string currentScene;
    private int movepause_Oncount = 0;
    private int fade_count = 0;
    public int move;
    public int move_Max; //
    public int fade_countMax;
    public static bool fade_outflg = false;
    public static bool BackStageSelect_flg = false;
    public static bool Restart_flg = false;
    public static bool BackTitle_flg = false;
    public static bool ExeEnd_flg = false;
    public static bool cancel_flg = false;
    Vector3 vec_Cursor;

    public LineAnimetion Line;

    // Use this for initialization
    void Start()
    {
        move = 0;
        move_Max = 2;
        fade_countMax = 20;
        is_pause = false;
        //BackStageSelect_flg = false;
        Sound.LoadSe("se_cancel", Sound.SearchFilename(Sound.eSoundFilename.PS_Cancel));
        Sound.LoadSe("se_enter", Sound.SearchFilename(Sound.eSoundFilename.PS_Enter));
        Sound.LoadSe("se_paper", Sound.SearchFilename(Sound.eSoundFilename.PS_Paper));
        Sound.LoadSe("se_select", Sound.SearchFilename(Sound.eSoundFilename.PS_Select));

        //lineObj = GameObject.Find("GameObject/Pause/Pause_Cursor/Poseline").gameObject;
       // LineObj = GameObject.Find("Poseline").gameObject;
        //LineObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //LineObj.transform.localPosition = new Vector3(1.25f, 0.9f, 1.1f);
        //StartCoroutine(LineAnimation(4.7f, AnimTime));
    }

    // Update is called once per frame
    void Update()
    {
  
        if (MapScript.Is_Map == true)
        {
            return;
        }

        

        // 現在読み込んでいるシーンの名前を取得
        currentScene = SceneManager.GetActiveScene().name;
   
        ////ｑキーでゲームバック
        if ((Input.GetKeyDown("q") || Input.GetButtonDown("StartButton"))
            && fade_outflg == false //フェード中オフ 
            && movepause.GetComponent<MovePose>().SlideOn_Off == false) //Animation中オフ
        {
            //GameScene特別な処理
            if (currentScene == "GameMain")
            {
                if(MainScript.GetComponent<GameMain>().ClearFlg == false//クリア中オフ
                    && MainScript.GetComponent<GameMain>().FailFlg == false //失敗中オフ
                    && MainScript.GetComponent<GameMain>().TutorialFlg == false) //チュートリアル中オフ){
                {
                    if (TitleFadeScript.GetComponent<TitleFade>().SceneChangeFlag == true) //タイトル表示中オフ 
                    {
                        Sound.PlaySe("se_paper", 5);
                        is_pause = !is_pause;
                        if (is_pause == false)
                        {
                            cancel_flg = true;
                        }
                        if (is_pause == true)
                        {
                            CursorReset();
                        }
                    }
                }
            }
            
            if(currentScene == "StageSelect")
            {
                if (StageSelectScript.GetComponent<StageSelect>().SelectStageFlag == false //ステージ決定中オフ
                    && fade.GetComponent<StageSelectFade>().FadeInFlag == false)
                {
                    Sound.PlaySe("se_paper", 7);
                    is_pause = !is_pause;
                    if (is_pause == false)
                    {
                        cancel_flg = true;
                    }
                    if (is_pause == true)
                    {
                        CursorReset();
                    }
                }

            }
        }


        if (is_pause == true)
        {
            SetPause();
            MoveSelect();
        }


        if ((is_pause == true && (Input.GetButtonDown("BButton")) || cancel_flg == true))//cancel_flg = true 
        {
            OffPause();
            is_pause = false;
            cancel_flg = false;
            //Sound.PlaySe("se_paper", 5);
            //CursorReset();
        }

        if (Input.GetButtonDown("AButton") && is_pause == true)
        {
            is_pause = false;
            //Sound.PlaySe("se_enter", 8);
            //if (MainScript.GetComponent<GameMain>().TutorialFlg == true)
            //{
            //    OffPause();
            //    return;
            //}
            switch (move)
            {
                case 0:

                    if (currentScene == "GameMain")//GameScene特別な処理
                    {
                        OffPause();
                        Sound.PlaySe("se_paper", 5);
                    }
                    else
                    {
                        Sound.PlaySe("se_enter", 8);
                        fade.GetComponent<StageSelectFade>().FadeOutFlag= true;
                        BackTitle_flg = true;
                    }
                    break;
                case 1:
                    //Sound.PlaySe("se_enter", 8);
                    if (currentScene == "GameMain")//GameScene特別な処理
                    {
                        Sound.PlaySe("se_enter", 8);
                        FedeIn();
                        Restart_flg = true;
                        //RestartLoad();
                    }
                    else
                    {
                        Sound.PlaySe("se_enter", 8);
                        fade.GetComponent<StageSelectFade>().FadeOutFlag = true;
                        ExeEnd_flg = true;
                    }
                    break;
                case 2:
                    //Sound.PlaySe("se_enter", 8);
                    if (currentScene == "GameMain")//GameScene特別な処理
                    {
                        Sound.PlaySe("se_enter", 8);
                        FedeIn();
                        BackStageSelect_flg = true;
                        //BackStageSelect();
                    }
                    else
                    {
                    }
   
                    break;
                default:
                    OffPause();
                    break;
            }
        }



        //if (movepause.GetComponent<MovePose>().SlideOn_Off == false && is_pause == false)
        //{
        //    //CursorReset();
        //}
        if (currentScene == "GameMain")//GameScene特別な処理
        {
            BackStageSelect();
            RestartLoad();
        }
        else
        {
            BackTitle();
            ExeEnd();
        }

    }

    private void RestartLoad()//リスタート
    {
        if (Restart_flg == true && fade_outflg == true && fade_count > (fade_countMax / 3))
        {
            //FedeIn();
            Restart_flg = false;
            gameObject.GetComponent<GameMain>().Restart();
            OffPause();
        }
        else
        {
            FedeOut();
        }
    }

    private void BackStageSelect()
    {

        //FedeIn();

        if (BackStageSelect_flg == true 
            && fade_outflg == true 
            && fade_count > (fade_countMax / 3))
        {
            OffPause();
            BackStageSelect_flg = false;
            pauseUI.SetActive(false);
            is_pause = false;
            fade_outflg = false;
            //セレクトへ遷移処理
            SceneManager.LoadSceneAsync("StageSelect", LoadSceneMode.Single);
        }
    }

    private void BackTitle()
    {
        if (BackTitle_flg == true
             && fade.GetComponent<StageSelectFade>().FadeOutFlag == false)
        {
            OffPause();
            is_pause = false;
            BackTitle_flg = false;
            fade_outflg = false;
            movepause.GetComponent<MovePose>().SlideOn_Off = false;
            PassStageID.GetStageID(0);//ステージ位置初期化
            //タイトルへ遷移処理
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }

    }

    private void ExeEnd()
    {
  
        if (ExeEnd_flg == true
             && fade.GetComponent<StageSelectFade>().FadeOutFlag == false)
        {
            is_pause = false;
            //終了処理
            Application.Quit();
            //exe以外ならタイトルへ処理
            ExeEnd_flg = false;
            PassStageID.GetStageID(0);//ステージ位置初期化
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }
    }

    private void SetPause()
    {
        //アニメーション追加予定
        //　ポーズUIのアクティブ、非アクティブを切り替え

        if (pauseUI.gameObject.activeSelf == false)
        {
            pauseUI.SetActive(true);
            movepause_Oncount = 0;
            //Sound.PlaySe("se_paper", 7);
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
        //Sound.PlaySe("se_cancel", 5);
        if (BackStageSelect_flg == false)
        {
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
        }

        //ポーズ画面非アクティブ化
        if (pauseUI.gameObject.activeSelf == true && movepause_Oncount >= (1 / movepause.GetComponent<MovePose>().SlideSpeed))
        {
            pauseUI.SetActive(false);
        }

        is_pause = false;
        Time.timeScale = 1f;
    }

    private void MoveSelect()
    {
        float Distance = Input.GetAxisRaw("LeftStick Y");


        //キー移動判定
        if (Input.GetKeyDown("up"))
        {
            KeyUpFlag = true;
            moved = false;
            CursorUP();

        }

        if (Input.GetKeyDown("down"))
        {
            KeyDownFlag = true;
            moved = false;
            CursorDown();

            //Debug.Log("KeyDownON");
        }

        if (KeyUpFlag == false || KeyUpFlag == false)
        {

            //コントローラ移動判定
            if (Distance != 0)
            {
                if (Distance < -0.5f || Distance > 0.5f)
                {
                    StickFlag = true;
                    //if (Distance < -0.5f)
                    //{
                    //    KeyDownFlag = true;
                    //}
                    //if (Distance > 0.5f)
                    //{
                    //    KeyUpFlag = true;
                    //}
                }
                else
                {
                    StickFlag = false;
                    moved = false;
                    //Debug.Log("KeyOFF");
                }

                if (moved == false && StickFlag == true && Distance > 0.5f)
                {
                    CursorUP();
                    //CursorDown();
                }
                if (moved == false && StickFlag == true && Distance < -0.5f)
                {
                    CursorDown();
                    //CursorUP();
                }

            }
        }
        else
        {
            KeyDownFlag = false;
            KeyUpFlag = false;
        }


   
        /*
        //移動判定
        //if (moved == false && ((StickFlag == true && Distance < -0.5f) || KeyUpFlag == true))
        //{
        //    move -= 1;
        //    moved = true;
        //    KeyUpFlag = false;
        //    KeyDownFlag = false;
        //    InitLineAnimation();     //アニメーション初期化
        //    Sound.PlaySe("se_select", 6);
        //    Debug.Log("KeyUpMove");
        //}
        //if (moved == false && KeyUpFlag == true)
        //{
        //    move -= 1;
        //    moved = true;
        //    KeyUpFlag = false;
        //    KeyDownFlag = false;
        //    InitLineAnimation();     //アニメーション初期化
        //    Sound.PlaySe("se_select", 6);
        //    Debug.Log("KeyUpMove");
        //}
        //if (moved == false && ((StickFlag == true && Distance > 0.5f) || KeyDownFlag == true))
        //{
        //    move += 1;
        //    moved = true;
        //    KeyUpFlag = false;
        //    KeyDownFlag = false;
        //    InitLineAnimation();     //アニメーション初期化
        //    Sound.PlaySe("se_select", 6);
        //    //Debug.Log("KeyDownMovw");
        //}
        //if (moved == false &&  KeyDownFlag == true)
        //{
        //    move += 1;
        //    moved = true;
        //    KeyUpFlag = false;
        //    KeyDownFlag = false;
        //    InitLineAnimation();     //アニメーション初期化
        //    Sound.PlaySe("se_select", 6);
        //    //Debug.Log("KeyDownMovw");
        //}
        //if (moved == false && KeyUpFlag == true)
        //{
        //    CursorUP();
        //}
        //if (moved == false && KeyDownFlag == true)
        //{
        //    CursorDown();
        //}
        */

        //Pause選択数分超えないようにループ
        if (currentScene == "GameMain")
        {
            if (move > move_Max)
            {
                move = 0;
            }
            if (move < 0)
            {
                move = move_Max;
            }
        }
        else
        {
            if (move > (move_Max-1))
            {
                move = 0;
            }
            if (move < 0)
            {
                move = move_Max-1;
            }
        }
 


        vec_Cursor = Cursor.transform.localPosition;
        //Pause画面セレクト指移動
        switch (move)//位置仮置き
        {
            case 0://バック位置
                vec_Cursor.x = -8.3f;
                vec_Cursor.y = -2.53f;
                //CursorReset();
                break;
            case 1://リスタート位置
                vec_Cursor.x = -8.3f;
                vec_Cursor.y = -3.54f;
                break;
            case 2://ステセレ位置
                vec_Cursor.x = -8.3f;
                vec_Cursor.y = -4.59f;
                break;
        }
        Cursor.transform.localPosition = vec_Cursor;

     }


    //維持
    //if (Pause.is_pause){return;}


    public void FedeIn()
    {
        fade.GetComponent<failed>().FadeIn_On();
        fade_outflg = true;
        fade_count++;
    }

    public void FedeOut()
    {

        if (fade_outflg == true)
        {
            fade_count++;
        }

        if (fade_count > 30)
        {
            fade_count = 0;
            fade_outflg = false;
            fade.GetComponent<failed>().FadeOut_On();
            CursorReset();
        }
    }

    public void CursorReset()
    {
        //カーソル位置初期化
        vec_Cursor.x = -8.3f;
        vec_Cursor.y = -2.53f;
        Cursor.transform.localPosition = vec_Cursor;
        move = 0;
        moved = false;
    }

    private void CursorUP()
    {
        move -= 1;
        moved = true;
        //KeyUpFlag = false;
        //KeyDownFlag = false;
        Line.InitLineAnimation();     //アニメーション初期化
        Sound.PlaySe("se_select", 6);
        //Debug.Log("KeyUpMove");
    }

    private void CursorDown()
    {
        move += 1;
        moved = true;
        //KeyUpFlag = false;
        //KeyDownFlag = false;
        Line.InitLineAnimation();     //アニメーション初期化
        Sound.PlaySe("se_select", 6);
        //Debug.Log("KeyDownMovw");
    }

    //private IEnumerator LineAnimation(float endScale, float animTime)
    //{
    //    // 排他制御
    //    if (isLineAnim)
    //    {
    //        yield break;
    //    }

    //    isLineAnim = true;

    //    LineObj.transform.localPosition = new Vector3(LineObj.transform.localPosition.x - LineObj.transform.localScale.x / 2, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
    //    LineObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

    //    float time = 0.0f;
    //    float tmp = 0.0f;
    //    float basePos;
    //    Transform tmpTrans;
    //    tmpTrans = LineObj.transform;
    //    basePos = tmpTrans.transform.localPosition.x;

    //    while(time < animTime)
    //    {

    //        time += Time.deltaTime;

    //        // 時間当たりの割合
    //        tmp = (time / animTime) * endScale;

    //        // 範囲外の値を反映させない用
    //        if (tmp > endScale)
    //        {
    //            break;
    //        }

    //        LineObj.transform.localScale = new Vector3(tmp, LineObj.transform.localScale.y, LineObj.transform.localScale.z);
    //        LineObj.transform.localPosition = new Vector3(LineObj.transform.localScale.x / 2, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
    //        LineObj.transform.localPosition = new Vector3(LineObj.transform.localPosition.x + basePos, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);

    //        yield return false;
    //    }

    //    // 補正処理
    //    LineObj.transform.localScale = new Vector3(endScale, LineObj.transform.localScale.y, LineObj.transform.localScale.z);
    //    LineObj.transform.localPosition = new Vector3(LineObj.transform.localScale.x / 2, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
    //    LineObj.transform.localPosition = new Vector3(LineObj.transform.localPosition.x + basePos, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);

    //    isLineAnim = false;
    //    yield return true;
    //}Animation
    /*
    private void LineAnimation(float endScale, float animTime)
    {
        //if (!isInit)
        //{
        //    InitLineAnimation();
        //    isInit = true;
        //}

        if (tmpTime < animTime)
        {
            // todo : ここをタイム関係とは別の変数で計算してほしい。
            tmpTime += AnimeStatoTime;//Time.deltaTime;

            // 時間当たりの割合
            tmpScale = (tmpTime / animTime) * endScale;

            // 範囲外の値を反映させない用
            if (tmpScale > endScale)
            {
                //moved = false;
            }
            else
            {
                LineObj.transform.localScale = new Vector3(tmpScale, LineObj.transform.localScale.y, LineObj.transform.localScale.z);
                LineObj.transform.localPosition = new Vector3(LineObj.transform.localScale.x * 2, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
                LineObj.transform.localPosition = new Vector3(LineObj.transform.localPosition.x + basePos, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
            }

        }

        if (tmpTime > AnimTime)
        {
            // 補正処理
            LineObj.transform.localScale = new Vector3(endScale, LineObj.transform.localScale.y, LineObj.transform.localScale.z);
            LineObj.transform.localPosition = new Vector3(LineObj.transform.localScale.x * 2, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
            LineObj.transform.localPosition = new Vector3(LineObj.transform.localPosition.x + basePos, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
            //isInit = false;
            isLineAnim = false;
        }

    }

    private void InitLineAnimation()
    {
        LineObj.transform.localPosition = new Vector3(LineObj.transform.localPosition.x - LineObj.transform.localScale.x * 2, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
        LineObj.transform.localScale = new Vector3(0.0f,1.0f, 0.1f);

        tmpTime = 0.0f;
        tmpScale = 0.0f;
        tmpTrans = LineObj.transform;
        basePos = tmpTrans.transform.localPosition.x;
        isLineAnim = true;
        //moved = false;
    }
    */
}


