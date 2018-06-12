using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeImage : MonoBehaviour {

    enum STATUS{
        LOOK = 0,
        INVISIBLE,
        FADEIN,
        FADEOUT,
    }

    private float LookTime;          // クリアイメージが見える時間
    private float InvisibleTime;     // クリアイメージが見えない時間
    private float FadeTime;          // フェードにかかる時間

    private STATUS status;          // ステータス
    private bool TimeGetFlg;        // 現在の時刻のリセット用
    private float NowTime;          // 現在の時間(alpha計算用)
    private float alpha;            // alpha計算用
    private float RGBARatio;        // 変化割合計算用

    private Renderer renderer;
    
    public void Init(Renderer rend, float looktime, float invisibletime, float fadetime)
    {
        // 変数初期化
        status = STATUS.INVISIBLE;
        TimeGetFlg = false;
        NowTime = 0.0f;
        alpha = 0.0f;

        // 変更するオブジェクトのレンダラをセット
        renderer = rend;

        // 各タイムの初期化
        LookTime = looktime;
        InvisibleTime = invisibletime;
        FadeTime = fadetime;
    }

    public void Init()
    {
        // 変数初期化
        status = STATUS.INVISIBLE;
        TimeGetFlg = false;
        NowTime = 0.0f;
        alpha = 0.0f;

        // 変更するオブジェクトのレンダラをセット
        renderer = this.GetComponent<Renderer>();
    }

    public void Init(Renderer rend)
    {
        // 変数初期化
        status = STATUS.INVISIBLE;
        TimeGetFlg = false;
        NowTime = 0.0f;
        alpha = 0.0f;

        // 変更するオブジェクトのレンダラをセット
        renderer = rend;
    }

    public void SetMaterialAlpha(float alpha)
    {
        renderer.materials[0].color = new Color(1, 1, 1, alpha);
    }

    public void Fade()
    {
        switch (status)
        {
            case STATUS.LOOK:       // クリアイメージ表示中
                if (!TimeGetFlg)
                {
                    TimeGetFlg = true;
                    NowTime = 0;
                    //renderer.materials[0].color = new Color(1, 1, 1, 1);
                    SetMaterialAlpha(1);
                }
                if (NowTime > LookTime)
                {
                    status = STATUS.FADEOUT;
                    TimeGetFlg = false;
                }
                else
                {
                    NowTime += Time.deltaTime;
                }
                
                break;

            case STATUS.INVISIBLE:  // ステージイメージ表示中
                if (!TimeGetFlg)
                {
                    TimeGetFlg = true;
                    NowTime = 0;
                    //renderer.materials[0].color = new Color(1, 1, 1, 0);
                    SetMaterialAlpha(0);
                }
                if (NowTime > InvisibleTime)
                {
                    status = STATUS.FADEIN;
                    TimeGetFlg = false;
                }
                else
                {
                    NowTime += Time.deltaTime;
                }

                break;

            case STATUS.FADEIN:     // フェードイン
                if (!TimeGetFlg)
                {
                    TimeGetFlg = true;
                    NowTime = 0;
                    alpha = 0.0f;
                    //renderer.materials[0].color = new Color(1, 1, 1, alpha);
                    SetMaterialAlpha(alpha);

                    RGBARatio = FadeTime * 255.0f;

                }
                if (NowTime > FadeTime)
                {
                    status = STATUS.LOOK;
                    TimeGetFlg = false;
                }
                else
                {
                    NowTime += Time.deltaTime;

                    alpha = NowTime / FadeTime;
                    //renderer.materials[0].color = new Color(1, 1, 1, alpha);
                    SetMaterialAlpha(alpha);
                }

                break;

            case STATUS.FADEOUT:    // フェードアウト
                if (!TimeGetFlg)
                {
                    TimeGetFlg = true;
                    NowTime = 0;
                    alpha = 1.0f;
                    //renderer.materials[0].color = new Color(1, 1, 1, alpha);
                    SetMaterialAlpha(alpha);

                    RGBARatio = FadeTime * 255.0f;

                }
                if (NowTime > FadeTime)
                {
                    status = STATUS.INVISIBLE;
                    TimeGetFlg = false;
                }
                else
                {
                    NowTime += Time.deltaTime;

                    alpha = 1.0f - NowTime / FadeTime;
                    //renderer.materials[0].color = new Color(1, 1, 1, alpha);
                    SetMaterialAlpha(alpha);
                }

                break;

        }
    }
}
