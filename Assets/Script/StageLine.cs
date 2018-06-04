using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLine : MonoBehaviour {

    public GameObject SetStarlineObj;//表示させるオブジェクトの親オブジェクト取得
    LifestarReceive GetLimitStarLine;//現在の上限回数取得用


	// Use this for initialization
	void Start () {
        GetLimitStarLine = GetComponentInParent<LifestarReceive>();
	}
	
	// Update is called once per frame
	void Update () {

        //子の数だけループ
        for (int i = 0; i < SetStarlineObj.transform.childCount; i++)
        {
            //上限回数によるオブジェクトのOn/Off
            if (i < GetLimitStarLine.ReceiveLimitNum)
            {
                SetStarlineObj.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                SetStarlineObj.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

       



	}
}
