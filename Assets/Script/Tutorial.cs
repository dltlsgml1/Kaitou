using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public static int CircleMax = 10;
    public int Limit = 5;
    public enum CircleIndex {RightStick=0,LeftStick,BButton,SlowButton,FastButton,ResetButton,PauseButton};
    public GameObject MainScript;
    public Text text;
    public Text textleft;
    public Text textright;
    public GameObject ExplainBlock;
    public GameObject CollapsBlock;
    public GameObject NormalBlock;
    public GameObject ControlBlock1;
    public GameObject ControlBlock2;
    public GameObject ControlBlock3;
    public GameObject LifeStar;

    public GameObject[] Circles = new GameObject[CircleMax];
    public Sprite[] TutorialSprite = new Sprite[30];
    public SpriteRenderer TutorialRenderer;

    public int TutorialIndex = 0;
    public int ExplainIndex = 0;
    public int ControlIndex = 0;

    public bool isChangedTutorialText = false;
    public bool isNext = false;
    public FadeImage fadeImage;
    public float fadeTime = 0.2f;

    public bool ResetCamera = false;

    //TutorialText.GetComponent<MeshRenderer>().materials[0].mainTexture = 
    void Start()
    {
           
        MainScript.GetComponent<GameMain>().TutorialFlg = true;

        fadeImage = this.GetComponent<FadeImage>();
        fadeImage.SetSpriteRenderer(TutorialRenderer);

        for (int i=0;i<30;i++)
        {
            TutorialSprite[i] = Resources.Load<Sprite>("Tutorial/Explain/tutorial" + (i+1));

        }
        
    }
    
    void Update()
    {
        if (Input.GetButtonDown("StartButton")||Input.GetKeyDown(KeyCode.Space)) 
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

    }
    void Explain()
    {
        switch (ExplainIndex)
        {
            case 0:
                MainScript.GetComponent<GameMain>().TutorialAtari = true;
                //TutorialRenderer.sprite = TutorialSprite[0];
                if(!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[0]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }
                NormalBlock.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f));
                NormalBlock.GetComponent<Blocks>().BurnFlg = false;
                NormalBlock.GetComponent<Blocks>().canburn = false;

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("RButton"))
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }  
                }
                break;
            case 1:
                //TutorialRenderer.sprite = TutorialSprite[1];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[1]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
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
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ExplainIndex--;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 2:
                //TutorialRenderer.sprite = TutorialSprite[2];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[2]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }
                for (int i = 0; i < Circles.Length; i++)
                {
                    if(Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Circles[(int)CircleIndex.LeftStick].activeSelf == false)
                    Circles[(int)CircleIndex.LeftStick].SetActive(true);

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ExplainIndex--;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 3:
                //TutorialRenderer.sprite = TutorialSprite[3];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[3]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }

                text.fontSize = 70;
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Circles[(int)CircleIndex.RightStick].activeSelf == false)
                    Circles[(int)CircleIndex.RightStick].SetActive(true);

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ExplainIndex--;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 4:
                //TutorialRenderer.sprite = TutorialSprite[4];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[4]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }

                text.fontSize = 60;
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Circles[(int)CircleIndex.SlowButton].activeSelf == false)
                    Circles[(int)CircleIndex.SlowButton].SetActive(true);
                if (Circles[(int)CircleIndex.FastButton].activeSelf == false)
                    Circles[(int)CircleIndex.FastButton].SetActive(true);

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ExplainIndex--;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 5:
                //TutorialRenderer.sprite = TutorialSprite[5];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[5]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }

                text.fontSize = 70;
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Circles[(int)CircleIndex.PauseButton].activeSelf == false)
                    Circles[(int)CircleIndex.PauseButton].SetActive(true);

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ExplainIndex--;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 6:
                //TutorialRenderer.sprite = TutorialSprite[6];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[6]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }

                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Circles[(int)CircleIndex.ResetButton].activeSelf == false)
                    Circles[(int)CircleIndex.ResetButton].SetActive(true);

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ExplainIndex--;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 7:
                MainScript.GetComponent<GameMain>().TutorialAtari = false;

                //TutorialRenderer.sprite = TutorialSprite[7];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[7]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
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
                if (Circles[(int)CircleIndex.RightStick].activeSelf == false)
                    Circles[(int)CircleIndex.RightStick].SetActive(true);

                if(NormalBlock.GetComponent<Blocks>().NormalNowcol==true)
                {
                    MainScript.GetComponent<GameMain>().mvcamera.StopCameraOn();
                    for (int i = 0; i < Circles.Length; i++)
                    {
                        if (Circles[i] != null)
                        {
                            if (Circles[i].activeSelf == true)
                                Circles[i].SetActive(false);
                        }
                    }
                    TutorialRenderer.sprite = TutorialSprite[8];
                    if (Circles[(int)CircleIndex.BButton].activeSelf == false)
                        Circles[(int)CircleIndex.BButton].SetActive(true);
                }

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (NormalBlock.GetComponent<Blocks>().BurnFlg == true)
                    {
                        ExplainIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 8:
                //TutorialRenderer.sprite = TutorialSprite[9];
                MainScript.GetComponent<GameMain>().mvcamera.StopCameraOff();
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[9]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
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
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ExplainIndex = 0;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                    if (NormalBlock.GetComponent<Blocks>().BurnFlg == true && Input.GetButtonDown("RButton"))
                    {
                        TutorialIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
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
                //TutorialRenderer.sprite = TutorialSprite[10];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[10]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }

                MainScript.GetComponent<GameMain>().SetBlock();
                if (ExplainBlock.gameObject.activeSelf == true)
                    ExplainBlock.gameObject.SetActive(false);
                if (ControlBlock1.gameObject.activeSelf == false)
                    ControlBlock1.gameObject.SetActive(true);

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 1:
                //TutorialRenderer.sprite = TutorialSprite[11];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[11]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ControlIndex--;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 2:
                //TutorialRenderer.sprite = TutorialSprite[12];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[12]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut() && !isNext)
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ControlIndex--;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                if (MainScript.GetComponent<GameMain>().NormalCount == 0)       // todo 判定つくってやる
                {
                    if (!isNext)
                    {
                        isChangedTutorialText = false;
                        isNext = true;
                    }

                    //TutorialRenderer.sprite = TutorialSprite[13];
                    if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                    {
                        fadeImage.SetSprite(TutorialSprite[13]);
                        GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                        isChangedTutorialText = true;
                    }

                    // フェード中かどうか
                    if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                    {
                        if (Input.GetButtonDown("RButton"))
                        {
                            ControlIndex++;
                            GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                            isChangedTutorialText = false;
                            isNext = false;
                        }
                    }

                }

                break;
            case 3:
                //TutorialRenderer.sprite = TutorialSprite[14];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[14]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
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
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 4:
                //TutorialRenderer.sprite = TutorialSprite[15];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[15]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ControlIndex--;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 5:
                //TutorialRenderer.sprite = TutorialSprite[16];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[16]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut() && !isNext)
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ControlIndex--;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                if (MainScript.GetComponent<GameMain>().NormalCount == 0)
                {
                    if (!isNext)
                    {
                        isChangedTutorialText = false;
                        isNext = true;
                    }

                    //TutorialRenderer.sprite = TutorialSprite[17];
                    if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                    {
                        fadeImage.SetSprite(TutorialSprite[17]);
                        GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                        isChangedTutorialText = true;
                    }

                    // フェード中かどうか
                    if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                    {
                        if (Input.GetButtonDown("RButton"))
                        {
                            ControlIndex++;
                            GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                            isChangedTutorialText = false;
                            isNext = false;
                        }
                    }
                }
                break;
            case 6:
                //TutorialRenderer.sprite = TutorialSprite[18];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[18]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                    MainScript.GetComponent<GameMain>().mvcamera.Rotation = new Vector3(0.0f, 0.0f, 0.0f);
                }

                if (LifeStar.activeSelf == true)
                    LifeStar.SetActive(false);
                if (ControlBlock2.activeSelf == true)
                    ControlBlock2.SetActive(false);
                if (ControlBlock3.activeSelf == false)
                    ControlBlock3.SetActive(true);

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 7:
                //TutorialRenderer.sprite = TutorialSprite[19];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[19]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ControlIndex--;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                    if (Input.GetButtonDown("RButton"))
                    {
                        ControlIndex++;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }

                break;
            case 8:
                //TutorialRenderer.sprite = TutorialSprite[20];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[20]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut() && !isNext)
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        ControlIndex--;
                        GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                        isChangedTutorialText = false;
                    }
                }


                if (MainScript.GetComponent<GameMain>().NormalCount == 0)
                {
                    if (!isNext)
                    {
                        isChangedTutorialText = false;
                        isNext = true;
                    }

                    //TutorialRenderer.sprite = TutorialSprite[21];
                    if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                    {
                        fadeImage.SetSprite(TutorialSprite[21]);
                        GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                        isChangedTutorialText = true;
                    }

                    // フェード中かどうか
                    if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                    {
                        if (Input.GetButtonDown("RButton"))
                        {
                            ControlIndex++;
                            GlobalCoroutine.Go(fadeImage.SpriteFadeOut(fadeTime));
                            isChangedTutorialText = false;
                            isNext = false;
                        }
                    }
                }

                break;
            case 9:
                //TutorialRenderer.sprite = TutorialSprite[22];
                if (!isChangedTutorialText && !fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    fadeImage.SetSprite(TutorialSprite[22]);
                    GlobalCoroutine.Go(fadeImage.SpriteFadeIn(fadeTime));
                    isChangedTutorialText = true;
                }

                if (ControlBlock3.activeSelf == true)
                    ControlBlock3.SetActive(false);

                // フェード中かどうか
                if (!fadeImage.GetIsFadingIn() && !fadeImage.GetIsFadingOut())
                {
                    if (Input.GetButtonDown("LButton"))
                    {
                        isChangedTutorialText = false;
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
    //제한횟수
    //면단위 제한
    private void OnDestroy()
    {
        MainScript.GetComponent<GameMain>().TutorialFlg = false;
    }
}
