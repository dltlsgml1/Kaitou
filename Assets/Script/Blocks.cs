﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {

    //public bool burnFlg { get; private set; }            //燃えているか判定
    //public bool startBlock { get; private set; }            //このブロックがスタートブロックなのか判定
    //public float burnCount { get; set; }               //このブロックは何秒で燃え移るのかを持つデータ

    //private short blockID {get;set;}                  //このブロックのID。CSVから読み込みする可能性あり

    //public GameObject Model { get; private set; }                      //ブレンダーなどで作られた３Dモデル。現状３Dモデルの型がきまってないから、とりあえずGameObjectで。
    //public GameObject Texture { get; private set; }                     //このブロックが持つテクスチャ。同じ理由でGameObject.

    //public GameObject StandardTexture;
    //public GameObject BurnTexture;

    public GameObject SetFire;
    public Material StandardMaterial;
    public Material BurnMaterial;
    public bool BurnFlg;
    public bool StartBlockFlg;

    // Use this for initialization
    void Start () {
        this.GetComponent<MeshRenderer>().material.color = Color.yellow;
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

    // マテリアルの変更
    private void ChangeMaterial(Material mat)
    {
        this.GetComponent<Renderer>().material = mat;
    }

    // マテリアルの初期化
    protected void InitMaterial()
    {
        if (StartBlockFlg)
        {
            BurnFlg = true;
            this.GetComponent<Renderer>().material = BurnMaterial;
        }
        else
        {
            this.GetComponent<Renderer>().material = StandardMaterial;
        }

    }
}
