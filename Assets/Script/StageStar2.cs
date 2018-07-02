using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStar2 : MonoBehaviour {

    struct light_container
    {
        public float light;
        public bool lightup;
    }
    public Material[] lightmat = new Material[10];
    //public GameObject SetStarlineObj;
    LifeStarRecive2 GetLimitStar;
    light_container[] Lcontainer = new light_container[10];



    // Use this for initialization
    void Start()
    {

        GetLimitStar = GetComponentInParent<LifeStarRecive2>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Lcontainer[i].light = 0.0f;
            Lcontainer[i].lightup = false;
        }
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<MeshRenderer>().material = lightmat[i];
            if (i < GetLimitStar.ReceiveLimitNum)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
                Lcontainer[i].light = 2.0f;

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

        if (GetLimitStar.clearflg != true)
        {


            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (GetLimitStar.ReceiveLimitNum == 0)
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



                    if (i < GetLimitStar.ReceiveLimitNum+1)
                    {
                        Lcontainer[i].lightup = true;
                        Lcontainer[i].light = 2.0f;
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
