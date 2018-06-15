using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFade : MonoBehaviour {
    public GameObject ClearObj;
    public GameObject FailedObj;
    public GameObject FogObj;
    public bool StartFlag = false;
    public bool alphaFlag = false;
    public bool emissionUpFlag = false;
    public bool emissionDownFlag = false;
    public bool endFlag = false;
    public float FadeS = 0.2f;
    private Color ClearFadeSpeed;
    private Color FogFadeSpeed;
    private Color FailedFadeSpeed;

    // Use this for initialization
    void Start () {
        ClearFadeSpeed = new Color(ClearObj.GetComponent<Renderer>().material.color.r, ClearObj.GetComponent<Renderer>().material.color.g, ClearObj.GetComponent<Renderer>().material.color.b,FadeS);
        FogFadeSpeed = new Color(FogObj.GetComponent<Renderer>().material.color.r, FogObj.GetComponent<Renderer>().material.color.g, FogObj.GetComponent<Renderer>().material.color.b, FadeS);
        FailedFadeSpeed = new Color(FailedObj.GetComponent<Renderer>().material.color.r, FailedObj.GetComponent<Renderer>().material.color.g, FailedObj.GetComponent<Renderer>().material.color.b, FadeS);
    }

    // Update is called once per frame
    void Update () {

		
	}


    //void ClearAnima()
    //{
    //    //オブジェクトをOnにする
    //    ClearObj.SetActive(true);


    //    //alphaを上げる
    //    if(alphaFlag && !emssionUpFlag && !emissionDownFlag)
    //    {
    //        ClearObj.GetComponent<Renderer>().material.color += ClearFadeSpeed;
    //        FogObj.GetComponent<Renderer>().material.color += FogFadeSpeed;
    //        if(ClearObj.GetComponent<Renderer>().material.color.a >= 1.0f && FogObj.GetComponent<Renderer>().material.color.a >= 1.0f)
    //        {
    //            alphaFlag = false;
    //            emissionUpFlag
    //        }
    //    }

    //    //エミッションを上げる

    //    //エミッションを下げる


    //    FadeImage.enabled = true;
    //    Alpha += FadeSpeed;
    //    SetColor();
    //    if (Alpha >= 1)
    //    {
    //        In = false;
    //        FadeInEnd = true;
    //        FadeOutEnd = false;
    //    }

    //}
}
