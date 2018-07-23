using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{

    GameObject StageSelectObject;
    public GameObject Finger;
    StageSelect StageEnable;
    GameObject CameraData;
    GameObject Fade;
    StageSelectFade FadeFlag;
    public GameObject spotlight;
    public GameObject Frame;
    public bool InitFlag = true;
    bool CameraPositionFlag = false;
    bool StagePositionFlag = false;
    bool PositionFlag = false;
    bool RotationFlag = false;
    bool FingerFlag = false;
    bool FadeInInit = false;
<<<<<<< HEAD
=======
    bool KeyUpFlag = false;
    bool keyDownFlag = false;
>>>>>>> Dev
    bool SelectButtonFlag = false;
    public static bool Is_Map = false;
    private bool SelectFlag = false;
    private bool FadeInit = false;
    private bool StartFade = false;
    private bool XStickFlag = false;
    public bool Xmoved;
    private bool YStickFlag = false;
    public bool Ymoved;
    public bool FingerMoveFlag = false;
    private bool MoveFlag = true;
    public Vector3 Vec = new Vector3(10, 0, 0);
    private Vector3 StartPosition;
    private Vector3 EndPosition;
    private Vector3 Rotation;
    Vector3 NowRotation;
    Vector3 StartRotation;
    Vector3 EndRotation;
    Vector3 StartFinger;
    Vector3 FramePosition;
    Vector3 EndFinger;
    float Width = 2.15f;
    float Height = 1.35f;
    float rate = 0f;
    float rate2 = 0f;
    float EvenNumber = -2.2f;
    // float OddNumber = -2.6f; 使わない変数はとりあえずコメント化　--6/25 李--
    float InitHeight = -0.8f;
    public float DefaultKey = 0.5f;         //このスティック以上倒すとキー入力判定
    public int StageID;
    private int MaxStage = 19;
    public int Decision = 0;
    public int FingerPos;

    // Use this for initialization
    void Start()
    {
    }

    private void OnEnable()
    {
        Is_Map = true;
        Sound.StopSe("Move", 0);
<<<<<<< HEAD

=======
        Sound.PlaySe("MapIn", 4);
     //   this.transform.rotation = Quaternion.Euler(90f, 180, 0);
>>>>>>> Dev
        StageSelectObject = GameObject.Find("StagePrefab");
        StageEnable = StageSelectObject.GetComponent<StageSelect>();
        spotlight.SetActive(true);
        Fade = GameObject.Find("Panel");
        FadeFlag = Fade.GetComponent<StageSelectFade>();
        StageID = PassStageID.PassStageId();
        if (StageID == 0)
        {
            StageID = PassStageID.PassStageId();
<<<<<<< HEAD
            StageSelectObject.transform.position = new Vector3(-14.0f * (StageID), 4, 0);
=======
//            StageSelectObject.transform.position = new Vector3(-14.0f * (StageID), 4, 0);
>>>>>>> Dev

        }
        else
        {
            StageID = PassStageID.PassStageId() - 1;
<<<<<<< HEAD
            StageSelectObject.transform.position = new Vector3(-14.0f * (StageID + 1), 4, 0);
=======
  //          StageSelectObject.transform.position = new Vector3(-14.0f * (StageID + 1), 4, 0);
>>>>>>> Dev
        }
        Decision = StageID / 5;
        FingerPos = StageID % 5;
        EndFinger = new Vector3(EvenNumber + ((FingerPos) * Width), InitHeight + (-Decision * Height), -11);
<<<<<<< HEAD
        FramePosition = new Vector3(-4.24f + ((FingerPos) * 2.33f), 0.04f + (-Decision * 1.589f), -10);
=======
        FramePosition = new Vector3(3.3f + ((FingerPos) * -1.65f), 0.11f, -3.35f + (Decision * 0.91f));
>>>>>>> Dev
        CameraData = GameObject.Find("CameraObejct");
        Ymoved = true;
        StartPosition = this.transform.position;
        StartRotation.x = this.transform.rotation.x;
        StartRotation.y = this.transform.rotation.y;
        StartRotation.z = this.transform.rotation.z;
        StartFinger = Finger.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.is_pause) { return; }
        if (InitFlag)
        {
<<<<<<< HEAD
            Sound.PlaySe("MapIn", 4);
=======
>>>>>>> Dev
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
                if (SelectButtonFlag && !FrameFade.FadeOutFlag || SelectFlag && !FrameFade.FadeOutFlag)
                {
                    OutMap();
                }
                else
                {
                    if (!FrameFade.FadeOutFlag)
                        StageSelect();
                }

            }
        }


    }
    public void MapOpen()
    {
        rate += 0.05f;
<<<<<<< HEAD
        EndPosition = new Vector3(0.39f, -8f, -4f);
        EndRotation = new Vector3(90f, 180, 0);
        if (!RotationFlag)
        {
            rate2 += 0.05f;
=======
        EndPosition = new Vector3(0.39f, -7.75f, -3.9f);
        EndRotation = new Vector3(114.464f, 360, 180);
        if (!RotationFlag)
        {
            
>>>>>>> Dev

            this.transform.rotation = Quaternion.Euler(Vector3.Lerp(StartRotation, EndRotation, rate2));
            NowRotation = Vector3.Lerp(StartRotation, EndRotation, rate2);
            if (NowRotation == EndRotation)
            {
                RotationFlag = true;
            }
        }
        if (!PositionFlag)
        {
<<<<<<< HEAD
=======
            rate2 += 0.05f;
>>>>>>> Dev
            this.transform.position = Vector3.Lerp(StartPosition, EndPosition, rate2);
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
            StageSelectObject.transform.position = Vector3.Lerp(new Vector3(StageSelectObject.transform.position.x, 0, 0), new Vector3(StageSelectObject.transform.position.x, 4, 0), rate);
            if (StageSelectObject.transform.position == new Vector3(StageSelectObject.transform.position.x, 4, 0))
            {
                StagePositionFlag = true;
            }
        }

        if (!CameraPositionFlag)
        {
            CameraData.transform.position = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, -5, 0), rate);
            Vector3 CameraPos = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(20, 0, 0), rate);
            CameraData.transform.rotation = Quaternion.Euler(CameraPos);
            if (CameraData.transform.position == new Vector3(0, -5, 0) && CameraPos == new Vector3(20, 0, 0))
            {
                CameraPositionFlag = true;
                rate = 0;
            }
        }
        if (PositionFlag && RotationFlag && StagePositionFlag && CameraPositionFlag)
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
                rate2 = 0;
                InitFlag = false;
                PositionFlag = false;
                RotationFlag = false;
                StagePositionFlag = false;
                CameraPositionFlag = false;
