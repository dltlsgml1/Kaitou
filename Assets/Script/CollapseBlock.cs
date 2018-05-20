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

    public void SetDeleteFlag(bool flag)
    {
        DeleteFlag = flag;
    }
    public void ActivingTimer()
    {
        DeleteCnt += 0.1f;
    }

    public bool IsTimerDone()
    {
        if(DeleteCnt==DeleteTime)
        {
            DeleteCnt = 0.0f;
            return true;
        }
        return false;
    }

    public void SetBurn(GameObject Obj)
    {
        Obj.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    public void SetDeleteFlag()
    {
        DeleteFlag = true;
    }
    
    public void DeleteObject()
    {
        DestroyObject(this, 5.0f);
    }
}
