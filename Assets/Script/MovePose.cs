using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        rect_transform = GetComponent<Transform>();
        On = false;
        NowTime = 0;
        SlideOn_Off = false;
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
    void MovePos()
    {
        //CalculatePosition.x = StocDisplayOff.x + (StocDisplayOn.x - StocDisplayOff.x) * (NowTime - TimeStart) / (TimeEnd - TimeStart);
        CalculatePosition.y = StocDisplayOff.y + (StocDisplayOn.y - StocDisplayOff.y) * (NowTime - TimeStart) / (TimeEnd - TimeStart);
        //CalculatePosition.z = StocDisplayOff.z + (StocDisplayOn.z - StocDisplayOff.z) * (NowTime - TimeStart) / (TimeEnd - TimeStart);

    }


    void SetPosition()
    {
        //rect_transform.position = new Vector3(CalculatePosition.x, CalculatePosition.y, CalculatePosition.z);
        rect_transform.localPosition = new Vector3(CalculatePosition.x, CalculatePosition.y, CalculatePosition.z);
    }
}