<<<<<<< HEAD
                Frame.transform.position = FramePosition;
                Frame.SetActive(true);
                FrameFade.FadeOutFlag = true;
=======
                Frame.transform.localPosition = FramePosition;
                Frame.SetActive(true);
                FrameFade.FadeOutFlag = true;
                StartRotation.x = this.transform.rotation.x;
                StartRotation.y = this.transform.rotation.y;
                StartRotation.z = this.transform.rotation.z;
                StartPosition = this.transform.position;
>>>>>>> Dev
            }
        }
    }

    public void MapMove()
    {

        if (FingerMoveFlag && !FrameFade.FadeOutFlag && !FrameFade.FadeInFlag && rate == 0)
        {
            rate = 1;
<<<<<<< HEAD
            Debug.Log("入ってる");
=======

>>>>>>> Dev
            FrameFade.FadeInFlag = true;
        }
        if (!FrameFade.FadeInFlag)
        {

            rate = 0f;
<<<<<<< HEAD
            Frame.transform.position = FramePosition;
            FrameFade.FadeOutFlag = true;
            FingerMoveFlag = false;
=======
            Frame.transform.localPosition = FramePosition;
            FrameFade.FadeOutFlag = true;
            FingerMoveFlag = false;
            Sound.PlaySe("MapSelect", 3);
>>>>>>> Dev
        }

    }

    public void StageSelect()
    {

        float XDecision;                                 //左右を判定用
        float YDecision;
        XDecision = Input.GetAxisRaw("LeftStick X");     //左スティックを取る
<<<<<<< HEAD
=======
        if (XDecision == 0)
        {
            XDecision = 0.1f;

        }
>>>>>>> Dev
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
            if (StageID == 0 && MoveFlag)
            {
                if (XStickFlag == true && Xmoved == false && XDecision < -0.5f || Input.GetKeyDown(KeyCode.LeftArrow))
                {

                    MoveFlag = false;
                    StageID = MaxStage;
                    Decision = StageID / 5;
                    FingerPos = StageID % 5;

                    Xmoved = true;
                    FingerMoveFlag = true;
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;

                    EndPosition.x = EvenNumber + ((FingerPos) * Width);
<<<<<<< HEAD
                    FramePosition = new Vector3(-4.24f + ((FingerPos) * 2.33f), 0.04f + (-Decision * 1.589f), -10);

                    EndPosition.y = InitHeight + (-Decision * Height);
                    Sound.PlaySe("MapSelect", 3);
=======
                    FramePosition = new Vector3(3.3f + ((FingerPos) * -1.65f), 0.11f, -3.35f + (Decision * 0.91f));

                    EndPosition.y = InitHeight + (-Decision * Height);
>>>>>>> Dev

                }
            }
            if (StageID == MaxStage && MoveFlag)
            {
                if (XStickFlag == true && Xmoved == false && XDecision > 0.5f || Input.GetKeyDown(KeyCode.RightArrow
                    ))
                {

                    MoveFlag = false;
                    StageID = 0;
                    Decision = StageID / 5;
                    FingerPos = StageID % 5;
                    Xmoved = true;
                    FingerMoveFlag = true;
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    EndPosition.x = EvenNumber + ((FingerPos) * Width);
<<<<<<< HEAD
                    FramePosition = new Vector3(-4.24f + ((FingerPos) * 2.33f), 0.04f + (-Decision * 1.589f), -10);

                    EndPosition.y = InitHeight + (-Decision * Height);
                    Sound.PlaySe("MapSelect", 3);
=======
                    FramePosition = new Vector3(3.3f + ((FingerPos) * -1.65f), 0.11f, -3.35f + (Decision * 0.91f));

                    EndPosition.y = InitHeight + (-Decision * Height);
>>>>>>> Dev

                }
            }
            if (StageID > 0 && MoveFlag)
            {
                if (XStickFlag == true && Xmoved == false && XDecision < -0.5f || Input.GetKeyDown(KeyCode.LeftArrow))
                {

                    MoveFlag = false;
                    StageID -= 1;
                    Decision = StageID / 5;
                    FingerPos = StageID % 5;

                    Xmoved = true;
                    FingerMoveFlag = true;
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    EndPosition.x = EvenNumber + ((FingerPos) * Width);
<<<<<<< HEAD
                    FramePosition = new Vector3(-4.24f + ((FingerPos) * 2.33f), 0.04f + (-Decision * 1.589f), -10);
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Sound.PlaySe("MapSelect", 3);
=======
                    FramePosition = new Vector3(3.3f + ((FingerPos) * -1.65f), 0.11f, -3.35f + (Decision * 0.91f));
                    EndPosition.y = InitHeight + (-Decision * Height);
>>>>>>> Dev

                }
            }
            if (StageID < MaxStage && MoveFlag)
            {
                if (XStickFlag == true && Xmoved == false && XDecision > 0.5f || Input.GetKeyDown(KeyCode.RightArrow
                    ))
                {

                    MoveFlag = false;
                    StageID += 1;
                    Decision = StageID / 5;
                    FingerPos = StageID % 5;
                    Xmoved = true;
                    FingerMoveFlag = true;
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    EndPosition.x = EvenNumber + ((FingerPos) * Width);
<<<<<<< HEAD
                    FramePosition = new Vector3(-4.24f + ((FingerPos) * 2.33f), 0.04f + (-Decision * 1.589f), -10);
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Sound.PlaySe("MapSelect", 3);
=======
                    FramePosition = new Vector3(3.3f + ((FingerPos) * -1.65f), 0.11f, -3.35f + (Decision * 0.91f));
                    EndPosition.y = InitHeight + (-Decision * Height);
>>>>>>> Dev

                }
            }

        }
        MoveFlag = true;
        YDecision = Input.GetAxisRaw("LeftStick Y");     //左スティックを取る
