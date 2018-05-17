using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消えるブロックのクラス。
/// </summary>
public class CollapseBlock : Blocks {

    public float DeleteTime = 5.0f;      //火がついてからブロックが消滅するまでの時間。
    public float DeleteCnt = 0.0f;   //burnFlgがついてからカウント開始。
    public bool DeleteFlag = false;       //消滅する時間以上になるとdelete処理に入るためのフラグ。

	// Use this for initialization
	void Start () {
       // InitMaterial();
	}
	
	// Update is called once per frame
	void Update () {
        if (DeleteFlag == true) 
        {
            DeleteCnt += 0.1f;
        }
        if (DeleteCnt == DeleteTime)
        {
            DestroyObject(this);
        }
        //CheckBurnFlg();
	}

    public void SetDeleteFlag()
    {
        DeleteFlag = true;
    }

    
}
