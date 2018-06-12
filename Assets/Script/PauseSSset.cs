using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSSset : MonoBehaviour {

    private static CsvLoad CsvData;
    public GameObject StageSS;
    

    // Use this for initialization
    void Start () {
        SSLood();
    }
	
	// Update is called once per frame
	void Update () {
        if (Pause.is_pause)
        {
            //SSLood();
            //SSset();
        }
	}


    void SSLood()
    {
        Renderer renderer;
        //GameObject obj;
        //for (int i = 0; i < 3; i++)
        //{
        Transform parent = this.transform;
        //Debug.Log("PauseSSPrefab/" + CsvData.StageDateList[PassStageID.PassStageId() - 1 + i].StageName);
        //StageSS = (GameObject)Resources.Load("PauseSSPrefab/" + CsvData.StageDateList[PassStageID.PassStageId()-1+i].StageName);
        //Texture2D tex2d = Resources.Load("PauseSSPrefab/" + PassStageID.StageName) as Texture2D;
        renderer = this.transform.Find("now_SS").GetComponent<Renderer>();
        renderer.materials[0].mainTexture = Resources.Load("PauseSStexture/" + PassStageID.StageName) as Texture2D;
    
        //}
    }

    void SSset()
    {

    }

}