<<<<<<< HEAD

=======
        if (YDecision == 0) {
            YDecision = 0.1f;
        }
>>>>>>> Dev
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


            if (StageID <= MaxStage - 5 && MoveFlag)
            {
                if (YStickFlag == true && Ymoved == false && YDecision < -0.5f || Input.GetKeyDown(KeyCode.DownArrow))
                {

                    MoveFlag = false;
                    Ymoved = true;
                    FingerMoveFlag = true;
                    StageID += 5;
                    Decision = StageID / 5;
                    FingerPos = StageID % 5;
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    EndPosition.x = EvenNumber + ((FingerPos) * Width);
<<<<<<< HEAD
                    FramePosition = new Vector3(-4.24f + ((FingerPos) * 2.33f), 0.04f + (-Decision * 1.589f), -10);
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Sound.PlaySe("MapSelect", 3);
=======
                    FramePosition = new Vector3(3.3f + ((FingerPos) * -1.65f), 0.11f, -3.35f + (Decision * 0.91f));
                    EndPosition.y = InitHeight + (-Decision * Height);
>>>>>>> Dev


                }
            }
            if (StageID > 4 && MoveFlag)
            {
                if (YStickFlag == true && Ymoved == false && YDecision > 0.5f || Input.GetKeyDown(KeyCode.UpArrow))
                {

                    MoveFlag = false;
                    StageID -= 5;
                    Decision = StageID / 5;
                    FingerPos = StageID % 5;
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    EndPosition.x = EvenNumber + ((FingerPos) * Width);
<<<<<<< HEAD
                    FramePosition = new Vector3(-4.24f + ((FingerPos) * 2.33f), 0.04f + (-Decision * 1.589f), -10);
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Ymoved = true;
                    FingerMoveFlag = true;
                    Sound.PlaySe("MapSelect", 3);
=======
                    FramePosition = new Vector3(3.3f + ((FingerPos) * -1.65f), 0.11f, -3.35f + (Decision * 0.91f));
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Ymoved = true;
                    FingerMoveFlag = true;
                    
>>>>>>> Dev

                }
            }
            if (StageID <= 4 && MoveFlag)
            {
                if (YStickFlag == true && Ymoved == false && YDecision > 0.5f || Input.GetKeyDown(KeyCode.UpArrow))
                {

                    MoveFlag = false;
                    StageID = StageID + 15;
                    Decision = StageID / 5;
                    FingerPos = StageID % 5;
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    EndPosition.x = EvenNumber + ((FingerPos) * Width);
<<<<<<< HEAD
                    FramePosition = new Vector3(-4.24f + ((FingerPos) * 2.33f), 0.04f + (-Decision * 1.589f), -10);
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Ymoved = true;
                    FingerMoveFlag = true;
                    Sound.PlaySe("MapSelect", 3);
=======
                    FramePosition = new Vector3(3.3f + ((FingerPos) * -1.65f), 0.11f, -3.35f + (Decision * 0.91f));
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Ymoved = true;
                    FingerMoveFlag = true;
>>>>>>> Dev

                }
            }
            if (StageID > MaxStage - 5 && MoveFlag)
            {
                if (YStickFlag == true && Ymoved == false && YDecision < -0.5f || Input.GetKeyDown(KeyCode.DownArrow))
                {

                    MoveFlag = false;
                    StageID = StageID - 15;
                    Decision = StageID / 5;
                    FingerPos = StageID % 5;
                    StartPosition = Finger.transform.position;
                    EndPosition = StartPosition;
                    EndPosition.x = EvenNumber + ((FingerPos) * Width);
<<<<<<< HEAD
                    FramePosition = new Vector3(-4.24f + ((FingerPos) * 2.33f), 0.04f + (-Decision * 1.589f), -10);
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Ymoved = true;
                    FingerMoveFlag = true;
                    Sound.PlaySe("MapSelect", 3);
=======
                    FramePosition = new Vector3(3.3f + ((FingerPos) * -1.65f), 0.11f, -3.35f + (Decision * 0.91f));
                    EndPosition.y = InitHeight + (-Decision * Height);
                    Ymoved = true;
                    FingerMoveFlag = true;
>>>>>>> Dev

                }
            }



        }
        MoveFlag = true;
