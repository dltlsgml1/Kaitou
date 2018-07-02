using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class failed : MonoBehaviour {
    //float Alpha;
    private float Red, Gleen, Blue, Alpha;
    public float FadeSpeed=0.2f;
    public bool FadeInEnd = false;
    public bool FadeOutEnd = true;
    public bool Out = false;
    public bool In = false;

    Image FadeImage;

	// Use this for initialization
    void Start()
    {
        FadeImage = GetComponent<Image>();
        Red = FadeImage.color.r;
        Gleen = FadeImage.color.g;
        Blue = FadeImage.color.b;
        Alpha = FadeImage.color.a;

    }
	
	// Update is called once per frame
	void Update () {
        if (In)
        {
            StartFadeIn();
        }

        if (Out)
        {
            StartFadeOut();
        }

	}



    void StartFadeIn()
    {
        FadeImage.enabled = true;
        Alpha += FadeSpeed;
        SetColor();
        if (Alpha >= 1)
        {
            In = false;
            FadeInEnd = true;
            FadeOutEnd = false;
        }
       
    }


    void StartFadeOut()
    {
        Alpha -= FadeSpeed;
        SetColor();
        if (Alpha <= 0)
        {
            Out = false;
            FadeInEnd = false;
            FadeOutEnd = true;
            FadeImage.enabled = false;
        }

    }

    void SetColor()
    {
        FadeImage.color = new Color(Red, Gleen, Blue, Alpha);
    }

    public void FadeIn_On()
    {
        if (In == false)
        {
            In = true;
        }
    }
    public void FadeOut_On()
    {
        if (Out == false)
        {
            Out = true;
        }
    }

}
