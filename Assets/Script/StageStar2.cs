using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStar2 : MonoBehaviour {

    public struct light_container
    {
        public float light;
        public bool lightup;
    }
    public Material[] lightmat = new Material[10];
    //public GameObject SetStarlineObj;
    LifeStarRecive2 GetLimitStarLine;
    light_container[] Lcontainer = new light_container[10];



    // Use this for initialization
    void Start()
    {

        GetLimitStarLine = GetComponentInParent<LifeStarRecive2>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Lcontainer[i].light = 0.0f;
            Lcontainer[i].lightup = false;
        }
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<MeshRenderer>().material = lightmat[i];
            if (i < GetLimitStarLine.ReceiveLimitNum)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
                Lcontainer[i].light = 1.5f;

            }
            else
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
                Lcontainer[i].light = -0.5f;
            }
            Lcontainer[i].lightup = true;
            lightmat[i].SetColor("_EmissionColor", new Color(Lcontainer[i].light, Lcontainer[i].light, 0));
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (GetLimitStarLine.clearflg != true)
        {


            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (GetLimitStarLine.ReceiveLimitNum == 0)
                {
                    if (this.transform.GetChild(i).gameObject.activeSelf)
                    {
                        if (Lcontainer[i].lightup)
                        {

                            Lcontainer[i].light += 0.1f;
                            if (Lcontainer[i].light > 3.0f)
                            {
                                Lcontainer[i].lightup = false;
                            }
                        }
                        else
                        {
                            Lcontainer[i].light -= 0.1f;
                            if (Lcontainer[i].light < -0.5f)
                            {
                                Lcontainer[i].lightup = true;
                                this.transform.GetChild(i).gameObject.SetActive(false);
                            }
                        }
                    }
                }
                else
                {



                    if (i < GetLimitStarLine.ReceiveLimitNum+1)
                    {
                        Lcontainer[i].lightup = true;
                        Lcontainer[i].light = 1.5f;
                        this.transform.GetChild(i).gameObject.SetActive(true);

                    }
                    else
                    {
                        if (this.transform.GetChild(i).gameObject.activeSelf)
                        {
                            if (Lcontainer[i].lightup)
                            {

                                Lcontainer[i].light += 0.1f;
                                if (Lcontainer[i].light > 3.0f)
                                {
                                    Lcontainer[i].lightup = false;
                                }
                            }
                            else
                            {
                                Lcontainer[i].light -= 0.1f;
                                if (Lcontainer[i].light < -0.5f)
                                {
                                    Lcontainer[i].lightup = true;
                                    this.transform.GetChild(i).gameObject.SetActive(false);
                                }
                            }


                        }

                    }
                }


                lightmat[i].SetColor("_EmissionColor", new Color(Lcontainer[i].light, Lcontainer[i].light, 0));


            }
        }




    }
}