<<<<<<< HEAD
        if (Input.GetButtonDown("AButton"))
=======
        if (Controller.GetButtonDown("AButton"))
>>>>>>> Dev
        {
            Sound.PlaySe("MapSelect", 3);

            SelectFlag = true;
        }

<<<<<<< HEAD
        if (Input.GetButtonDown("SelectButton"))
=======
        if (Controller.GetButtonDown("SelectButton"))
>>>>>>> Dev
        {
            Sound.PlaySe("MapIn", 4);
            SelectButtonFlag = true;

        }

    }

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
                    //  Sound.PlaySe("Move", 0);
                    Frame.SetActive(false);
                    FadeFlag.FadeOutFlag = true;
                    FadeInit = true;
                }
                if (!FadeFlag.FadeOutFlag)
                {
                    spotlight.SetActive(false);



                    if (!FadeInInit)
                    {
                        //Sound.StopSe("Move", 0);
                        StageID += 1;
                        StageSelectObject.transform.position = new Vector3(-14.0f * (StageID), 4, 0);
                        FadeInInit = true;
                        FadeFlag.FadeInFlag = true;
                    }
                    if (!FadeFlag.FadeInFlag)
                    {
                        rate += 0.05f;
                        if (!StagePositionFlag)
                        {
<<<<<<< HEAD
=======
                            this.transform.rotation = Quaternion.Euler(101f, 270f, 90f);
>>>>>>> Dev
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
<<<<<<< HEAD
                                EndPosition = new Vector3(-13.9f, -15.7f, -2.7f);
=======
                                EndPosition = new Vector3(-8.1f, -12.2f, -2.9f);
>>>>>>> Dev
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
<<<<<<< HEAD
                            if (PositionFlag)
                            {
                                this.transform.rotation = Quaternion.Euler(80.5f, 136f, -41f);
                                RotationFlag = true;
=======
                            /* if (PositionFlag)
                             {
                                 this.transform.rotation = Quaternion.Euler(101f, 270f, 90f);
                                 RotationFlag = true;
                             }*/
                            if (!RotationFlag)
                            {
                                this.transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(114.464f, 360, 180), new Vector3(101f, 270f, 90f), rate2));

                                if (this.transform.rotation.x == 101f)
                                {
                                    RotationFlag = true;
                                }
>>>>>>> Dev
                            }

                        }



<<<<<<< HEAD
                        if (PositionFlag && RotationFlag && StagePositionFlag && CameraPositionFlag)
=======
                        if (PositionFlag && StagePositionFlag && CameraPositionFlag)
>>>>>>> Dev
                        {
                            rate = 0;
                            rate2 = 0;
                            FadeInInit = false;
                            InitFlag = true;
                            FadeInit = false;
                            SelectFlag = false;

                            PassStageID.GetStageID(StageID);
                            FingerFlag = false;
                            StagePositionFlag = false;
                            PositionFlag = false;
                            RotationFlag = false;
                            Is_Map = false;
                            CameraPositionFlag = false;
                            StageEnable.enabled = true;
                            this.enabled = false;
                        }
                    }
                }
            }

        }

        if (SelectButtonFlag)
        {
<<<<<<< HEAD
=======
            EndRotation = new Vector3(101f, 270f, 90f);
            EndPosition = new Vector3(-8.1f, -12.2f, -2.9f);
>>>>>>> Dev
            spotlight.SetActive(false);
            Frame.SetActive(false);
            rate += 0.05f;
            if (!StagePositionFlag)
            {
                StageSelectObject.transform.position = Vector3.Lerp(new Vector3(-14.0f * (PassStageID.PassStageId()), 4, 0), new Vector3(-14.0f * (PassStageID.PassStageId()), 0, 0), rate);
                if (StageSelectObject.transform.position == new Vector3(-14.0f * (PassStageID.PassStageId()), 0, 0))
                {
<<<<<<< HEAD
=======
                                                this.transform.rotation = Quaternion.Euler(101f, 270f, 90f);

>>>>>>> Dev
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
<<<<<<< HEAD
                    StartPosition = this.transform.position;
                    EndPosition = new Vector3(-13.9f, -15.7f, -2.7f);
                }
            }

            if (CameraPositionFlag && StagePositionFlag)
            {
=======
                  
                    
                }
            }

            //if (CameraPositionFlag && StagePositionFlag)
            //{
