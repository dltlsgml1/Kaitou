using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifestarReceive : MonoBehaviour
{
    public int ReceiveLimitNum;
    private GameMain Main;

	// Use this for initialization
	void Start () {
        Main = GameObject.Find("MainSceneScript").GetComponent<GameMain>();
        ReceiveLimitNum = PassStageID.PassUpperCount();
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        ReceiveLimitNum = Main.Limit;
    }
}
