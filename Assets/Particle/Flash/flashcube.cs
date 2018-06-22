using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashcube : MonoBehaviour {

    //GameObject Setblock;
    Blocks SetBlock;
    GameMain SetGameMain;
    bool oneroot;
	// Use this for initialization
	void Start () {
        oneroot = false;
        SetBlock = GetComponentInParent<Blocks>();
        SetGameMain = GameObject.Find("MainSceneScript").GetComponent<GameMain>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);

        }
        if (SetBlock.StartBlockFlg)
        {
                Destroy(GameObject.Find("FlashCubeParticle"));
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (!SetBlock.StartBlockFlg)
        {
            if (oneroot)
            {
                Destroy(GameObject.Find("FlashCubeParticle"));
            }


            if (SetBlock.BurnFlg && SetGameMain.buttonup)
            {
                    for (int i = 0; i < this.transform.childCount; i++)
                    {
                        this.transform.GetChild(i).gameObject.SetActive(true);

                    }
                    oneroot = true;
            }
           
        }

	}
}
