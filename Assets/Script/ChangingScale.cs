using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingScale : MonoBehaviour
{
    private GameObject ChangeObj;    // スケールを変更するオブジェクト

    public float StartScale;        // 開始時のスケールサイズ
    public float MinScale;          // 最小のスケールサイズ
    public float MaxScale;          // 最大のスケールサイズ
    private float ChangeValue;

    public float ChangeTime;        // 最大と最小にかかる時間

    private float NowTime;           // 現在の時間(計算用)

    public bool UpFlg;              // スケール計算判定用(true:大きくする)
    public bool DownFlg;            // スケール計算判定用(true:小さくする)

    // Use this for initialization
    void Start()
    {

        ChangeValue = MaxScale - MinScale;

        // StartScale用計算
        NowTime = (StartScale * ChangeTime / ChangeValue) - (MinScale * ChangeTime / ChangeValue);

        ChangeObj = this.gameObject;
        ChangeObj.GetComponent<Transform>().localScale = new Vector3(StartScale, StartScale, StartScale);

        // エラー処理
        if(UpFlg && DownFlg || !UpFlg && !DownFlg)
        {
            UpFlg = true;
            DownFlg = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // スケールの変更
        ChangeScale();
    }

    void ChangeScale()
    {
        float val = 0;

        // スケール計算
        if(UpFlg)
        {            
            NowTime += Time.deltaTime;
            val = NowTime / ChangeTime * ChangeValue + MinScale;
        }
        if (DownFlg)
        {
            NowTime -= Time.deltaTime;
            val = NowTime / ChangeTime * ChangeValue + MinScale;
        }

        // 秒数範囲外処理
        if (NowTime > ChangeTime)
        {
            UpFlg = false;
            DownFlg = true;
            val = MaxScale;
        }
        if(NowTime < 0)
        {
            UpFlg = true;
            DownFlg = false;
            val = MinScale;
        }

        ChangeObj.GetComponent<Transform>().localScale = new Vector3(val, val, val);

    }
}
