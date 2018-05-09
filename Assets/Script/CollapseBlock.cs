using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消えるブロックのクラス。
/// </summary>
public class CollapseBlock : Blocks {

    public float deleteTime { get; private set; }       //火がついてからブロックが消滅するまでの時間。
    public float deleteCnt { get; private set; }        //burnFlgがついてからカウント開始。
    public bool deleteFlg { get; private set; }         //消滅する時間以上になるとdelete処理に入るためのフラグ。

	// Use this for initialization
	void Start () {
        InitMaterial();
	}
	
	// Update is called once per frame
	void Update () {
        CheckBurnFlg();
	}
}
