using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiftfire : MonoBehaviour {

    public Vector3 NowPosition;
    private const float StartTime=0.0f;
    private const float EndTime=1.0f;
    private int count;
    private ParticleSystem[] ParticleController;

    public bool ShiftOn;
    public float speed;
    public float timer;
    public Vector3 StartPos;
    public Vector3 EndPos;



	// Use this for initialization
	void Start () {
        ParticleController = GetComponentsInChildren<ParticleSystem>();

        count = 0;
        timer = 0;
        ShiftOn = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (ShiftOn)
        {
            switch (count)
            {
                case 0:
                    count = 1;
                    for (int i = 0; i < 2; i++)
                    {
                        ParticleController[i].Play();
                    }
                    NowPosition = StartPos;
                    this.transform.localPosition = new Vector3(NowPosition.x, NowPosition.y, NowPosition.z);
                    break;
                case 1:
                    //Shift(NowPosition.x, StartPos.x, EndPos.x);
                    //Shift(NowPosition.y, StartPos.y, EndPos.y);
                    //Shift(NowPosition.z, StartPos.z, EndPos.z);
                    Shift();
                    this.transform.localPosition = new Vector3(NowPosition.x, NowPosition.y, NowPosition.z);
                    timer += speed;
                    if (timer > EndTime)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            ParticleController[i].Stop();
                        }
                        ShiftOn = false;
                    }
                    break;
            }
            
        }
        else
        {
            timer = 0.0f;
            count = 0;
        }



	}

    //変更後 = x1 + (x2 - x1) * (NowTime - TimeStart) / (TimeEnd - TimeStart);
    void Shift()
    {
        NowPosition.x = StartPos.x + (EndPos.x - StartPos.x) * (timer - StartTime) / (EndTime - StartTime);
        NowPosition.y = StartPos.y + (EndPos.y - StartPos.y) * (timer - StartTime) / (EndTime - StartTime);
        NowPosition.z = StartPos.z + (EndPos.z - StartPos.z) * (timer - StartTime) / (EndTime - StartTime);
        //Pos = start + (end - start) * (timer - StartTime) / (EndTime - StartTime);

        return;
    }

}

