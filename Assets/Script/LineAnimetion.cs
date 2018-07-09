using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimetion : MonoBehaviour {


    // カーソルアニメーション
    public GameObject LineObj;
    public float AnimTime = 5.0f;
    public float AnimeStatoTime = 0.5f;
    public float LineXSize = 1.0f;
    public bool isLineAnim = false;

    private float tmpTime = 0.0f;
    private float tmpScale = 0.0f;
    private float basePos;
    private Transform tmpTrans;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isLineAnim)
        {
            LineAnimation(LineXSize, AnimTime);
        }
    }

    public void InitLineAnimation()
    {
        LineObj.transform.localPosition = new Vector3(LineObj.transform.localPosition.x - LineObj.transform.localScale.x * 2, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
        LineObj.transform.localScale = new Vector3(0.0f, 1.0f, 0.1f);

        tmpTime = 0.0f;
        tmpScale = 0.0f;
        tmpTrans = LineObj.transform;
        basePos = tmpTrans.transform.localPosition.x;
        isLineAnim = true;

    }

    private void LineAnimation(float endScale, float animTime)
    {
        //if (!isInit)
        //{
        //    InitLineAnimation();
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
                LineObj.transform.localPosition = new Vector3(LineObj.transform.localScale.x * 2, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
                LineObj.transform.localPosition = new Vector3(LineObj.transform.localPosition.x + basePos, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
            }

        }

        if (tmpTime > AnimTime)
        {
            // 補正処理
            LineObj.transform.localScale = new Vector3(endScale, LineObj.transform.localScale.y, LineObj.transform.localScale.z);
            LineObj.transform.localPosition = new Vector3(LineObj.transform.localScale.x * 2, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
            LineObj.transform.localPosition = new Vector3(LineObj.transform.localPosition.x + basePos, LineObj.transform.localPosition.y, LineObj.transform.localPosition.z);
            //isInit = false;
            isLineAnim = false;
        }

    }

}
