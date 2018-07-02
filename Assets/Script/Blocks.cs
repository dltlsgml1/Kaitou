using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {

    public static bool nowplayingse = false;
    public GameObject SetFire;
    public Material StandardMaterial;
    public Material BurnMaterial;
    public bool BurnFlg;
    public bool StartBlockFlg;
    public float BurnCnt;
    public static Material Mat_Normal;
    public static Material Mat_Collaps;
    // Use this for initialization
    private void Awake()
    {
        Mat_Normal = Resources.Load("GameMain/Materials/GameMain_BlockNomal_01") as Material;
        Mat_Collaps = Resources.Load("GameMain/Materials/GameMain_BlockNomal_02") as Material;
        
    }

    void Start () {
        this.GetComponent<MeshRenderer>().material.color = Color.yellow;
        if (StartBlockFlg == true)
        {
            BurnFlg = true;
            SetBurn();
            SetMaterial();
        }
    }

    public void StartFlg()
    {
        if (StartBlockFlg == true)
        {
            BurnFlg = true;
            SetBurn();
            SetMaterial();
        }

    }
	
	public void SetMaterial()
    {
        this.GetComponent<MeshRenderer>().material = Mat_Collaps;
    }

    public void SetBurn(GameObject Obj)
    {
        //エフェクト操作
        SetFire.gameObject.SetActive(true);
        SetFire.gameObject.transform.position = new Vector3(this.transform.position.x,
                                                            this.transform.position.y+0.5f,
                                                            this.transform.position.z);
        
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
        BurnCnt += 0.05f;
        if (BurnCnt >= DefineScript.JUDGE_BURNNINGTIME)
        {
            BurnFlg = true;
            BurnCnt = 0.0f;
            SetBurn();
            SetMaterial();
            return true;
        }
        
        return false;
    }
}
