using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emission : MonoBehaviour
{
<<<<<<< HEAD
    public bool ChangeEmissionFlag = false;      //点滅し始めるフラグ
    public Blocks S_blooks;
    float time = 0;                             //秒数計算用
    float timeCount = 0;                        //秒数計算用
    private float MiniEmission = 0.0f;
    private float MaxEmission = 0.8f;
    private float Maxtime = 0.4f;                       //現状の何秒で変わっていくか
    private float CanBurnMiniEmission = 0.0f;
    private float CanBurnMaxEmission = 0.5f;
    private float CanBurnMaxTime = 0.5f;                 //0.15f;
    public bool LightEnd = false;

    double Speed;
    double canBurnSpeed;
    public bool UpCountFlag = true;                    //明るくするフラグ
    public bool DownCountFlag = false;                 //暗くするフラグ
    private bool OneUCFlag = false;                    //一回だけUpCount処理したいときのフラグ
    public bool canBurnFlag = false;                    //燃えれるインフォメーションのフラグ
                                                        // Use this for initialization
    void Start()
    {
        Speed = MaxEmission / (Maxtime * 60);
        canBurnSpeed = CanBurnMaxEmission / (CanBurnMaxTime * 60);
=======
    // エミッション管理マネージャ
    private EmissionManager EmManager;

    // 内部処理用
    public GameObject EmissiveObj;
    //private Color ObjColor;
    private Color EmissionColor;

    // 時間計算系
    private float NowTime;
    private float ChangeTime;
    private bool UpFlg;

    private float value;

    public bool EndFlg;
    public bool StartFlg;

    private bool isEmissioning;
    private bool Old_isEmissioning;
    public bool CanEmission;
    private bool Old_CanEmission;
    private bool isBaseTimeObj;
    private bool isEnding;

    public Blocks S_Blocks;
    public bool isBurned;
    private bool isBurning;

    // Use this for initialization
    void Start()
    {

        Init();
>>>>>>> Dev

    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        BurnEmisson();
        CanBurnEmission();

        //if(S_blooks.BurnFlg)
        //{
        //    ChangeEmissionFlag = false;
        //    UpCountFlag = true;
        //    DownCountFlag = false;
        //}
    }

    void BurnEmisson()
    {
        if (S_blooks.BurnFlg && UpCountFlag)
        {
            ChangeEmissionFlag = true;
            //UpCountFlag = true;
            //DownCountFlag = false;
        }
        else
        {

            ChangeEmissionFlag = false;
        }
        if (!S_blooks.BurnFlg)
        {
            Color color = new Color(0, 0, 0); //エミッションの光度を変えてる。
            GetComponent<Renderer>().material.SetColor("_EmissionColor", color);　//ここで色を入れ込む。

        }

        if (ChangeEmissionFlag)
        {
            time = Mathf.PingPong(timeCount, MaxEmission + 0.1f); //これでマックスエミッションまで行き来するようにして

            //ここでスピード調整して行き来する。
            if (UpCountFlag)
            {
                timeCount += (float)Speed;
            }
            //if (DownCountFlag)
            //{
            //    timeCount -= (float)Speed;
            //}


            float val = time;
            //float num = val * val;  使わない変数はとりあえずコメント化　--6/25 李--
            // Color color = new Color(val * val, val * val, val * val);
            Color color = new Color(val, val, val); //エミッションの光度を変えてる。
            GetComponent<Renderer>().material.SetColor("_EmissionColor", color);　//ここで色を入れ込む。



            //光るか光らなくなるかを見てる
            if (time < MaxEmission - 0.1f && !DownCountFlag)
            {
            }
            else
            {
                if (DownCountFlag)
                {

                }
                else
                {
                    UpCountFlag = false;
                    DownCountFlag = true;
                    LightEnd = true;
                    ChangeEmissionFlag = false;
                }
            }
            if (time > MiniEmission + 0.2f && !UpCountFlag)
            {
            }
            else
            {
                if (UpCountFlag)
                {

                }
                else
                {
                    UpCountFlag = true;
                    DownCountFlag = false;
                    ChangeEmissionFlag = false;
                }
            }
        }
    }

    void CanBurnEmission()
    {
        //if (canBurnFlag && UpCountFlag)
        //{
        //    ChangeEmissionFlag = true;
        //}
        //else
        //{
        //    ChangeEmissionFlag = false;
        //}
        if (S_blooks.NormalNowcol)
        {
            canBurnFlag = S_blooks.NormalNowcol;
        }
        else
        {
            canBurnFlag = false;
        }



        if (S_blooks.BurnFlg)
        {

            canBurnFlag = false;
        }


        if (canBurnFlag)
        {
            OneUCFlag = true;

            
            time = Mathf.PingPong(timeCount, CanBurnMaxEmission); //引数１と引数２の数値を行き来させる。
            

            //ここでスピード調整して行き来する。
            if (UpCountFlag)
            {
                timeCount += (float)canBurnSpeed;
            }
            if (DownCountFlag)
            {
                timeCount -= (float)canBurnSpeed;
            }


            float val = time;
           // float num = val * val;
            // Color color = new Color(val * val, val * val, val * val);
            Color color = new Color(val/2, val, 0); //エミッションの光度を変えてる。
            GetComponent<Renderer>().material.SetColor("_EmissionColor", color);　//ここで色を入れ込む。



            //光るか光らなくなるかを見てる
            if (time < CanBurnMaxEmission && !DownCountFlag)
            {
            }
            else
            {
                if (DownCountFlag)
                {

                }
                else
                {
                    UpCountFlag = false;
                    OneUCFlag = false;
                    DownCountFlag = true;
                    ChangeEmissionFlag = false;
                }
            }
            if (time > CanBurnMiniEmission && !UpCountFlag)
            {
            }
            else
            {
                if (UpCountFlag)
                {

                }
                else
                {
                    UpCountFlag = true;
                    DownCountFlag = false;
                    ChangeEmissionFlag = false;
                }
            }
        }
        else if (S_blooks.BurnFlg == false && OneUCFlag && S_blooks.NormalNowcol == false)
        {

            time = Mathf.PingPong(timeCount, CanBurnMaxEmission); //引数１と引数２の数値を行き来させる。
            if (UpCountFlag)
            {
                timeCount += (float)canBurnSpeed;
            }
            if (DownCountFlag)
            {
                timeCount -= (float)canBurnSpeed;
            }

            float val = time;
            // float num = val * val; 使わない変数はとりあえずコメント化　--6/25 李--
            // Color color = new Color(val * val, val * val, val * val);
            Color color = new Color(val/2, val, 0); //エミッションの光度を変えてる。
            GetComponent<Renderer>().material.SetColor("_EmissionColor", color); //ここで色を入れ込む。

            //光るか光らなくなるかを見てる
            if (time < CanBurnMaxEmission-0.06f && !DownCountFlag && OneUCFlag)
            {
                
            }
            else
            {
                if (DownCountFlag)
                {

                }
                else
                {
                    UpCountFlag = false;
                    DownCountFlag = true;
                    ChangeEmissionFlag = false;
                }
            }
            if (time > CanBurnMiniEmission+0.06f && !UpCountFlag&&OneUCFlag)
            {

            }
            else
            {
                if (UpCountFlag)
                {

                }
                else
                {
                    UpCountFlag = true;
                    DownCountFlag = false;
                    ChangeEmissionFlag = false;
                    OneUCFlag = false;
                }
            }
        }
        else if ((S_blooks.BurnFlg && OneUCFlag))
        {
            UpCountFlag = true;
            DownCountFlag = false;
            ChangeEmissionFlag = false;
            OneUCFlag = false;

        }
    }
}
=======

        //// 違うオブジェクトに変わったら(デバッグ用？)
        //if (EmManager.Edit_EmissiveObj == EmissiveObj)
        //{
        //    InitMaterial();
        //}

        
        if(!S_Blocks.NormalNowcol && !isBurned)
        {
            EndFlg = true;
        }
        if (S_Blocks.CanBurn && !isBurned)
        {
            CanEmission = true;
        }
        if (S_Blocks.CantBurn && !isBurned)
        {
            CanEmission = false;
        }
        if (S_Blocks.NormalNowcol && !isBurned)
        {
            StartFlg = true;
        }
        if (S_Blocks.BurnFlg && !isBurned)
        {
            isBurned = true;
            isBurning = true;
            EmManager.EmissionCnt--;
        }

        // エミッションの種類の切り替わり
        if (Old_CanEmission != CanEmission)
        {
            EndFlg = true;
        }
        Old_CanEmission = CanEmission;

        if (isBurning)
        {
            BurnEmission();
        }
        else
        {

            // エミッション消え始め
            if (EndFlg)
            {
                EndFlg = false;
                isEnding = true;
            }

            // エミッション燃え始め
            if (StartFlg)
            {
                StartFlg = false;
                isEmissioning = true;
            }

            // カラーのセット
            //EmissiveObj.GetComponent<Renderer>().material.color = ObjColor;

            // エミッション中処理
            if (isEmissioning && !isEnding)
            {
                SetEditMatrial();

                if (EmManager.GetIsBasedSetted())
                {
                    NowTime = EmManager.shareNowTime;
                }
                else
                {
                    isBaseTimeObj = true;
                    EmManager.SetIsBasedSetted(true);
                }

                CalcEmission();

                if (isBaseTimeObj)
                {
                    EmManager.shareNowTime = NowTime;
                }
            }

            if (isEnding)
            {
                EndEmission();
            }

            // エミッションが始まったか終わったか
            if (Old_isEmissioning != isEmissioning)
            {
                if (isEmissioning)
                {
                    // 他にエミッションがスタートしていない場合、ベースのタイムを設定
                    if (!EmManager.GetIsBasedSetted() && isEmissioning)
                    {
                        EmManager.shareNowTime = NowTime;
                        EmManager.SetIsBasedSetted(true);
                        isBaseTimeObj = true;
                    }

                    EmManager.EmissionCnt++;
                }
                else
                {
                    if (isBaseTimeObj)
                    {
                        EmManager.SetIsBasedSetted(false);
                        isBaseTimeObj = false;
                    }
                    EmManager.EmissionCnt--;
                }
            }
            Old_isEmissioning = isEmissioning;
        }

    }

    public void Init()
    {
        // エミッションマネージャ取得
        GetEmissionInfo();

        // 各値初期化
        NowTime = 0.0f;
        ChangeTime = EmManager.Edit_ChangeTime;
        UpFlg = EmManager.Edit_UpStartFlg;

        value = 0;

        EndFlg = false;
        StartFlg = false;

        isEmissioning = false;
        Old_isEmissioning = false;
        isBaseTimeObj = false;
        isEnding = false;

        EmManager.EmissionCnt = 0;

        // マテリアル初期化
        InitMaterial();

        S_Blocks = this.transform.parent.GetComponent<Blocks>();


        if (S_Blocks.StartBlockFlg)
        {
            isBurned = true;
            isBurning = true;
            Color color = new Color(1, 1, 1); //エミッションの光度を変えてる。
            EmissiveObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", color * EmManager.EmissionPower);　//ここで色を入れ込む。
        }
        else
        {
            isBurned = false;
            isBurning = false;
            Color color = new Color(0, 0, 0); //エミッションの光度を変えてる。
            EmissiveObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);　//ここで色を入れ込む。
        }

    }

    private void InitMaterial()
    {
        if (EmissiveObj == null)
        {
            //EmissiveObj = this.transform.Find("GlassBlock").gameObject;
            EmissiveObj = this.gameObject;
            SetEditMatrial();
        }

        //BlendModeUtils.SetBlendMode(EmissiveObj.GetComponent<Renderer>().material, BlendModeUtils.Mode.Emission);

    }


    private void SetEditMatrial()
    {
        //ObjColor = EmManager.Edit_ObjColor;
        if (CanEmission)
        {
            EmissionColor = EmManager.Edit_CanEmissionColor;
        }
        else
        {
            EmissionColor = EmManager.Edit_CanNotEmissionColor;
        }
    }

    private void CalcEmission()
    {
        // 割合計算
        if (UpFlg && !EndFlg)
        {
            NowTime += Time.deltaTime;
            value = NowTime / ChangeTime;
        }
        if (!UpFlg && !EndFlg)
        {
            NowTime -= Time.deltaTime;
            value = NowTime / ChangeTime;
        }

        // 秒数範囲外処理
        if (NowTime > ChangeTime)
        {
            UpFlg = false;
            NowTime = ChangeTime;
            value = 1.0f;
        }
        if (NowTime < 0)
        {
            UpFlg = true;
            NowTime = 0;
            value = 0.0f;
        }

        EmissionColor = new Color(EmissionColor.r * value, EmissionColor.g * value, EmissionColor.b * value);

        EmissiveObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", EmissionColor * EmManager.EmissionPower);
    }

    private void EndEmission()
    {
        if (isEnding)
        {
            NowTime -= Time.deltaTime * (2 * ChangeTime);
            value = NowTime / ChangeTime;

            if (NowTime < 0)
            {
                UpFlg = true;
                value = 0.0f;
                NowTime = 0;
                EndFlg = false;
                isEmissioning = false;
                isEnding = false;
            }

            EmissionColor = new Color(EmissionColor.r * value, EmissionColor.g * value, EmissionColor.b * value);

            EmissiveObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", EmissionColor * EmManager.EmissionPower);

        }

    }

    private void BurnEmission()
    {
        if (isBurning)
        {
            NowTime += Time.deltaTime * (2 * ChangeTime);
            value = NowTime / ChangeTime;

            if (NowTime > ChangeTime)
            {
                value = 1.0f;
                NowTime = ChangeTime;
                //isBurning = false;
                EmManager.SetIsBasedSetted(false);
            }

            EmissionColor = new Color(EmManager.Edit_BurnEmissionColor.r * value, EmManager.Edit_BurnEmissionColor.g * value, EmManager.Edit_BurnEmissionColor.b * value);

            EmissiveObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", EmissionColor * EmManager.EmissionPower);

        }

    }

    private void GetEmissionInfo()
    {
        EmManager = GameObject.Find("EmissionManager").GetComponent<EmissionManager>();
    }
}
>>>>>>> Dev
