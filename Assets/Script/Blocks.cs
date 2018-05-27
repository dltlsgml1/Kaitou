using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {


    public GameObject SetFire;
    public Material StandardMaterial;
    public Material BurnMaterial;
    public bool BurnFlg;
    public bool StartBlockFlg;
    public float BurnCnt;

    // Use this for initialization
    void Start () {
        this.GetComponent<MeshRenderer>().material.color = Color.yellow;
        if (StartBlockFlg == true)
        {
            BurnFlg = true;
            SetBurn();
            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
	
	// Update is called once per frame
	void Update () {


        
	}

    public void SetBurn(GameObject Obj)
    {
        //エフェクト操作
        SetFire.gameObject.SetActive(true);
        SetFire.gameObject.transform.position = new Vector3(this.transform.position.x,
                                                            this.transform.position.y+0.5f,
                                                            this.transform.position.z);
        Obj.GetComponent<MeshRenderer>().material.color = Color.red;
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
        BurnCnt += 0.05f;
        if (BurnCnt >= DefineScript.JUDGE_BURNNINGTIME)
        {
            BurnCnt = 0.0f;
            SetBurn();
            return true;
        }
        
        return false;
    }
}
