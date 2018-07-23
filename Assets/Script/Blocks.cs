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
<<<<<<< HEAD

    public static bool nowplayingse = false;
    public bool BurnFlg;
    public bool StartBlockFlg;
    public float BurnCnt = 0.0f;
    public int CollapsNum = 0;
    public float BurnCnt2 = 0.0f;
    public static int NowCollapsingBlock = 0;
    public bool canburn = false;
    
=======
    
    public bool BurnFlg;
    public bool StartBlockFlg;
    public float BurnCnt = 0.0f;
    public bool BurnOK = false;
    public bool CantBurn = false;
    public bool CanBurn = false;
    

>>>>>>> Dev
    void Start ()
    {      
        if (StartBlockFlg == true)
        {
            BurnFlg = true;
        }
    }

    public void StartFlg()
    {
        if (StartBlockFlg == true)
        {
            BurnFlg = true;
        }
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
<<<<<<< HEAD
=======
        BurnOK = false;
        CantBurn = false;
        CanBurn = false;
>>>>>>> Dev
    }
}
