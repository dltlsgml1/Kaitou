using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLine2 : MonoBehaviour {

    public struct light_container
    {
        public float lightpower;
        public bool lightswitch;
    }
    public Material[] lightmat = new Material[9];
    LifeStarRecive2 GetLimitStarLine;
    light_container[] Lcontainer = new light_container[9];

    // Use this for initialization
    void Start()
    {
        GetLimitStarLine = GetComponentInParent<LifeStarRecive2>();
        LightLineInit();
    }

    // Update is called once per frame
    void Update()
    {

        if (GetLimitStarLine.clearflg != true)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (i < GetLimitStarLine.ReceiveLimitNum)
                {
                    LightLive(i);
                }
                else
                {
                    Light_Delete(i);
                }
                lightmat[i].SetColor("_EmissionColor", new Color(Lcontainer[i].lightpower, Lcontainer[i].lightpower, 0));
            }
        }

    }


    void LightLineInit()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<SpriteRenderer>().material = lightmat[i];
           
            if (i < GetLimitStarLine.ReceiveLimitNum)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
                Lcontainer[i].lightpower = 2.0f;

            }
            else
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
                Lcontainer[i].lightpower = -0.5f;
            }
            Lcontainer[i].lightswitch = true;
            lightmat[i].SetColor("_EmissionColor", new Color(Lcontainer[i].lightpower, Lcontainer[i].lightpower, 0));
        }
    }






    void LightLive(int materialcount)
    {
        this.transform.GetChild(materialcount).gameObject.SetActive(true);
        if (materialcount == (GetLimitStarLine.ReceiveLimitNum - 1))
        {
            LightUP_DOWN(materialcount);
        }
        else
        {
            Lcontainer[materialcount].lightpower = 2.0f;
        }
    }


    void LightUP_DOWN(int materialcount)
    {
        if (Lcontainer[materialcount].lightswitch)
        {
            Lcontainer[materialcount].lightpower += 0.2f;
            if (Lcontainer[materialcount].lightpower > 3.5f)
            {
                Lcontainer[materialcount].lightswitch = false;
            }
        }
        else
        {
            Lcontainer[materialcount].lightpower -= 0.2f;
            if (Lcontainer[materialcount].lightpower < 1.0f)
            {
                Lcontainer[materialcount].lightswitch = true;
            }
        }

    }






    void Light_Delete(int materialcount)
    {
        if (Lcontainer[materialcount].lightpower > 0)
        {
            Lcontainer[materialcount].lightpower -= 0.1f;
            if (Lcontainer[materialcount].lightpower < 0)
            {
                this.transform.GetChild(materialcount).gameObject.SetActive(false);
            }
        }
    }



    
    //
    
}
