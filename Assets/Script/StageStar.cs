using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StageStar : MonoBehaviour {

    public GameObject SetStarObj;//表示させるオブジェクトの親オブジェクト取得
    LifestarReceive GetLimitStar;//現在の上限回数取得用

	// Use this for initialization
	void Start () {
        GetLimitStar = GetComponentInParent<LifestarReceive>();
	}
	
	// Update is called once per frame
	void Update () {


        //子の数だけループ
        for ( int i = 0; i < SetStarObj.transform.childCount; i++) {
            //上限が0の場合全消去
            if (GetLimitStar.ReceiveLimitNum <= 0)
            {
                SetStarObj.transform.GetChild(i).gameObject.SetActive(false);
            }
            //上限回数によるオブジェクトのOn/Off
            else
            {
                if (i < GetLimitStar.ReceiveLimitNum + 1)
                {
                    SetStarObj.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    SetStarObj.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        


	}
}
