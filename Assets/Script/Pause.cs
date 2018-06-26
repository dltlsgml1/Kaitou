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
    bool StickFlag = false;
    bool moved = false;
    // int count = 0;  使わない変数はとりあえずコメント化　--6/25 李--

    //public enum PouseState { Back, Restart, Stageselect };
    //PouseState state;
    public int move;
    public int move_Max; //
    private int movepause_Oncount = 0;
    private int fade_count = 0;
    public int fade_countMax;
    public static bool fade_outflg = false;
    public static bool BackStageSelect_flg = false;
    public static bool Restart_flg = false;
    public static bool BackTitle_flg = false;
    public static bool ExeEnd_flg = false;
    public static bool cancel_flg = false;
    Vector3 vec_Cursor;

    // カーソルアニメーション
    public GameObject LineObj;
    public float AnimTime = 1.0f;
    public float AnimeStatoTime = 0.5f;
    public bool isLineAnim = false;

    private float tmpTime = 0.0f;
    private float tmpScale = 0.0f;
    private float basePos;
    private Transform tmpTrans;
    private bool isInit = false;

    // Use this for initialization
    void Start()
    {
        move = 0;
        move_Max = 2;
        fade_countMax = 20;

        //BackStageSelect_flg = false;
        Sound.LoadSe("se_cancel", Sound.SearchFilename(Sound.eSoundFilename.PS_Cancel));
        Sound.LoadSe("se_enter", Sound.SearchFilename(Sound.eSoundFilename.PS_Enter));
        Sound.LoadSe("se_paper", Sound.SearchFilename(Sound.eSoundFilename.PS_Paper));
        Sound.LoadSe("se_select", Sound.SearchFilename(Sound.eSoundFilename.PS_Select));

        //lineObj = GameObject.Find("GameObject/Pause/Pause_Cursor/Poseline").gameObject;
        LineObj = GameObject.Find("Poseline").gameObject;
        //LineObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //LineObj.transform.localPosition = new Vector3(1.25f, 0.9f, 1.1f);
        //StartCoroutine(LineAnimation(4.7f, AnimTime));
    }

    // Update is called once per frame
    void Update()
    {
        // 現在読み込んでいるシーンの名前を取得
        string currentScene = SceneManager.GetActiveScene().name;
   
        // todo : これが初期化　アニメーション前にかならず行う。
        if (Input.GetKeyDown(KeyCode.U))
        {
            InitLineAnimaton();
        }

        if (isLineAnim)
        {
            LineAnimation(4.7f, AnimTime);
        }

        if ((Input.GetKeyDown("0")))
        {
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
        Debug.Log("fade_outflg" + fade_outflg);
        Debug.Log("SlideOn_Off" + movepause.GetComponent<MovePose>().SlideOn_Off);

        ////ｑキーでゲームバック
        if ((Input.GetKeyDown("q") || Input.GetButtonDown("StartButton"))
            && fade_outflg == false //フェード中オフ 
            && movepause.GetComponent<MovePose>().SlideOn_Off == false) //Animation中オフ
        {
            //GameScene特別な処理
            if (currentScene == "GameMain" || currentScene == "Tutorial")
            {
                if(MainScript.GetComponent<GameMain>().ClearFlg == false//クリア中オフ
                && MainScript.GetComponent<GameMain>().TutorialFlg == false) //チュートリアル中オフ){
                {
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
            else//GameScene以外
            {
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
            Sound.PlaySe("se_paper", 5);
            //CursorReset();
        }

        if ((Input.GetKeyDown("y") ||Input.GetButtonDown("AButton")) && is_pause == true)
        {
            is_pause = false;
            Sound.PlaySe("se_enter", 8);
            //if (MainScript.GetComponent<GameMain>().TutorialFlg == true)
            //{
            //    OffPause();
            //    return;
            //}
            switch (move)
            {
                case 0:
                    OffPause();
                    Sound.PlaySe("se_paper", 5);
                    break;
                case 1:
                    if (currentScene == "GameMain")//GameScene特別な処理
                    {
                        FedeIn();
                        Restart_flg = true;
                        //RestartLoad();
                    }
                    else
                    {
                        fade.GetComponent<StageSelectFade>().FadeInFlag = true;
                         BackTitle_flg = true;
                    }
                    break;
                case 2:
                    if (currentScene == "GameMain")//GameScene特別な処理
                    {
                        FedeIn();
                        BackStageSelect_flg = true;
                        //BackStageSelect();
                    }
                    else
                    {
                        fade.GetComponent<StageSelectFade>().FadeInFlag = true;
                        ExeEnd_flg = true;
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
        //Debug.Log("BackTitle_flg" + fade.GetComponent<StageSelectFade>().FadeInFlag);
        if (BackTitle_flg == true)// Todo
        {
            Debug.Log("title");
            OffPause();
            BackTitle_flg = false;
            fade_outflg = false;
            movepause.GetComponent<MovePose>().SlideOn_Off = false;
            //タイトルへ遷移処理
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }

        if (BackTitle_flg == true
            && fade.GetComponent<StageSelectFade>().FadeInFlag == false)
           // && fade_count > (fade_countMax / 3))
        {
            Debug.Log("title");
            OffPause();
            BackTitle_flg = false;
            pauseUI.SetActive(false);
            //タイトルへ遷移処理
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }
    }

    private void ExeEnd()
    {
        //Debug.Log("ExeEnd_flg" + fade.GetComponent<StageSelectFade>().FadeInFlag);
        if (ExeEnd_flg == true)// Todo
        {
            Debug.Log("end");
            //終了処理
            Application.Quit();
            //exe以外ならタイトルへ処理
            ExeEnd_flg = false;
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }

        if (ExeEnd_flg == true
            && fade.GetComponent<StageSelectFade>().FadeInFlag == false)
            //&& fade_count > (fade_countMax / 3))
        {
            Debug.Log("end");
            //終了処理
            Application.Quit();
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
            Sound.PlaySe("se_paper", 7);
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
        //ある座標に向かって移動アニメーション追加予定
        float Distance = Input.GetAxisRaw("LeftStick Y");

        //移動先
        if (Distance != 0)
        {

            if (Distance < -0.5f || Distance > 0.5f)
            {
                StickFlag = true;
            }
            else
            {
                StickFlag = false;
                moved = false;
            }

            if (moved == false && ((StickFlag == true && Distance < -0.5f) || Input.GetKeyDown("down")))
            {
                move += 1;
                moved = true;
                Sound.PlaySe("se_select", 6);
            }

            if (moved == false && ((StickFlag == true && Distance > 0.5f) || Input.GetKeyDown("up")))
            {
                move -= 1;
                moved = true;
                Sound.PlaySe("se_select", 6);
            }

        }


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

        // 動いた先の座標で線のアニメーション座標を初期化
        if (moved)
        {
            InitLineAnimaton();
        }
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
    //}

    private void LineAnimation(float endScale, float animTime)
    {
        //if (!isInit)
        //{
        //    InitLineAnimaton();
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

            }
            else
            {
                LineObj.transform.localScale = new Vector3(tmpScale, LineObj.transform.localScale.y, LineObj.transform.localScale.z);
                LineObj.transform.localPosition = new Vector3(LineObj.transform.localScale.x / 2, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
                LineObj.transform.localPosition = new Vector3(LineObj.transform.localPosition.x + basePos, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
            }

        }

        if (tmpTime > AnimTime)
        {
            // 補正処理
            LineObj.transform.localScale = new Vector3(endScale, LineObj.transform.localScale.y, LineObj.transform.localScale.z);
            LineObj.transform.localPosition = new Vector3(LineObj.transform.localScale.x / 2, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
            LineObj.transform.localPosition = new Vector3(LineObj.transform.localPosition.x + basePos, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
            //isInit = false;
            isLineAnim = false;
        }

    }

    private void InitLineAnimaton()
    {
        LineObj.transform.localPosition = new Vector3(LineObj.transform.localPosition.x - LineObj.transform.localScale.x / 2, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
        LineObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        tmpTime = 0.0f;
        tmpScale = 0.0f;
        tmpTrans = LineObj.transform;
        basePos = tmpTrans.transform.localPosition.x;
        isLineAnim = true;

    }
}


