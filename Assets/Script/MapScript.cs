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
    private bool SelectFlag = false;
    private bool FadeInit = false;
    private bool StartFade = false;
    private bool XStickFlag = false;
    public bool Xmoved;
    private bool YStickFlag = false;
    public bool Ymoved;
    public bool FingerMoveFlag = false;
    public bool EvenFlag = false;
    public Vector3 Vec = new Vector3(10, 0, 0);
    private Vector3 StartPosition;
    private Vector3 EndPosition;
    float Width = 2.15f;
    float Height = 1.31f;
    float rate = 0f;
    float EvenNumber = -3.52f;
    float OddNumber = -3.96f;
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
            Finger.transform.position = new Vector3(EvenNumber + ((FingerPos) * Width), -1+(-Decision * Height), -11);
        }
        else
        {
            Finger.transform.position = new Vector3(OddNumber + ((FingerPos) * Width), -1+(-Decision * Height), -11);
        }
        StageSelectObject = GameObject.Find("StagePrefab");
        StageEnable=StageSelectObject.GetComponent<StageSelect>();
        CameraData = GameObject.Find("CameraObejct");
        Ymoved = true;
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
        StageSelectObject.transform.position = new Vector3(0, 4, 0);
        CameraData.transform.position = new Vector3(0, -5, 0);
        CameraData.transform.rotation= Quaternion.Euler(20, 0, 0);

        this.transform.position = new Vector3(0.39f, -8.92f, -2.78f);
        this.transform.rotation = Quaternion.Euler(73.569f, 180, 0);



       // if(PositionFlag&&CameraPositionFlag&&StagePositionFlag)
            InitFlag = false;
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
            if (StageID > 0)
            {
                if (XStickFlag == true && Xmoved == false && XDecision < -0.5f || Input.GetKeyDown(KeyCode.LeftArrow))
                {
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
                    EndPosition.y=-1f+(-Decision*Height);
                }
            }
            if (StageID < MaxStage)
            {
                if (XStickFlag == true && Xmoved == false && XDecision > 0.5f || Input.GetKeyDown(KeyCode.RightArrow
                    ))
                {
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
                    EndPosition.y = -1f + (-Decision * Height);
                    Debug.Log(EndPosition);
                }
            }
        }
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
            if (StageID >= MaxStage - 6)
            {
                if (YStickFlag == true && Ymoved == false && YDecision < -0.5f || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    StageID = 29;
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
                    EndPosition.y = -1f + (-Decision * Height);
                    Ymoved = true;
                    FingerMoveFlag = true;
                }
            }
            if (StageID < MaxStage-6)
            {
                if (YStickFlag == true && Ymoved == false && YDecision < -0.5f || Input.GetKeyDown(KeyCode.DownArrow))
                {
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
                    EndPosition.y = -1f + (-Decision * Height);
                   
                    
                }
            }
            
            if (StageID <= 5)
            {
                if (YStickFlag == true && Ymoved == false && YDecision > 0.5f || Input.GetKeyDown(KeyCode.UpArrow))
                {
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
                    EndPosition.y = -1f + (-Decision * Height);
                    Ymoved = true;
                    FingerMoveFlag = true;
                }
            }
            if (StageID > 5)
            {
                if (YStickFlag == true && Ymoved == false && YDecision > 0.5f || Input.GetKeyDown(KeyCode.UpArrow))
                {
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
                    EndPosition.y = -1f + (-Decision * Height);
                    Ymoved = true;
                    FingerMoveFlag = true;
                }
            }
        }
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
                    FadeFlag.FadeOutFlag = true;
                    FadeInit = true;
                }
                if (!FadeFlag.FadeOutFlag)
                {
                    PassStageID.GetStageID(StageID);
                    StageEnable.enabled = true;
                    CameraData.transform.position = new Vector3(0, 0, 0);
                    CameraData.transform.rotation = Quaternion.Euler(0, 0, 0);
                    Finger.transform.position = new Vector3(100, 100, 100);
                    this.transform.position = new Vector3(-9.26f, -11.9f, -2.01f);
                    this.transform.rotation = Quaternion.Euler(80.68201f, 135.925f, -41.118f);
                    FadeFlag.FadeInFlag = true;
                    InitFlag = true;
                    FadeInit = false;
                    SelectFlag = false;
                    this.enabled = false;
                }
            }

        }
    }
}
