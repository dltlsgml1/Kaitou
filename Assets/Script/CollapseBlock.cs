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

    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        SetFire.gameObject.SetActive(true);
        SetFire.gameObject.transform.position = new Vector3(this.transform.position.x,
                                                            this.transform.position.y+0.5f,
                                                            this.transform.position.z);
    }
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

    public new void SetBurn(GameObject Obj)
    {
        Obj.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
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
