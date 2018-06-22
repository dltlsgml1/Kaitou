using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashcube : MonoBehaviour
{

    //GameObject Setblock;
    Blocks SetBlock;
    GameMain SetGameMain;

    ParticleSystem ppp;
    private bool oneroot = true;


    // Use this for initialization
    void Start()
    {

        SetBlock = GetComponentInParent<Blocks>();
        SetGameMain = GameObject.Find("MainSceneScript").GetComponent<GameMain>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            //this.transform.GetChild(i).gameObject.SetActive(false);
            ppp = this.transform.GetChild(i).GetComponent<ParticleSystem>();
            ppp.Stop();

        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!SetBlock.StartBlockFlg)
        {
            //if (oneroot)
            //{
            //    Destroy(GameObject.Find("FlashCubeParticle"));
            //}


            if (SetBlock.BurnFlg && SetGameMain.buttonup && oneroot)
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    //this.transform.GetChild(i).gameObject.SetActive(true);
                    ppp = this.transform.GetChild(i).GetComponent<ParticleSystem>();
                    ppp.Play();

                }
                oneroot = false;
            }

        }

    }
}
