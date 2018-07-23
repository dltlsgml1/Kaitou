using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
public class MovePose : MonoBehaviour {

    const float TimeStart = 0;
    const float TimeEnd = 1;

    private bool On;
    // private bool LinearInterpolation;  使わない変数はとりあえずコメント化　--6/25 李--
    private float NowTime;

    public Vector3  StocDisplayOn;
    public Vector3 StocDisplayOff;
    public Vector3 CalculatePosition;
    public float SlideSpeed;
    public bool SlideOn_Off;
    
    
    Transform rect_transform;
    


	// Use this for initialization
	void Start () {
=======
using UnityEngine.SceneManagement;

public class MovePose : MonoBehaviour
{

    const float TimeStart = 0;
    const float TimeEnd = 1;
    float rate = 0;
    private bool On;
    // private bool LinearInterpolation;  使わない変数はとりあえずコメント化　--6/25 李--
    private float NowTime;
    private string currentScene;
    public Vector3 StocDisplayOn;
    public Vector3 StocDisplayOff;
    public Vector3 StageSelectPosOn;
    public Vector3 StageSelectPosOff;
    public Vector3 StageSelectRotOn;
    public Vector3 StageSelectRotOff;
    public Vector3 CalculatePosition;
    public Vector3 CalculateRotation;
    public float SlideSpeed;
    public bool SlideOn_Off;
    public GameObject Map;
    private Vector3 StartPosition;
    private Vector3 EndPosition;
    Vector3 EndRotation;
    Vector3 StartRotation;
    Vector3 NowRotation;
    bool PositionFlag = false;
    bool RotationFlag = false;

    Transform rect_transform;



    // Use this for initialization
    void Start()
    {
>>>>>>> Dev
        rect_transform = GetComponent<Transform>();
        On = false;
        NowTime = 0;
        SlideOn_Off = false;
<<<<<<< HEAD
       // LinearInterpolation = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (SlideOn_Off)
        {
            if (On)//スライドOnの場合Offに
            {
                NowTime -= SlideSpeed;
                if (NowTime < TimeStart)
                {
                    NowTime = 0.0f;
                    On = false;
                    SlideOn_Off = false;
                }
            }
            else//スライドOffの場合Onに
            {
                NowTime += SlideSpeed;
                if (NowTime > TimeEnd)
                {
                    NowTime = 1.0f;
                    On = true;
                    SlideOn_Off = false;
                }
            }
           
        }

        MovePos();


        SetPosition();
       


		
	}
=======
        StartRotation.x = this.transform.localRotation.x;
        StartRotation.y = this.transform.localRotation.y;
        StartRotation.z = this.transform.localRotation.z;
        // LinearInterpolation = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "GameMain")
        {
            if (SlideOn_Off)
            {
                if (On)//スライドOnの場合Offに
                {
                    NowTime -= SlideSpeed;
                    if (NowTime < TimeStart)
                    {
                        NowTime = 0.0f;
                        On = false;
                        SlideOn_Off = false;
                    }
                }
                else//スライドOffの場合Onに
                {
                    NowTime += SlideSpeed;
                    if (NowTime > TimeEnd)
                    {
                        NowTime = 1.0f;
                        On = true;
                        SlideOn_Off = false;
                    }
                }

            }
            MovePos();
            SetPosition();
        }

        if (currentScene == "StageSelect")
        {
            if (SlideOn_Off)
            {
                if (On)//スライドOnの場合Offに
                {
                    Map.SetActive(true);
                    NowTime -= SlideSpeed;
                    if (NowTime < TimeStart)
                    {
                        NowTime = 0.0f;
                        On = false;
                        SlideOn_Off = false;
                    }
                }
                else//スライドOffの場合Onに
                {
                    Map.SetActive(false);
                    NowTime += SlideSpeed;
                    if (NowTime > TimeEnd)
                    {
                        NowTime = 1.0f;
                        On = true;
                        SlideOn_Off = false;
                    }
                }

            }
            StageSelectMovePos();
            StageSelectSetPosition();
        }


    }
>>>>>>> Dev
    void MovePos()
    {
        //CalculatePosition.x = StocDisplayOff.x + (StocDisplayOn.x - StocDisplayOff.x) * (NowTime - TimeStart) / (TimeEnd - TimeStart);
        CalculatePosition.y = StocDisplayOff.y + (StocDisplayOn.y - StocDisplayOff.y) * (NowTime - TimeStart) / (TimeEnd - TimeStart);
        //CalculatePosition.z = StocDisplayOff.z + (StocDisplayOn.z - StocDisplayOff.z) * (NowTime - TimeStart) / (TimeEnd - TimeStart);
<<<<<<< HEAD

    }

=======
    }

    void StageSelectMovePos()
    {
        //CalculatePosition.x = StageSelectPosOff.x + (StageSelectPosOn.x - StageSelectPosOff.x) * (NowTime - TimeStart) / (TimeEnd - TimeStart);
        CalculatePosition.y = StageSelectPosOff.y + (StageSelectPosOn.y - StageSelectPosOff.y) * (NowTime - TimeStart) / (TimeEnd - TimeStart);
        CalculatePosition.z = StageSelectPosOff.z + (StageSelectPosOn.z - StageSelectPosOff.z) * (NowTime - TimeStart) / (TimeEnd - TimeStart);
        CalculateRotation.x = StageSelectRotOff.x + (StageSelectRotOn.x - StageSelectRotOff.x) * (NowTime - TimeStart) / (TimeEnd - TimeStart);
        CalculateRotation.y = StageSelectRotOff.y + (StageSelectRotOn.y - StageSelectRotOff.y) * (NowTime - TimeStart) / (TimeEnd - TimeStart);
        CalculateRotation.z = StageSelectRotOff.z + (StageSelectRotOn.z - StageSelectRotOff.z) * (NowTime - TimeStart) / (TimeEnd - TimeStart);

    }
>>>>>>> Dev

    void SetPosition()
    {
        //rect_transform.position = new Vector3(CalculatePosition.x, CalculatePosition.y, CalculatePosition.z);
        rect_transform.localPosition = new Vector3(CalculatePosition.x, CalculatePosition.y, CalculatePosition.z);
    }
<<<<<<< HEAD
=======

    void StageSelectSetPosition()
    {
        rect_transform.localPosition = new Vector3(CalculatePosition.x, CalculatePosition.y, CalculatePosition.z);
        rect_transform.rotation = Quaternion.Euler(CalculateRotation);
    }
>>>>>>> Dev
}
