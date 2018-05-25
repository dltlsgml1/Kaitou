using System.Collections;
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

    public Material StandardMaterial;
    public Material BurnMaterial;
    public bool BurnFlg = false;
    public bool StartBlockFlg;

    // Use this for initialization
    void Start () {
        this.GetComponent<MeshRenderer>().material.color = Color.yellow;
        if(StartBlockFlg == true)
        {
            BurnFlg = true;
            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        
	}

    public void SetBurn(GameObject Obj)
    {
        Obj.GetComponent<MeshRenderer>().material.color = Color.red;
    }



}
