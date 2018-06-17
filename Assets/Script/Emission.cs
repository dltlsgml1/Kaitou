﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emission : MonoBehaviour
{
    public bool ChangeEmissionFlag = false;      //点滅し始めるフラグ
    public Blocks S_blooks;
    float time = 0;                             //秒数計算用
    float timeCount = 0;                        //秒数計算用
    public float MiniEmission = 0.0f;
    public float MaxEmission = 1.0f;
    public float Maxtime = 1;                       //現状の何秒で変わっていくか
    private float CanBurnMiniEmission = 0.0f;
    private float CanBurnMaxEmission = 0.5f;
    private float CanBurnMaxTime = 0.15f;

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
        Debug.Log(Speed);

    }

    // Update is called once per frame
    void Update()
    {
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
            float num = val * val;
            // Color color = new Color(val * val, val * val, val * val);
            Color color = new Color(val, val, val); //エミッションの光度を変えてる。
            GetComponent<Renderer>().material.SetColor("_EmissionColor", color);　//ここで色を入れ込む。



            //光るか光らなくなるかを見てる
            if (time < MaxEmission - 0.1f && !DownCountFlag)
            {
                //  Debug.Log(num);
            }
            else
            {
                if (DownCountFlag)
                {

                }
                else
                {
                    // Debug.Log(Time.time);
                    UpCountFlag = false;
                    DownCountFlag = true;
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
            Debug.Log("点滅中");

            OneUCFlag = true;
            time = Mathf.PingPong(timeCount, CanBurnMaxEmission); //引数１と引数２の数値を行き来させる。
            Debug.Log("エミッションの数値 / " + time);

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
            float num = val * val;
            // Color color = new Color(val * val, val * val, val * val);
            Color color = new Color(val, val, val); //エミッションの光度を変えてる。
            GetComponent<Renderer>().material.SetColor("_EmissionColor", color);　//ここで色を入れ込む。



            //光るか光らなくなるかを見てる
            if (time < CanBurnMaxEmission && !DownCountFlag)
            {
                //  Debug.Log(num);
            }
            else
            {
                if (DownCountFlag)
                {

                }
                else
                {
                    Debug.Log(Time.time);
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

            Debug.Log("エミッション点滅を終わらせる");
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
            float num = val * val;
            // Color color = new Color(val * val, val * val, val * val);
            Color color = new Color(val, val, val); //エミッションの光度を変えてる。
            GetComponent<Renderer>().material.SetColor("_EmissionColor", color); //ここで色を入れ込む。

            Debug.Log("Time" + time);
            //光るか光らなくなるかを見てる
            if (time < CanBurnMaxEmission-0.06f && !DownCountFlag && OneUCFlag)
            {
                //  Debug.Log(num);
                Debug.Log("Up処理" + DownCountFlag);
                
                //Debug.Log("DownTime" + time + "<" + CanBurnMaxEmission + "CanBurnMaxEmission");
            }
            else
            {
                Debug.Log("Up処理2" + DownCountFlag);
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
            if (time > CanBurnMiniEmission+0.06f && !UpCountFlag)
            {
                Debug.Log("ダウン処理" + UpCountFlag);
                //Debug.Log("UpTime" + time + "<" + CanBurnMiniEmission + "CanBurnMinEmission");

            }
            else
            {
                Debug.Log("ダウン処理2" + UpCountFlag);
                if (UpCountFlag)
                {

                }
                else
                {
                    UpCountFlag = true;
                    DownCountFlag = false;
                    ChangeEmissionFlag = false;
                    Debug.Log("点滅終了処理終わりの手前でフラグ変更" + OneUCFlag);
                    OneUCFlag = false;
                }
            }
        }
        else if ((S_blooks.BurnFlg && OneUCFlag))
        {
            Debug.Log("点滅してないからフラグを整理中");
            UpCountFlag = true;
            DownCountFlag = false;
            ChangeEmissionFlag = false;
            OneUCFlag = false;

        }
    }
}