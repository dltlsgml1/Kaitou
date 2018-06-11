using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {


    public bool CollapsTop;
    public bool CollapsBottom;
    public bool CollapsFront;
    public bool CollapsBack;
    public bool CollapsLeft;
    public bool CollapsRight;

    public bool IsTopCollapsed = false;
    public bool IsRightCollapsed = false;
    public bool IsLeftCollapsed = false;
    public bool IsBottomCollapsed = false;
    public bool IsFrontCollapsed = false;
    public bool IsBackCollapsed = false;

    public bool NormalNowcol = false;
    public bool CollapsNowcol = false;

    public static bool nowplayingse = false;
    public GameObject SetFire;
    public bool BurnFlg;
    public bool StartBlockFlg;
    public float BurnCnt = 0.0f;
    public int CollapsNum = 0;
    public static int NowCollapsingBlock = 0;
    public bool canburn = false;
    


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


    public void UnsetCollapsFlag()
    {
        IsTopCollapsed = false;
        IsRightCollapsed = false;
        IsLeftCollapsed = false;
        IsBottomCollapsed = false;
        IsFrontCollapsed = false;
        IsBackCollapsed = false;
        NormalNowcol = false;
        CollapsNowcol = false;
    }
}
