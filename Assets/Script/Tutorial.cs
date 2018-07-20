using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    enum TUTORIAL_SPRITE
    {
        TEXT_INDEX = 21,
        num_values
    }

    public static int CircleMax = 4;
    public int Limit = 5;
    public enum CircleIndex { LeftStick = 0, AButton, SlowButton, FastButton };
    public GameObject MainScript;
    public GameObject ExplainBlock;
    public GameObject CollapsBlock;
    public GameObject NormalBlock;
    public GameObject ControlBlock1;
    public GameObject ControlBlock2;
    public GameObject ControlBlock3;
    public GameObject LifeStar;

    public GameObject[] Circles = new GameObject[CircleMax];
    public Sprite[] TutorialSprite = new Sprite[(int)TUTORIAL_SPRITE.num_values];
    public SpriteRenderer TutorialRenderer;

    public int TutorialIndex = 0;
    public int ExplainIndex = 0;
    public int ControlIndex = 0;

    public bool isChangedTutorialText = false;
    public bool isNext = false;
    public FadeImage TutorialText;
    public float FadeTime = 0.2f;

    public bool ResetCamera = false;

    public SpriteRenderer TextBackRenderer;
    public SpriteRenderer TextNextRenderer;

    public FadeImage TextBackFade;
    public FadeImage TextNextFade;

    public bool TextBackFlg = false;
    public bool TextNextFlg = false;
    public bool isFadeStartTextBack = false;
    public bool isFadeStartTextNext = false;

    public bool FadeControllerFlg = false;
    public FadeImage FadeController;
    public SpriteRenderer ControllerRenderer;

    public bool FadeControllerInfoFlg = false;
    public FadeImage FadeControllerInfo;
    public SpriteRenderer ControllerInfoRenderer;

    public bool FadeControllerInfoFrameFlg = false;
    public FadeImage FadeControllerInfoFrame;
    public SpriteRenderer ControllerInfoFrameRenderer;


    //MainScript.GetComponent<GameMain>().TutorialAtari = true;     // 判定きかない 判定OFF
    //MainScript.GetComponent<GameMain>().TutorialAtari = false;    // 判定きかせる 判定ON
    //MainScript.GetComponent<GameMain>().mvcamera.StopCameraOn();  // カメラ操作とめる カメラ操作OFF
    //MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff(); // カメラ操作うごかす カメラ操作ON



    //TutorialText.GetComponent<MeshRenderer>().materials[0].mainTexture = 
    void Start()
    {

        MainScript.GetComponent<GameMain>().TutorialFlg = true;

        Init();

    }

    void Init()
    {
        // 各変数初期化
        Limit = 5;

        TutorialIndex = 0;
        ExplainIndex = 0;
        ControlIndex = 0;

        isChangedTutorialText = false;
        isNext = false;

        FadeTime = 0.2f;

        ResetCamera = false;
        TextBackFlg = false;
        TextNextFlg = false;
        isFadeStartTextBack = false;
        isFadeStartTextNext = false;

        FadeControllerFlg = false;
        FadeControllerInfoFlg = false;
        FadeControllerInfoFrameFlg = false;

        // スプライト初期化
        InitSprite();
    }

    void InitSprite()
    {
        TutorialText = this.GetComponent<FadeImage>();
        TutorialText.SetSpriteRenderer(TutorialRenderer);

        TextBackFade = GameObject.Find("TutorialBack").GetComponent<FadeImage>();
        TextBackRenderer = GameObject.Find("TutorialBack").GetComponent<SpriteRenderer>();
        TextBackFade.SetSpriteRenderer(TextBackRenderer);
        TextBackRenderer.color = new Color(1, 1, 1, 0);

        TextNextFade = GameObject.Find("TutorialNext").GetComponent<FadeImage>();
        TextNextRenderer = GameObject.Find("TutorialNext").GetComponent<SpriteRenderer>();
        TextNextFade.SetSpriteRenderer(TextNextRenderer);
        TextNextRenderer.color = new Color(1, 1, 1, 0);

        FadeController = GameObject.Find("Controller").GetComponent<FadeImage>();
        ControllerRenderer = GameObject.Find("Controller").GetComponent<SpriteRenderer>();
        FadeController.SetSpriteRenderer(ControllerRenderer);
        ControllerRenderer.color = new Color(1, 1, 1, 0);

        FadeControllerInfo = GameObject.Find("Controller_info_Image").GetComponent<FadeImage>();
        ControllerInfoRenderer = GameObject.Find("Controller_info_Image").GetComponent<SpriteRenderer>();
        FadeControllerInfo.SetSpriteRenderer(ControllerInfoRenderer);
        ControllerInfoRenderer.color = new Color(1, 1, 1, 0);

        FadeControllerInfoFrame = GameObject.Find("Controller_info_Frame").GetComponent<FadeImage>();
        ControllerInfoFrameRenderer = GameObject.Find("Controller_info_Frame").GetComponent<SpriteRenderer>();
        FadeControllerInfoFrame.SetSpriteRenderer(ControllerInfoFrameRenderer);
        ControllerInfoFrameRenderer.color = new Color(1, 1, 1, 0);


        // チュートリアルテキスト読み込み
        for (int i = 0; i < (int)TUTORIAL_SPRITE.TEXT_INDEX; i++)
        {
            TutorialSprite[i] = Resources.Load<Sprite>("Tutorial/Explain/tutorial" + (i + 1));
        }

    }

    void Update()
    {
        if (Input.GetButtonDown("StartButton") || Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);

        switch (TutorialIndex)
        {
            case 0:
                Explain();
                break;
            case 1:
                Control();
                break;
            case 2:
                break;
        }

        FadeBackText();
        FadeNextText();

    }


    void FadeBackText()
    {
        // フェード中じゃなかったら
        if (!TextBackFade.GetIsFadingIn() && !TextBackFade.GetIsFadingOut() && isFadeStartTextBack)
        {
            if (TextBackFlg)
            {
                isFadeStartTextBack = false;
                //GlobalCoroutine.Go(TextBackFade.SpriteFadeIn(FadeTime));
                GlobalCoroutine.Go(TextBackFade.SpriteFadeIn(FadeTime), "FadeBackText");
            }
            else
            {
                isFadeStartTextBack = false;
                //GlobalCoroutine.Go(TextBackFade.SpriteFadeOut(FadeTime));
                GlobalCoroutine.Go(TextBackFade.SpriteFadeOut(FadeTime), "FadeBackText");
            }
        }
    }

    void FadeNextText()
    {
        // フェード中じゃなかったら
        if (!TextNextFade.GetIsFadingIn() && !TextNextFade.GetIsFadingOut() && isFadeStartTextNext)
        {
            if (TextNextFlg)
            {
                isFadeStartTextNext = false;
                //GlobalCoroutine.Go(TextNextFade.SpriteFadeIn(FadeTime));
                GlobalCoroutine.Go(TextNextFade.SpriteFadeIn(FadeTime), "FadeNextText");
            }
            else
            {
                isFadeStartTextNext = false;
                //GlobalCoroutine.Go(TextNextFade.SpriteFadeOut(FadeTime));
                GlobalCoroutine.Go(TextNextFade.SpriteFadeOut(FadeTime), "FadeNextText");
            }
        }
    }

    void SetBackTextFlg(bool flg)
    {
        if (flg)
        {
            TextBackFlg = true;
            isFadeStartTextBack = true;
        }
        else
        {
            TextBackFlg = false;
            isFadeStartTextBack = true;
        }
    }
    void SetNextTextFlg(bool flg)
    {
        if (flg)
        {
            TextNextFlg = true;
            isFadeStartTextNext = true;
        }
        else
        {
            TextNextFlg = false;
            isFadeStartTextNext = true;
        }
    }

    void SetInfoFlg(bool back, bool next)
    {
        SetBackTextFlg(back);
        SetNextTextFlg(next);
    }

    void Explain()
    {
        switch (ExplainIndex)
        {
            case 0:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!FadeControllerFlg)
                {
                    FadeControllerFlg = true;
                    GlobalCoroutine.Go(FadeController.SpriteFadeIn(FadeTime));
                }

                if (!ResetCamera)
                {
                    MainScript.GetComponent<GameMain>().mvcamera.Rotation = new Vector3(15.0f, -25.0f, 0.0f);
                    ResetCamera = true;
                }

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[0]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetNextTextFlg(true);
                }
                NormalBlock.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f));
                NormalBlock.GetComponent<Blocks>().BurnFlg = false;
                NormalBlock.GetComponent<Blocks>().BurnOK = false;

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("RButton"))
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetNextTextFlg(false);
                    }
                }
                break;
            case 1:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[1]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetInfoFlg(true, true);
                }
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Circles[(int)CircleIndex.LeftStick].activeSelf == false)
                    Circles[(int)CircleIndex.LeftStick].SetActive(true);

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ExplainIndex--;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetInfoFlg(false, false);
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetInfoFlg(false, false);
                    }
                }

                break;
            case 2:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[2]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetInfoFlg(true, true);
                }
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Circles[(int)CircleIndex.FastButton].activeSelf == false)
                    Circles[(int)CircleIndex.FastButton].SetActive(true);
                if (Circles[(int)CircleIndex.SlowButton].activeSelf == false)
                    Circles[(int)CircleIndex.SlowButton].SetActive(true);

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ExplainIndex--;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetInfoFlg(false, false);
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetInfoFlg(false, false);
                    }
                }

                break;

            case 3:
                MainScript.GetComponent<GameMain>().TutorialAtari = false;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut() && !isNext)
                {
                    TutorialText.SetSprite(TutorialSprite[3]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetBackTextFlg(true);
                }

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut() && NormalBlock.GetComponent<Blocks>().NormalNowcol == false && !isNext)
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ExplainIndex--;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetBackTextFlg(false);
                    }
                }


                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Circles[(int)CircleIndex.LeftStick].activeSelf == false)
                    Circles[(int)CircleIndex.LeftStick].SetActive(true);

                if (NormalBlock.GetComponent<Blocks>().NormalNowcol == true)
                {
                    // todo: 要調整
                    if (isChangedTutorialText && !isNext)
                    {
                        SetBackTextFlg(false);
                        isChangedTutorialText = false;
                        isNext = true;
                    }

                    MainScript.GetComponent<GameMain>().mvcamera.StopCameraOn();
                    for (int i = 0; i < Circles.Length; i++)
                    {
                        if (Circles[i] != null)
                        {
                            if (Circles[i].activeSelf == true)
                                Circles[i].SetActive(false);
                        }
                    }
                    // todo: フェード入れる
                    if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut() && isNext)
                    {
                        TutorialText.SetSprite(TutorialSprite[4]);
                        GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                        isChangedTutorialText = true;
                    }

                    if (Circles[(int)CircleIndex.AButton].activeSelf == false)
                        Circles[(int)CircleIndex.AButton].SetActive(true);
                }

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (NormalBlock.GetComponent<Blocks>().BurnFlg == true)
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        isNext = false;
                    }
                }

                break;
            case 4:
                MainScript.GetComponent<GameMain>().TutorialAtari = false;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[5]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetNextTextFlg(true);
                }

                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (NormalBlock.GetComponent<Blocks>().BurnFlg == true && Input.GetButtonDown("RButton"))
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetNextTextFlg(false);
                        if (FadeControllerFlg)
                        {
                            FadeControllerFlg = false;
                            GlobalCoroutine.Go(FadeController.SpriteFadeOut(FadeTime));
                        }
                    }
                }

                break;

            case 5:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!FadeControllerInfoFlg && !FadeControllerInfoFrameFlg)
                {
                    FadeControllerInfoFlg = true;
                    FadeControllerInfoFrameFlg = true;
                    GlobalCoroutine.Go(FadeControllerInfo.SpriteFadeIn(FadeTime));
                    GlobalCoroutine.Go(FadeControllerInfoFrame.SpriteFadeIn(FadeTime));
                }

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[6]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetNextTextFlg(true);
                }
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("RButton"))
                    {
                        TutorialIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetNextTextFlg(false);
                        ResetCamera = false;

                        if (FadeControllerInfoFlg && FadeControllerInfoFrameFlg)
                        {
                            FadeControllerInfoFlg = false;
                            FadeControllerInfoFrameFlg = false;
                            GlobalCoroutine.Go(FadeControllerInfo.SpriteFadeOut(FadeTime));
                            GlobalCoroutine.Go(FadeControllerInfoFrame.SpriteFadeOut(FadeTime));
                        }

                    }
                }

                break;


        }

    }

    void Control()
    {
        MainScript.GetComponent<GameMain>().SetBlock();
        switch (ControlIndex)
        {
            case 0:
                MainScript.GetComponent<GameMain>().TutorialAtari = false;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut() && !isNext)
                {
                    TutorialText.SetSprite(TutorialSprite[7]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    isNext = true;
                }

                if (!ResetCamera)
                {
                    MainScript.GetComponent<GameMain>().mvcamera.Rotation = new Vector3(20.0f, 40.0f, 0.0f);
                    ResetCamera = true;
                }


                MainScript.GetComponent<GameMain>().SetBlock();
                if (ExplainBlock.gameObject.activeSelf == true)
                    ExplainBlock.gameObject.SetActive(false);
                if (ControlBlock1.gameObject.activeSelf == false)
                    ControlBlock1.gameObject.SetActive(true);

                if (MainScript.GetComponent<GameMain>().NormalCount == 0)       // todo 判定つくってやる
                {
                    if (isNext)
                    {
                        isChangedTutorialText = false;
                        isNext = false;
                    }

                    if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut() && !isNext)
                    {
                        TutorialText.SetSprite(TutorialSprite[8]);
                        GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                        isChangedTutorialText = true;
                        isNext = false;
                        SetNextTextFlg(true);
                    }

                    // フェード中かどうか
                    if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut() && !isNext)
                    {
                        if (Input.GetButtonDown("RButton"))
                        {
                            ControlIndex++;
                            GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                            isChangedTutorialText = false;
                            SetNextTextFlg(false);
                        }
                    }

                }

                break;

            case 1:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[9]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetNextTextFlg(true);
                }

                if (ControlBlock1.gameObject.activeSelf == true)
                    ControlBlock1.gameObject.SetActive(false);
                if (ControlBlock2.gameObject.activeSelf == false)
                    ControlBlock2.gameObject.SetActive(true);
                if (LifeStar.gameObject.activeSelf == false)
                {
                    LifeStar.gameObject.SetActive(true);
                    MainScript.GetComponent<GameMain>().Limit = 5;
                }

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetNextTextFlg(false);
                    }
                }

                break;

            case 2:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[10]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetInfoFlg(true, true);
                }

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ControlIndex--;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetInfoFlg(false, false);
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetInfoFlg(false, false);
                    }
                }


                break;
            case 3:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[11]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetInfoFlg(true, true);
                }

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ControlIndex--;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetInfoFlg(false, false);
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetInfoFlg(false, false);
                    }
                }


                break;

            case 4:
                MainScript.GetComponent<GameMain>().TutorialAtari = false;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[12]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetBackTextFlg(true);
                }

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut() && !isNext)
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ControlIndex--;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetBackTextFlg(false);
                    }
                }

                if (MainScript.GetComponent<GameMain>().NormalCount == 0)
                {
                    if (!isNext)
                    {
                        isChangedTutorialText = false;
                        isNext = true;
                        SetBackTextFlg(false);
                    }

                    if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                    {
                        TutorialText.SetSprite(TutorialSprite[13]);
                        GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                        isChangedTutorialText = true;
                        SetNextTextFlg(true);
                    }

                    // フェード中かどうか
                    if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                    {
                        if (Input.GetButtonDown("RButton"))
                        {
                            ControlIndex++;
                            GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                            isChangedTutorialText = false;
                            isNext = false;
                            SetNextTextFlg(false);
                        }
                    }
                }
                break;
            case 5:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[14]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetNextTextFlg(true);
                }

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetNextTextFlg(false);
                        ResetCamera = false;
                    }
                }

                break;
            case 6:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!ResetCamera)
                {
                    MainScript.GetComponent<GameMain>().mvcamera.Rotation = new Vector3(30.0f, 115.0f, 0.0f);
                    ResetCamera = true;
                }

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[15]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetNextTextFlg(true);
                }

                if (LifeStar.activeSelf == true)
                    LifeStar.SetActive(false);
                if (ControlBlock2.activeSelf == true)
                    ControlBlock2.SetActive(false);
                if (ControlBlock3.activeSelf == false)
                    ControlBlock3.SetActive(true);

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetNextTextFlg(false);
                    }
                }

                break;
            case 7:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[16]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetInfoFlg(true, true);
                }

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ControlIndex--;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetInfoFlg(false, false);
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetInfoFlg(false, false);
                    }
                }

                break;

            case 8:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[17]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetInfoFlg(true, true);
                }

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ControlIndex--;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetInfoFlg(false, false);
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetInfoFlg(false, false);
                    }
                }

                break;

            case 9:
                MainScript.GetComponent<GameMain>().TutorialAtari = false;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[18]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetBackTextFlg(true);
                }

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut() && !isNext)
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ControlIndex--;
                        GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                        isChangedTutorialText = false;
                        SetBackTextFlg(false);
                    }
                }


                if (MainScript.GetComponent<GameMain>().NormalCount == 0)
                {
                    if (!isNext)
                    {
                        isChangedTutorialText = false;
                        isNext = true;
                        SetBackTextFlg(false);
                    }

                    if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                    {
                        TutorialText.SetSprite(TutorialSprite[19]);
                        GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                        isChangedTutorialText = true;
                        SetNextTextFlg(true);
                    }

                    // フェード中かどうか
                    if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                    {
                        if (Input.GetButtonDown("RButton"))
                        {
                            ControlIndex++;
                            GlobalCoroutine.Go(TutorialText.SpriteFadeOut(FadeTime));
                            isChangedTutorialText = false;
                            isNext = false;
                            SetNextTextFlg(false);
                        }
                    }
                }

                break;
            case 10:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();

                if (!isChangedTutorialText && !TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    TutorialText.SetSprite(TutorialSprite[20]);
                    GlobalCoroutine.Go(TutorialText.SpriteFadeIn(FadeTime));
                    isChangedTutorialText = true;
                    SetNextTextFlg(true);
                }

                if (ControlBlock3.activeSelf == true)
                    ControlBlock3.SetActive(false);

                // フェード中かどうか
                if (!TutorialText.GetIsFadingIn() && !TutorialText.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        isChangedTutorialText = false;
                        ResetCamera = false;
                        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        isChangedTutorialText = false;
                        SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
                    }
                }

                break;
        }

    }


    private void OnDestroy()
    {
        MainScript.GetComponent<GameMain>().TutorialFlg = false;
    }
}
