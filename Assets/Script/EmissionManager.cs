using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionManager : MonoBehaviour {

    // エディタから調整用
    public Color Edit_ObjColor;
    public Color Edit_CanEmissionColor;
    public Color Edit_CanNotEmissionColor;
    public Color Edit_BurnEmissionColor;
    public float Edit_ChangeTime;
    public bool Edit_UpStartFlg;

    public float shareNowTime;
    public float EmissionPower;
    public int EmissionCnt;

    private bool isBaseSetted;


    // Use this for initialization
    void Start () {
        EmissionCnt = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool GetIsBasedSetted()
    {
        return isBaseSetted;
    }
    public void SetIsBasedSetted(bool flg)
    {
        isBaseSetted = flg;
    }
}
