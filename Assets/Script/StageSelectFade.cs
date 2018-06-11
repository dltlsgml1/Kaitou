using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectFade : MonoBehaviour {

    public float Speed = 0.01f;
    public bool FadeInFlag = true;
    public bool FadeOutFlag = false;
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
            }
        }

        if (FadeOutFlag)
        {
            FadeOut();
            if (alfa >= 1)
            {
                FadeOutFlag = false;
            }
        }
    }

    public void FadeIn()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa -= Speed;      
    }

    public void FadeOut()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += Speed;
    }
}
