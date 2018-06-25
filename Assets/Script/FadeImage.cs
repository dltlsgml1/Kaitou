using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeImage : MonoBehaviour
{

    // フェード処理状態
    enum STATUS
    {
        LOOK = 0,       // 表示中
        INVISIBLE,      // 非表示中
        FADEIN,         // フェードイン中
        FADEOUT,        // フェードアウト中
    }

    private float LookTime;          // クリアイメージが見える時間
    private float InvisibleTime;     // クリアイメージが見えない時間
    private float FadeTime;          // フェードにかかる時間

    private STATUS status;          // ステータス
    private bool TimeGetFlg;        // 現在の時刻のリセット用
    private float NowTime;          // 現在の時間(alpha計算用)
    private float alpha;            // alpha計算用

    private Renderer renderer;

    private bool isFadingIN;
    private bool isFadingOut;

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

    // マテリアルのアルファ値セット
    public void SetMaterialAlpha(float alpha)
    {
        renderer.materials[0].color = new Color(1, 1, 1, alpha);
    }

    // フェード処理
    public void Fade()
    {
        switch (status)
        {
            case STATUS.LOOK:       // クリアイメージ表示中

                // タイムの初期化
                if (!TimeGetFlg)
                {
                    TimeGetFlg = true;
                    NowTime = 0;
                    SetMaterialAlpha(1);
                }

                // 表示時間超過したらフェードアウト開始
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

                // タイムの初期化
                if (!TimeGetFlg)
                {
                    TimeGetFlg = true;
                    NowTime = 0;
                    SetMaterialAlpha(0);
                }

                // 非表示時間超過したらフェードイン開始
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

                // タイムの初期化
                if (!TimeGetFlg)
                {
                    TimeGetFlg = true;
                    NowTime = 0;
                    alpha = 0.0f;
                    SetMaterialAlpha(alpha);
                }

                // フェードイン終了
                if (NowTime > FadeTime)
                {
                    status = STATUS.LOOK;
                    TimeGetFlg = false;
                }
                else
                {
                    NowTime += Time.deltaTime;

                    // 現在の時間からアルファ値の割合計算
                    alpha = NowTime / FadeTime;
                    SetMaterialAlpha(alpha);
                }

                break;

            case STATUS.FADEOUT:    // フェードアウト

                // タイムの初期化
                if (!TimeGetFlg)
                {
                    TimeGetFlg = true;
                    NowTime = 0;
                    alpha = 1.0f;
                    SetMaterialAlpha(alpha);
                }

                // フェードアウト終了
                if (NowTime > FadeTime)
                {
                    status = STATUS.INVISIBLE;
                    TimeGetFlg = false;
                }
                else
                {
                    NowTime += Time.deltaTime;

                    // 現在の時間からアルファ値の割合計算
                    alpha = 1.0f - NowTime / FadeTime;
                    SetMaterialAlpha(alpha);
                }

                break;

        }
    }

    // フェードイン
    public IEnumerator FadeIn(Renderer rend, float fadeTime)
    {
        // 排他制御
        if (isFadingIN)
        {
            yield break;
        }

        isFadingIN = true;
        rend.materials[0].color = new Color(1, 1, 1, 0);

        float nowTime = 0.0f;
        float a = 0;

        while (NowTime > fadeTime)
        {
            nowTime += Time.deltaTime;

            // 現在の時間からアルファ値の割合計算
            a = nowTime / fadeTime;
            rend.materials[0].color = new Color(1, 1, 1, a);

            yield return true;

        }

        rend.materials[0].color = new Color(1, 1, 1, 1);

        isFadingIN = false;

    }

    // フェードアウト
    public IEnumerator FadeOut(Renderer rend, float fadeTime)
    {
        // 排他制御
        if (isFadingOut)
        {
            yield break;
        }

        isFadingOut = true;
        rend.materials[0].color = new Color(1, 1, 1, 1);

        float nowTime = 0.0f;
        float a = 1;

        while (NowTime > fadeTime)
        {
            nowTime += Time.deltaTime;

            // 現在の時間からアルファ値の割合計算
            a = 1.0f - nowTime / fadeTime;
            rend.materials[0].color = new Color(1, 1, 1, a);

            yield return true;

        }

        rend.materials[0].color = new Color(1, 1, 1, 0);

        isFadingOut = false;
    }
}