>>>>>>> Dev
                rate2 += 0.05f;
                if (!PositionFlag)
                {
                    this.transform.position = Vector3.Lerp(StartPosition, EndPosition, rate2);
                    if (this.transform.position == EndPosition)
                    {
                        PositionFlag = true;
                    }
                }

<<<<<<< HEAD
                Finger.transform.position = new Vector3(11, -10, 0);
                if (PositionFlag)
                {
                    this.transform.rotation = Quaternion.Euler(80.5f, 136f, -41f);
                    RotationFlag = true;
                }

            }
=======
                if (!RotationFlag)
                {
                    this.transform.rotation = Quaternion.Euler(Vector3.Lerp(StartRotation, EndRotation, rate2));
                    NowRotation = Vector3.Lerp(StartRotation, EndRotation, rate2);
                    if (NowRotation == EndRotation)
                    {
                        RotationFlag = true;
                    }
                }
            //}
>>>>>>> Dev



            if (PositionFlag && RotationFlag && StagePositionFlag && CameraPositionFlag)
            {
                rate = 0;
                rate2 = 0;
                FadeInInit = false;
                InitFlag = true;
                FadeInit = false;
                SelectButtonFlag = false;

                FingerFlag = false;
                StagePositionFlag = false;
                PositionFlag = false;
                RotationFlag = false;
                Is_Map = false;
                CameraPositionFlag = false;
                StageEnable.enabled = true;
                this.enabled = false;
            }
        }
    }
}