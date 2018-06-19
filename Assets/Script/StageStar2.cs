using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStar2 : MonoBehaviour {

    //public GameObject SetStarObj;
    LifeStarRecive2 GetLimitStar;
    // Use this for initialization
    void Start()
    {
        GetLimitStar = GetComponentInParent<LifeStarRecive2>();

    }

    // Update is called once per frame
    void Update()
    {

        if (GetLimitStar.clearflg != true)
        {

            for (int i = 0; i < this.transform.childCount; i++)
            {

                if (GetLimitStar.ReceiveLimitNum <= 0)
                {
                    this.transform.GetChild(i).gameObject.SetActive(false);
                }

                else
                {
                    if (i < GetLimitStar.ReceiveLimitNum + 1)
                    {
                        this.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        this.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
        }






    }
}
