using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emission : MonoBehaviour
{
    public bool ChangeEmissionFlag = true;      //点滅し始めるフラグ
    float time = 0;                             //秒数計算用
    float timeCount = 0;                        //秒数計算用
    public float MiniEmissio = 0.0f;
    public float MaxEmission = 1.0f;
    public int Maxtime = 3;                       //現状の何秒で変わっていくか
    double Speed;
    public bool UpCountFlag = true;                    //明るくするフラグ
    public bool DownCountFlag = false;                 //暗くするフラグ
                                                // Use this for initialization
    void Start()
    {
        Speed = MaxEmission / (Maxtime * 60);
        Debug.Log(Speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeEmissionFlag = true;
        }

        if (ChangeEmissionFlag)
        {
            time = Mathf.PingPong(timeCount, MaxEmission+0.1f);
            if (UpCountFlag)
            {
                timeCount +=(float)Speed;
            }
            if (DownCountFlag)
            {
                timeCount -= (float)Speed;
            }
            float val = time;
            float num = val * val;
            // Color color = new Color(val * val, val * val, val * val);
            Color color = new Color(val, val, val);
            GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
            if ( time< MaxEmission-0.1f&&!DownCountFlag)
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
                    DownCountFlag = true;
                    ChangeEmissionFlag = false;
                }
            }
            if (time > MiniEmissio+0.2f&&!UpCountFlag)
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
}