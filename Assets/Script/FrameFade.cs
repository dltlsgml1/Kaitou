using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameFade : MonoBehaviour {
    public float Speed = 0.001f;
    public static bool FadeInFlag = true;
    public static bool FadeOutFlag = false;
    bool FadeInit = false;
    float alfa = 0;
    float time = 0.3f;
    float red, green, blue;

    // Use this for initialization
    void Start()
    {
        red = GetComponent<SpriteRenderer>().color.r;
        green = GetComponent<SpriteRenderer>().color.g;
        blue = GetComponent<SpriteRenderer>().color.b;
        Speed = 1f / (time * 60f);
    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("アウト");
            alfa = 1;
            FadeInit = true;
        }
        GetComponent<SpriteRenderer>().color = new Color(red, green, blue, alfa);
        alfa -= Speed;
    }

    public void FadeOut()
    {
        if (!FadeInit)
        {
            Debug.Log("イン");
            alfa = 0;
            FadeInit = true;
        }
        GetComponent<SpriteRenderer>().color = new Color(red, green, blue, alfa);
        alfa += Speed;
    }
}
