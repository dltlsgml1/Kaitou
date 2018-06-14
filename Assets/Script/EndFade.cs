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
    public float FogEmissionFadeS = 0.25f;
    private Color ClearFadeSpeed;
    private Color FogFadeSpeed;
    private Color FailedFadeSpeed;
    private Color FogEmissionUp;
    private Color FogEmissionDown;


    // Use this for initialization
    void Start () {
        ClearFadeSpeed = new Color(ClearObj.GetComponent<Renderer>().material.color.r, ClearObj.GetComponent<Renderer>().material.color.g, ClearObj.GetComponent<Renderer>().material.color.b,FadeS);
        FogFadeSpeed = new Color(FogObj.GetComponent<Renderer>().material.color.r, FogObj.GetComponent<Renderer>().material.color.g, FogObj.GetComponent<Renderer>().material.color.b, FadeS);
        FailedFadeSpeed = new Color(FailedObj.GetComponent<Renderer>().material.color.r, FailedObj.GetComponent<Renderer>().material.color.g, FailedObj.GetComponent<Renderer>().material.color.b, FadeS);
        FogEmissionUp = new Color(FailedObj.GetComponent<Renderer>().material.color.r, FailedObj.GetComponent<Renderer>().material.color.g, FailedObj.GetComponent<Renderer>().material.color.b, FadeS);
        FogEmissionDown = new Color(FailedObj.GetComponent<Renderer>().material.color.r, FailedObj.GetComponent<Renderer>().material.color.g, FailedObj.GetComponent<Renderer>().material.color.b, FogEmissionFadeS);

    }

    // Update is called once per frame
    void Update () {

		
	}


    void ClearAnima()
    {
        //オブジェクトをOnにする
        ClearObj.SetActive(true);


        //alphaを上げる
        if(alphaFlag)
        {
            ClearObj.GetComponent<Renderer>().material.color += ClearFadeSpeed;
            FogObj.GetComponent<Renderer>().material.color += FogFadeSpeed;
            if(ClearObj.GetComponent<Renderer>().material.color.a >= 1.0f && FogObj.GetComponent<Renderer>().material.color.a >= 1.0f)
            {
                alphaFlag = false;
                emissionUpFlag = true;
            }
        }
        //エミッションを上げる
        if(emissionUpFlag)
        {
            FogEmissionUp = new Color(FailedObj.GetComponent<Renderer>().material.color.r, FailedObj.GetComponent<Renderer>().material.color.g, FailedObj.GetComponent<Renderer>().material.color.b, FadeS);
            FogObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", FogEmissionUp);

            //sif()
        }

        //エミッションを下げる
        if (emissionDownFlag)
        {

        }

    }
}
