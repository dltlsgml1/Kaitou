using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour {

    GameObject StageSelectObject;
    GameObject Finger;
    StageSelect StageEnable;
    GameObject CameraData;
    bool PositionFlag = false;
    float Width = 1.9f;
    bool CameraPositionFlag = false;
    bool StagePositionFlag = false;
    public Vector3 Vec = new Vector3(10, 0, 0);
    public bool InitFlag = true;
    public float DefaultKey = 0.5f;         //このスティック以上倒すとキー入力判定
    public int StageID;

	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
    {
        StageID = PassStageID.PassStageId();
        Finger = GameObject.Find("Pause_Cursor");
        Finger.transform.position = new Vector3(-7.3f+((StageID-1) * Width), -1f, -11);
        StageSelectObject = GameObject.Find("StagePrefab");
        StageEnable=StageSelectObject.GetComponent<StageSelect>();
       
        CameraData = GameObject.Find("CameraObejct");
    }

    // Update is called once per frame
    void Update () {
        if (InitFlag)
        {
            MapOpen();
        }
        else
        {
            StageSelect();
        }

 
	}
    /*
        CameraObject
        Position x,0 y,-5 z,0
        Rotation x,20 y,0 z,0

        SS_StageList
        Position x,0 y,-6 z,-10
        Rotation x,0 y,0 z,0

        StagePrefab
        Position x,0 y,4 z,0
        Rotation x,0 y,0 z,0
     */
    public void MapOpen()
    {
        StageSelectObject.transform.position = new Vector3(0, 4, 0);
        CameraData.transform.position = new Vector3(0, -5, 0);
        CameraData.transform.rotation= Quaternion.Euler(20, 0, 0);

        this.transform.position = new Vector3(0, -6, -10);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);



       // if(PositionFlag&&CameraPositionFlag&&StagePositionFlag)
            InitFlag = false;
    }

    public void MapMove()
    {

    }

    public void StageSelect()
    {
        float XDecision;                                 //左右を判定用
        float YDecision;
        XDecision = Input.GetAxisRaw("LeftStick X");     //左スティックを取る
        if (XDecision != 0)
        {
            if (StageID < 32)
            {

                if (XDecision > DefaultKey)
                    StageID += 1;                           //左入力でステージナンバーが上がるはずなので上げる
            }

            if (XDecision < -DefaultKey)
            {
                StageID -= 1;                           //左入力でステージナンバーが下がるはずなので下げる
            }
        }
        YDecision = Input.GetAxisRaw("LeftStick Y");     //左スティックを取る
        if (YDecision != 0)
        {
            if (StageID < 32-6)
            {

                if (YDecision > DefaultKey)
                    StageID += 6;                           //左入力でステージナンバーが上がるはずなので上げる
            }

            if (StageID > 6)
            {
                if (YDecision < -DefaultKey)
                {
                    StageID -= 6;                           //左入力でステージナンバーが下がるはずなので下げる
                }
            }
        }
        if (Input.GetButtonDown("AButton"))
        {
            PassStageID.GetStageID(StageID);
            StageEnable.enabled = true;
            Debug.Log(StageID);
            CameraData.transform.position = new Vector3(0, 0, 0);
            CameraData.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.transform.position = new Vector3(-8.36f, -9.48f, -10);
            this.transform.rotation = Quaternion.Euler(50, 0, 10);
            InitFlag = true;
            this.enabled = false;
        }
        /*
Position X -8.36 Y-9.48 Z -10
Rotation X 50 Y 0 Z 10
*/
    }
}
