using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emission : MonoBehaviour
{
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
    public bool canNotBurnFlag = false;                    //燃えれないインフォメーションのフラグ
                                                        // Use this for initialization
    void Start()
    {
        Speed = MaxEmission / (Maxtime * 60);
        canBurnSpeed = CanBurnMaxEmission / (CanBurnMaxTime * 60);

    }

    // Update is called once per frame
    void Update()
    {
        BurnEmisson();
        CanBurnEmission();
        CanNotBurnEmission();

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
        if (S_blooks.NormalNowcol && S_blooks.CanBurn)
        {
            canBurnFlag = true;
            canNotBurnFlag = false;
        }
        else
        {
            canBurnFlag = false;
            canNotBurnFlag = false;
        }



        if (S_blooks.BurnFlg)
        {
            canBurnFlag = false;
            canNotBurnFlag = false;
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

    void CanNotBurnEmission()
    {
        //if (canBurnFlag && UpCountFlag)
        //{
        //    ChangeEmissionFlag = true;
        //}
        //else
        //{
        //    ChangeEmissionFlag = false;
        //}
        if (S_blooks.NormalNowcol && S_blooks.CantBurn)
        {
            canBurnFlag = false;
            canNotBurnFlag = true;
        }
        else
        {
            canBurnFlag = false;
            canNotBurnFlag = false;
        }


        // Todo:切るタイミング
        if (S_blooks.BurnFlg)
        {
            canBurnFlag = false;
            canNotBurnFlag = false;
        }


        if (canNotBurnFlag)
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
            Color color = new Color(val, 0, 0); //エミッションの光度を変えてる。
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
            Color color = new Color(val, 0, 0); //エミッションの光度を変えてる。
            GetComponent<Renderer>().material.SetColor("_EmissionColor", color); //ここで色を入れ込む。

            //光るか光らなくなるかを見てる
            if (time < CanBurnMaxEmission - 0.06f && !DownCountFlag && OneUCFlag)
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
            if (time > CanBurnMiniEmission + 0.06f && !UpCountFlag && OneUCFlag)
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