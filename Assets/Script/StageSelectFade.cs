using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectFade : MonoBehaviour {

    public float Speed = 0.01f;
    public bool FadeInFlag = true;
    public bool FadeOutFlag = false;
    bool FadeInit = false;
    float alfa=0;
    float red, green, blue;

	// Use this for initialization
	void Start () {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
	}
	
	// Update is called once per frame
	void Update () {
        if (FadeInFlag)
        {
            FadeIn();
            if (alfa <= 0)
            {
                FadeInFlag = false;
                FadeInit = false;
            }
        }

        if (FadeOutFlag)
        {
            FadeOut();
            if (alfa >= 1)
            {
                FadeOutFlag = false;
                FadeInit = false;
            }
        }
    }

    public void FadeIn()
    {
        if (!FadeInit)
        {
            alfa = 1;
            FadeInit = true;
        }
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa -= Speed;      
    }

    public void FadeOut()
    {
        if(!FadeInit)
        {
            alfa = 0;
            FadeInit = true;
        }
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += Speed;
    }
}
