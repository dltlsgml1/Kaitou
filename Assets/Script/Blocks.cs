﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {
    

    
    public static bool nowplayingse = false;
    public GameObject SetFire;
    public bool BurnFlg;
    public bool StartBlockFlg;
    public float BurnCnt;
    public bool[] CollapsPlain = new bool[6];
    public int CollapsNum = 0;


    public static int NowCollapsingBlock = 0;
    public static float BurnCount = 0.0f;
    public bool NowBurning = false;
    public bool BurningDone = false;
    Material Mat_Normal;
    Material Mat_Collaps;

    private void Awake()
    {
        Mat_Normal = Resources.Load("GameMain/Materials/GameMain_BlockNomal_01") as Material;
        Mat_Collaps = Resources.Load("GameMain/Materials/GameMain_BlockNomal_02") as Material;
       
    }

    void Start ()
    {      
        if (StartBlockFlg == true)
        {
            BurnFlg = true;
            SetBurn();
            SetBurnMaterial();
        }
    }

    public void StartFlg()
    {
        if (StartBlockFlg == true)
        {
            BurnFlg = true;
            SetBurn();
            SetBurnMaterial();
        }
    }
	
	public void SetBurnMaterial()
    {
        this.GetComponent<MeshRenderer>().material = Mat_Collaps;
    }
    

    public void SetBurn()
    {
        SetFire.gameObject.SetActive(true);
        SetFire.gameObject.transform.position = new Vector3(this.transform.position.x,
                                                            this.transform.position.y + 0.5f,
                                                            this.transform.position.z);
    }

    public bool Burning()
    {
        if (nowplayingse == false)
        {
            Sound.PlaySe("se_burnnow", 1);
            nowplayingse = true;
        }

        if (NowBurning == false)
        {
            BurnCnt = 0.0f;
            NowBurning = true;
        }

        BurnCnt += DefineScript.JUDGE_BURNNIGSPEED;
        if (BurnCnt >= DefineScript.JUDGE_BURNNINGTIME)
        {
            BurnFlg = true;
            BurnCnt = 0.0f;
            SetBurn();
            SetBurnMaterial();
            return true;
        }
        return false;
    }
}
