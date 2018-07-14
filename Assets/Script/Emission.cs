using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emission : MonoBehaviour
{
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

    private Blocks S_Blocks;
    public bool isBurned;
    private bool isBurning;

    // Use this for initialization
    void Start()
    {

        Init();

    }

    // Update is called once per frame
    void Update()
    {

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

    private void Init()
    {
        GetEmissionInfo();

        NowTime = 0.0f;
        ChangeTime = EmManager.Edit_ChangeTime;
        //MinValue = Edit_MinValue;
        //MaxValue = Edit_MaxValue;
        UpFlg = EmManager.Edit_UpStartFlg;
        EndFlg = false;
        value = 0;
        isEmissioning = false;
        Old_isEmissioning = false;
        isBaseTimeObj = false;
        isEnding = false;

        InitMaterial();

        S_Blocks = this.GetComponent<Blocks>();


        if (S_Blocks.StartBlockFlg)
        {
            isBurned = true;
            Color color = new Color(1, 1, 1); //エミッションの光度を変えてる。
            EmissiveObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", color * EmManager.EmissionPower);　//ここで色を入れ込む。
        }
        else
        {
            isBurned = false;
            Color color = new Color(0, 0, 0); //エミッションの光度を変えてる。
            EmissiveObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);　//ここで色を入れ込む。
        }

    }

    private void InitMaterial()
    {
        if (EmissiveObj == null)
        {
            EmissiveObj = this.transform.Find("GlassBlock").gameObject;
            //EmissiveObj = this.gameObject;
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
