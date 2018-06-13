using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour {

    GameObject StageSelectObject;
    GameObject Finger;
    StageSelect StageEnable;
    GameObject CameraData;
    GameObject Fade;
    StageSelectFade FadeFlag; 
    public bool InitFlag = true;
    bool CameraPositionFlag = false;
    bool StagePositionFlag = false;
    bool PositionFlag = false;
    bool RotationFlag = false;
    bool FingerFlag = false;
    bool FadeInInit = false;
    bool FadeOutIni = false;
    private bool SelectFlag = false;
    private bool FadeInit = false;
    private bool StartFade = false;
    private bool XStickFlag = false;
    public bool Xmoved;
    private bool YStickFlag = false;
    public bool Ymoved;
    public bool FingerMoveFlag = false;
    public bool EvenFlag = false;
    private bool MoveFlag = true;
    public Vector3 Vec = new Vector3(10, 0, 0);
    private Vector3 StartPosition;
    private Vector3 EndPosition;
    private Vector3 Rotation;
    Vector3 NowRotation;
    Vector3 StartRotation;
    Vector3 EndRotation;
    Vector3 StartFinger;
    Vector3 EndFinger;
    float Width = 2.15f;
    float Height = 1.35f;
    float rate = 0f;
    float rate2 = 0f;
    float EvenNumber = -2.2f;
    float OddNumber = -2.6f;
    float InitHeight = -0.8f;
    public float DefaultKey = 0.5f;         //このスティック以上倒すとキー入力判定
    public int StageID;
    public int MaxStage = 29;
    public int Decision = 0;
    public int FingerPos;

    // Use this for initialization
    void Start () {
	}

    private void OnEnable()
    {
        Fade = GameObject.Find("Panel");
        FadeFlag = Fade.GetComponent<StageSelectFade>();
        StageID = PassStageID.PassStageId();
        Finger = GameObject.Find("Pause_Cursor");
        Decision = StageID / 6;
        FingerPos = StageID % 6;
        switch (Decision)
        {
            case 0:
                EvenFlag = true;
                break;
            case 1:
                EvenFlag = false;
                break;
            case 2:
                EvenFlag = true;
                break;
            case 3:
                EvenFlag = false;
                break;
            case 4:
                EvenFlag = true;
                break;
            default:
                break;
        }
        if (EvenFlag)
        {
            EndFinger = new Vector3(EvenNumber + ((FingerPos) * Width), InitHeight + (-Decision * Height), -11);
        }
        else
        {
            EndFinger = new Vector3(OddNumber + ((FingerPos) * Width), InitHeight + (-Decision * Height), -11);
        }
        StageSelectObject = GameObject.Find("StagePrefab");
        StageEnable=StageSelectObject.GetComponent<StageSelect>();
        CameraData = GameObject.Find("CameraObejct");
        Ymoved = true;
        StartPosition = this.transform.position;
        StartRotation.x = this.transform.rotation.x;
        StartRotation.y = this.transform.rotation.y;
        StartRotation.z = this.transform.rotation.z;
        StartFinger = Finger.transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (InitFlag)
        {
            MapOpen();
        }
        else
        {
            if (FingerMoveFlag)
            {
                MapMove();
            }
            else
            {
                StageSelect();
                OutMap();
            }
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
        rate += 0.05f;
        EndPosition = new Vector3(0.39f, -8.92f, -2.78f);
        EndRotation = new Vector3(90f, 180, 0);
        if (!RotationFlag)
        {
            
            this.transform.rotation = Quaternion.Euler(Vector3.Lerp(StartRotation, EndRotation, rate));
            NowRotation = Vector3.Lerp(StartRotation, EndRotation, rate);
            if (NowRotation == EndRotation)
            {
                RotationFlag = true;
            }
        }
        if (!PositionFlag)
        {
            this.transform.position=Vector3.Lerp(StartPosition, EndPosition, rate);
            if (this.transform.position == EndPosition)
            {
                StartPosition.x = this.transform.rotation.x;
                StartPosition.y = this.transform.rotation.y;
                StartPosition.z = this.transform.rotation.z;
                PositionFlag = true;
            }
        }

        if (!StagePositionFlag)
        {
            StageSelectObject.transform.position = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, 4, 0), rate);
            if(StageSelectObject.transform.position==new Vector3(0, 4, 0))
            {
                StagePositionFlag = true;
            }
        }

        if(!CameraPositionFlag)
        {
            CameraData.transform.position = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, -5, 0), rate);
            Vector3 CameraPos = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(20, 0, 0), rate);
            CameraData.transform.rotation = Quaternion.Euler(CameraPos);
            if (CameraData.transform.position==new Vector3(0,-5,0)&&CameraPos==new Vector3(20,0,0))
            {
                CameraPositionFlag = true;
                rate = 0;
            }
        }
        if (PositionFlag&&RotationFlag&&StagePositionFlag&&CameraPositionFlag)
        {
            
            if (!FingerFlag)
            {
                rate += 0.01f;
                Finger.transform.position = Vector3.Lerp(StartFinger, EndFinger, rate);
                if (Finger.transform.position == EndFinger)
                {
                    FingerFlag = true;
                }
            }
            else
            {
                rate = 0;
                InitFlag = false;
                PositionFlag = false;
                RotationFlag = false;
                StagePositionFlag = false;
                CameraPositionFlag = false;
            }
        }
    }

    public void MapMove()
    {

            Finger.transform.position = Vector3.Lerp(StartPosition, EndPosition, rate);
            rate += 0.1f;
            //if (Finger.transform.position.x >= EndPosition.x)
            if(Finger.transform.position==EndPosition)
            {
                rate = 0f;
                FingerMoveFlag = false;
            }
        
       
    }

    public void StageSelect()
    {
        float XDecision;                                 //左右を判定用
        float YDecision;
        XDecision = Input.GetAxisRaw("LeftStick X");     //左スティックを取る
        if (XDecision != 0)
        {
            if (XDecision < -0.5f || XDecision > 0.5f)
            {
                XStickFlag = true;
            }
            else
            {
                XStickFlag = false;
                Xmoved = false;
            }
            if (StageID == 0&&MoveFlag)
            {
                if (XStickFlag == true && Xmoved == false && XDecision < -0.5f || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveFlag = false;
                    StageID = MaxStage;
                    Decision = StageID / 6;
                    FingerPos = StageID % 6;
                    switch (Decision)
                    {
                        case 0:
                            EvenFlag = true;
                            break;
                        case 1:
                            EvenFlag = false;
                            break;
                        case 2:
                            EvenFlag = true;
                            break;
                        case 3:
                            EvenFlag = false;
                            break;
                        case 4:
                            EvenFlag = true;
                            break;
                        default:
                            break;
                    }

                    Xmoved = true;
                    FingerMoveFlag = true;
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    if (EvenFlag)
                    {
                        EndPosition.x = EvenNumber + ((FingerPos) * Width);
                    }
                    else
                    {
                        EndPosition.x = OddNumber + ((FingerPos) * Width);
                    }
                    EndPosition.y = InitHeight + (-Decision * Height);
                }
            }
            if (StageID == MaxStage && MoveFlag)
            {
                if (XStickFlag == true && Xmoved == false && XDecision > 0.5f || Input.GetKeyDown(KeyCode.RightArrow
                    ))
                {
                    MoveFlag = false;
                    StageID = 0;
                    Decision = StageID / 6;
                    FingerPos = StageID % 6;
                    switch (Decision)
                    {
                        case 0:
                            EvenFlag = true;
                            break;
                        case 1:
                            EvenFlag = false;
                            break;
                        case 2:
                            EvenFlag = true;
                            break;
                        case 3:
                            EvenFlag = false;
                            break;
                        case 4:
                            EvenFlag = true;
                            break;
                        default:
                            break;
                    }
                    Xmoved = true;
                    FingerMoveFlag = true;
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    if (EvenFlag)
                    {
                        EndPosition.x = EvenNumber + ((FingerPos) * Width);
                    }
                    else
                    {
                        EndPosition.x = OddNumber + ((FingerPos) * Width);
                    }
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Debug.Log(EndPosition);
                }
            }
            if (StageID > 0 && MoveFlag)
            {
                if (XStickFlag == true && Xmoved == false && XDecision < -0.5f || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveFlag = false;
                    StageID -= 1;
                    Decision = StageID / 6;
                    FingerPos = StageID % 6;
                    switch (Decision)
                    {
                        case 0:
                            EvenFlag = true;
                            break;
                        case 1:
                            EvenFlag = false;
                            break;
                        case 2:
                            EvenFlag = true;
                            break;
                        case 3:
                            EvenFlag = false;
                            break;
                        case 4:
                            EvenFlag = true;
                            break;
                        default:
                            break;
                    }

                    Xmoved = true;
                    FingerMoveFlag = true;
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    if (EvenFlag)
                    {
                        EndPosition.x = EvenNumber + ((FingerPos) * Width);
                    }
                    else
                    {
                        EndPosition.x = OddNumber + ((FingerPos) * Width);
                    }
                    EndPosition.y=InitHeight+(-Decision*Height);
                }
            }
            if (StageID < MaxStage && MoveFlag)
            {
                if (XStickFlag == true && Xmoved == false && XDecision > 0.5f || Input.GetKeyDown(KeyCode.RightArrow
                    ))
                {
                    MoveFlag = false;
                    StageID += 1;
                    Decision = StageID / 6;
                    FingerPos = StageID % 6;
                    switch (Decision)
                    {
                        case 0:
                            EvenFlag = true;
                            break;
                        case 1:
                            EvenFlag = false;
                            break;
                        case 2:
                            EvenFlag = true;
                            break;
                        case 3:
                            EvenFlag = false;
                            break;
                        case 4:
                            EvenFlag = true;
                            break;
                        default:
                            break;
                    }
                    Xmoved = true;
                    FingerMoveFlag = true;
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    if (EvenFlag)
                    {
                        EndPosition.x = EvenNumber + ((FingerPos) * Width);
                    }
                    else
                    {
                        EndPosition.x = OddNumber + ((FingerPos) * Width);
                    }
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Debug.Log(EndPosition);
                }
            }

        }
        MoveFlag = true;
        YDecision = Input.GetAxisRaw("LeftStick Y");     //左スティックを取る

        if (YDecision != 0)
        {
            if (YDecision < -0.5f || YDecision > 0.5f)
            {
                YStickFlag = true;
            }
            else
            {
                YStickFlag = false;
                Ymoved = false;
            }

            
            if (StageID <= MaxStage-6&&MoveFlag)
            {
                if (YStickFlag == true && Ymoved == false && YDecision < -0.5f || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    MoveFlag = false;
                    Ymoved = true;
                    FingerMoveFlag = true;
                    StageID += 6;
                    Decision = StageID / 6;
                    FingerPos = StageID % 6;
                    switch (Decision)
                    {
                        case 0:
                            EvenFlag = true;
                            break;
                        case 1:
                            EvenFlag = false;
                            break;
                        case 2:
                            EvenFlag = true;
                            break;
                        case 3:
                            EvenFlag = false;
                            break;
                        case 4:
                            EvenFlag = true;
                            break;
                        default:
                            break;
                    }
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    if (EvenFlag)
                    {
                        EndPosition.x = EvenNumber + ((FingerPos) * Width);
                    }
                    else
                    {
                        EndPosition.x = OddNumber + ((FingerPos) * Width);
                    }
                    EndPosition.y = InitHeight + (-Decision * Height);
                   
                    
                }
            }
            if (StageID > 5 && MoveFlag)
            {
                if (YStickFlag == true && Ymoved == false && YDecision > 0.5f || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    MoveFlag = false;
                    StageID -= 6;
                    Decision = StageID / 6;
                    FingerPos = StageID % 6;
                    switch (Decision)
                    {
                        case 0:
                            EvenFlag = true;
                            break;
                        case 1:
                            EvenFlag = false;
                            break;
                        case 2:
                            EvenFlag = true;
                            break;
                        case 3:
                            EvenFlag = false;
                            break;
                        case 4:
                            EvenFlag = true;
                            break;
                        default:
                            break;
                    }
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    if (EvenFlag)
                    {
                        EndPosition.x = EvenNumber + ((FingerPos) * Width);
                    }
                    else
                    {
                        EndPosition.x = OddNumber + ((FingerPos) * Width);
                    }
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Ymoved = true;
                    FingerMoveFlag = true;
                }
            }
            if (StageID <= 5 && MoveFlag)
            {
                if (YStickFlag == true && Ymoved == false && YDecision > 0.5f || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    MoveFlag = false;
                    StageID = StageID + 24;
                    Decision = StageID / 6;
                    FingerPos = StageID % 6;
                    switch (Decision)
                    {
                        case 0:
                            EvenFlag = true;
                            break;
                        case 1:
                            EvenFlag = false;
                            break;
                        case 2:
                            EvenFlag = true;
                            break;
                        case 3:
                            EvenFlag = false;
                            break;
                        case 4:
                            EvenFlag = true;
                            break;
                        default:
                            break;
                    }
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    if (EvenFlag)
                    {
                        EndPosition.x = EvenNumber + ((FingerPos) * Width);
                    }
                    else
                    {
                        EndPosition.x = OddNumber + ((FingerPos) * Width);
                    }
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Ymoved = true;
                    FingerMoveFlag = true;
                }
            }
            if (StageID > MaxStage - 6 && MoveFlag)
            {
                if (YStickFlag == true && Ymoved == false && YDecision < -0.5f || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    MoveFlag = false;
                    StageID = StageID - 24;
                    Decision = StageID / 6;
                    FingerPos = StageID % 6;
                    switch (Decision)
                    {
                        case 0:
                            EvenFlag = true;
                            break;
                        case 1:
                            EvenFlag = false;
                            break;
                        case 2:
                            EvenFlag = true;
                            break;
                        case 3:
                            EvenFlag = false;
                            break;
                        case 4:
                            EvenFlag = true;
                            break;
                        default:
                            break;
                    }
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    if (EvenFlag)
                    {
                        EndPosition.x = EvenNumber + ((FingerPos) * Width);
                    }
                    else
                    {
                        EndPosition.x = OddNumber + ((FingerPos) * Width);
                    }
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Ymoved = true;
                    FingerMoveFlag = true;
                }
            }



        }
        MoveFlag = true;
        if (Input.GetButtonDown("AButton"))
        {
            SelectFlag = true;
        }

    }
    /*
Position X -8.36 Y-9.48 Z -10
Rotation X 50 Y 0 Z 10
*/
    public void OutMap()
    {
        if (SelectFlag)
        {
            if (!StartFade)
            {
                StartFade = true;
            }
            else
            {
                if (!FadeInit)
                {
                    Sound.PlaySe("Move", 0);
                    FadeFlag.FadeOutFlag = true;
                    FadeInit = true;
                }
                if (!FadeFlag.FadeOutFlag)
                {
                    PassStageID.GetStageID(StageID);
                    
                    if (!FadeInInit)
                    {
                        Sound.StopSe("Move", 0);
                        StageSelectObject.transform.position = new Vector3(-14.0f * (StageID), 4, 0);
                        FadeInInit = true;
                        FadeFlag.FadeInFlag = true;
                    }
                    if (!FadeFlag.FadeInFlag)
                    {
                        rate += 0.05f;
                        if (!StagePositionFlag)
                        {
                            StageSelectObject.transform.position = Vector3.Lerp(new Vector3(-14.0f * (StageID), 4, 0), new Vector3(-14.0f * (StageID), 0, 0), rate);
                            if (StageSelectObject.transform.position == new Vector3(-14.0f * (StageID), 0, 0))
                            {
                                StagePositionFlag = true;
                            }
                        }

                        if (!CameraPositionFlag)
                        {
                            CameraData.transform.position = Vector3.Lerp(new Vector3(0, -5, 0), new Vector3(0, 0, 0), rate);
                            Vector3 CameraPos = Vector3.Lerp(new Vector3(20, 0, 0), new Vector3(0, 0, 0), rate);
                            CameraData.transform.rotation = Quaternion.Euler(CameraPos);
                            if (CameraData.transform.position == new Vector3(0, 0, 0) && CameraPos == new Vector3(0, 0, 0))
                            {
                                CameraPositionFlag = true;
                                rate = 0;
                                StartFinger = Finger.transform.position;
                                EndFinger = new Vector3(11, -10, 0);
                                StartPosition = this.transform.position;
                                EndPosition= new Vector3(-9.26f, -11.9f, -2.01f);
                            }
                        }

                        if (CameraPositionFlag && StagePositionFlag)
                        {
                            rate2 += 0.05f;
                            if (!PositionFlag)
                            {
                                this.transform.position = Vector3.Lerp(StartPosition, EndPosition, rate2);
                                if (this.transform.position == EndPosition)
                                {
                                    PositionFlag = true;
                                }
                            }

                            Finger.transform.position = new Vector3(11, -10, 0);
                            if (PositionFlag)
                            {
                                this.transform.rotation = Quaternion.Euler(80.5f, 136f, -41f);
                                RotationFlag = true;
                            }
                            
                        }



                        if (PositionFlag && RotationFlag && StagePositionFlag && CameraPositionFlag)
                        {
                            rate = 0;
                            rate2 = 0;
                            FadeInInit = false;
                           InitFlag = true;
                            FadeInit = false;
                            SelectFlag = false;
                            FingerFlag = false;
                            StagePositionFlag = false;
                            PositionFlag = false;
                            RotationFlag = false;
                            CameraPositionFlag = false;
                            StageEnable.enabled = true;
                            this.enabled = false;
                        }
                    }
                }
            }

        }
    }
}
