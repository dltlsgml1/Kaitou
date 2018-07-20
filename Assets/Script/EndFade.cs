using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFade : MonoBehaviour
{
    public GameObject ClearObj;
    public GameObject FailedObj;
    public GameObject FogObj;

    public bool ClearStartFlag = false;
    public bool FailedStartFlag = false;

    public bool ClearEndFlag = false;
    public bool FailedEndFlag = false;

    public bool alphaFlag = true;
    public bool emissionUpFlag = false;
    public bool emissionDownFlag = false;

    public bool endFlag = false;

    public float FadeS = 0.2f;
    public float FailedFadeS = 0.1f;
    public float FogEmissionFadeUp = 0.3f;
    public float FogEmissionFadeDown = 0.1f;

    private Color ClearFadeSpeed;
    private Color FogFadeSpeed;
    private Color FailedFadeSpeed;


    public bool ChangeEmissionFlag = false;      //点滅し始めるフラグ
    float time = 0;                             //秒数計算用
    float timeCount = 0;                        //秒数計算用
    
    public float MiniEmission = 0.0f;
    public float MaxEmission = 0.3f;

    public double UpSpeed = 0.1f;
    public double DownSpeed = 0.1f;

    //時間確認
    public bool stopFlag = false;     //次のシーン行くまで待機的なフラグ
    private float countTime = 0.0f;
    private float endTime = 1.0f;
    private float ClearFadeTime = 2.0f;
    public float FailedFadeTime = 2.0f;
    bool endChake = false;

    //Fadeアウトする
    public bool SceneChangeFlag = false;

    //ゲームのClear判定
    public GameMain MainScript;
    public failed ClearFade;
    public failed FailedFade;

    // スクショ用
    private ScreenShot SS;
    private bool ScreenshotFlg = false;
    GameObject saveObj;

    //クリア・失敗のときのSEフラグ
    bool PlayedSE = false;

    // Use this for initialization
    void Start()
    {
        ClearFadeSpeed = new Color(ClearObj.GetComponent<Renderer>().material.color.r, ClearObj.GetComponent<Renderer>().material.color.g, ClearObj.GetComponent<Renderer>().material.color.b, FadeS);
        FogFadeSpeed = new Color(FogObj.GetComponent<Renderer>().material.color.r, FogObj.GetComponent<Renderer>().material.color.g, FogObj.GetComponent<Renderer>().material.color.b, FadeS);
        FailedFadeSpeed = new Color(FailedObj.GetComponent<Renderer>().material.color.r, FailedObj.GetComponent<Renderer>().material.color.g, FailedObj.GetComponent<Renderer>().material.color.b, FadeS);

        // スクショ用準備
        SS = this.GetComponent<ScreenShot>();
        SS.Init("Stage", "ClearStageSS", "ClearImage");
        saveObj = GameObject.Find("SaveData").gameObject;
        saveObj.GetComponent<ExportCsvScript>().SetStageId(GameObject.Find("StagePrefab").GetComponent<MainStageLoad>().StageID);
    }

    // Update is called once per frame
    void Update()
    {
        if(MainScript.ClearFlg && !endChake)
        {
            countTime += Time.deltaTime;

            if(ClearFadeTime / 2 <= countTime)
            {
                SaveAndScreenshot();
                GlobalCoroutine.Go(SS.CreateScreenshot("ResultImage"));
            }

            if (ClearFadeTime <= countTime && !ClearFade.In)
            {
                countTime = 0.0f;
                endChake = true;
                ClearFade.FadeIn_On();
            }
        }
        if (MainScript.FailFlg && !endChake)
        {
            countTime += Time.deltaTime;
            if (FailedFadeTime <= countTime && !FailedFade.In)
            {
                countTime = 0.0f;
                endChake = true;
                FailedFade.FadeIn_On();
            }
        }

        if (MainScript.ClearFlg && ClearFade.FadeInEnd)
        {
            ClearStartFlag = MainScript.ClearFlg;

        }
        if (MainScript.FailFlg && FailedFade.FadeInEnd)
        {
            FailedStartFlag = MainScript.FailFlg;

        }
        if (ClearStartFlag)
        {
            Sound.StopBgm();
            if (PlayedSE == false)
            {
                PlayedSE = true;
                Sound.PlaySe("SE_CLEAR");
            }
            ClearAnima();
        }
        if (FailedStartFlag)
        {
            Sound.StopBgm();
            if (PlayedSE == false)
            {
                PlayedSE = true;
                Sound.PlaySe("SE_FAIL");
            }
            FailedAnima();
        }

        if (ClearEndFlag || FailedEndFlag)
        {
            SceneChange();
        }
    }

    void ClearAnima()
    {
        //alphaを上げる
        if (alphaFlag)
        {
            ClearFadeSpeed.a += FadeS;
            FogFadeSpeed.a += FadeS;

            ClearObj.GetComponent<Renderer>().material.color = ClearFadeSpeed;
            FogObj.GetComponent<Renderer>().material.color = FogFadeSpeed;
            if (ClearObj.GetComponent<Renderer>().material.color.a >= 1.0f && FogObj.GetComponent<Renderer>().material.color.a >= 1.0f)
            {
                alphaFlag = false;
                ChangeEmissionFlag = true;
                emissionUpFlag = true;

            }
        }

        if (ChangeEmissionFlag)
        {

            time = Mathf.PingPong(timeCount, MaxEmission + 0.1f); //これでマックスエミッションまで行き来するようにして

            //ここでスピード調整して行き来する。
            if (emissionUpFlag)
            {
                timeCount += (float)UpSpeed;
            }
            if (emissionDownFlag)
            {
                timeCount -= (float)DownSpeed;
            }


            float val = time;
            //float num = val * val;  使わない変数はとりあえずコメント化　--6/25 李--
            // Color color = new Color(val * val, val * val, val * val);
            Color color = new Color(val, val, val); //エミッションの光度を変えてる。
            FogObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", color); //ここで色を入れ込む。



            //光るか光らなくなるかを見てる
            if (time <= MaxEmission && !emissionDownFlag)
            {
            }
            else
            {
                if (emissionDownFlag)
                {

                }
                else
                {
                    emissionUpFlag = false;
                    emissionDownFlag = true;
                }
            }
            if (time >= MiniEmission && !emissionUpFlag)
            {
            }
            else
            {
                if (emissionUpFlag)
                {

                }
                else
                {
                    emissionUpFlag = true;
                    emissionDownFlag = false;
                    ChangeEmissionFlag = false;
                    ClearEndFlag = true;
                }
            }
        }
    }

    void FailedAnima()
    {
        //alphaを上げる
        if (alphaFlag)
        {
            FailedFadeSpeed.a += FailedFadeS;

            FailedObj.GetComponent<Renderer>().material.color = FailedFadeSpeed;
            if (FailedObj.GetComponent<Renderer>().material.color.a >= 1.0f)
            {
                alphaFlag = false;

                FailedEndFlag = true;

            }
        }
    }

    void FailedOuntAnima()
    {
        FailedFadeSpeed.a -= FailedFadeS;
        FailedObj.GetComponent<Renderer>().material.color = FailedFadeSpeed;

        if (FailedObj.GetComponent<Renderer>().material.color.a <= 0.0f)
        {
            SceneChangeFlag = true;
        }

    }

    void ClearOuntAnima()
    {
        ClearFadeSpeed.a -= FadeS;
        FogFadeSpeed.a -= FadeS;

        ClearObj.GetComponent<Renderer>().material.color = ClearFadeSpeed;
        FogObj.GetComponent<Renderer>().material.color = FogFadeSpeed;



        if (ClearObj.GetComponent<Renderer>().material.color.a <= 0.0f && FogObj.GetComponent<Renderer>().material.color.a <= 0.0f)
        {
            SceneChangeFlag = true;
        }

    }

    void SceneChange()
    {
        countTime += Time.deltaTime;
        if(endTime <= countTime)
        {
            stopFlag = false;

            if(ClearEndFlag)
            {
                ClearOuntAnima();
            }
            if (FailedEndFlag)
            {
                FailedOuntAnima();
            }
            
        }

        if(SceneChangeFlag)
        {
            //数値初期化する必要あるならここ！

            //Sceneチェンジ
            MainScript.FadeEnd = true;
        }

    }

    void SaveAndScreenshot()
    {
        // スクショ作成とセーブデータ更新
        if (!ScreenshotFlg)
        {
            if(GameObject.Find("MainSceneScript").GetComponent<GameMain>().ClearLimit <= saveObj.GetComponent<ExportCsvScript>().GetClearData(saveObj.GetComponent<ExportCsvScript>().GetNowStageId()))
            {
                GlobalCoroutine.Go(SS.CreateClearImage(saveObj.GetComponent<ExportCsvScript>().GetNowStageId()));
                saveObj.GetComponent<ExportCsvScript>().SetClearData(GameObject.Find("MainSceneScript").GetComponent<GameMain>().ClearLimit);
                saveObj.GetComponent<ExportCsvScript>().WriteFile();
            }

            ScreenshotFlg = true;
        }

    }
}
