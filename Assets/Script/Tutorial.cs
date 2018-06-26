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
    //TutorialText.GetComponent<MeshRenderer>().materials[0].mainTexture = 
    void Start()
    {
        
        MainScript.GetComponent<GameMain>().TutorialFlg = true;
        for(int i=0;i<30;i++)
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
                TutorialRenderer.sprite = TutorialSprite[0];
                NormalBlock.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f));
                NormalBlock.GetComponent<Blocks>().BurnFlg = false;
                NormalBlock.GetComponent<Blocks>().canburn = false;
                if(Input.GetButtonDown("RButton"))  
                    ExplainIndex++;
                break;
            case 1:
                TutorialRenderer.sprite = TutorialSprite[1];
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Input.GetButtonDown("LButton"))   
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))   
                    ExplainIndex++;
                break;
            case 2:
                TutorialRenderer.sprite = TutorialSprite[2];
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
                if (Input.GetButtonDown("LButton"))  
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))   
                    ExplainIndex++;
                break;
            case 3:
                TutorialRenderer.sprite = TutorialSprite[3];
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
                if (Input.GetButtonDown("LButton"))       
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))      
                    ExplainIndex++;
                break;
            case 4:
                TutorialRenderer.sprite = TutorialSprite[4];
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
                if (Input.GetButtonDown("LButton"))       
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))       
                    ExplainIndex++;
                break;
            case 5:
                TutorialRenderer.sprite = TutorialSprite[5];
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
                if (Input.GetButtonDown("LButton"))      
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))      
                    ExplainIndex++;
                break;
            case 6:
                TutorialRenderer.sprite = TutorialSprite[6];
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
                if (Input.GetButtonDown("LButton"))       
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))      
                    ExplainIndex++;
                break;
            case 7:
                TutorialRenderer.sprite = TutorialSprite[7];
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

                if (Input.GetButtonDown("LButton"))         
                    ExplainIndex--;

                if(NormalBlock.GetComponent<Blocks>().NormalNowcol==true)
                {
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
                if (NormalBlock.GetComponent<Blocks>().BurnFlg == true)
                    ExplainIndex++;
                break;
            case 8:
                TutorialRenderer.sprite = TutorialSprite[9];
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Input.GetButtonDown("LButton"))         
                    ExplainIndex = 0;
                if (Input.GetButtonDown("RButton"))          
                    TutorialIndex++;
                break;
              }
        
    }
    void Control()
    {
        MainScript.GetComponent<GameMain>().SetBlock();
        switch (ControlIndex)
        {
            case 0:
                MainScript.GetComponent<GameMain>().SetBlock();
                if (ExplainBlock.gameObject.activeSelf == true)
                    ExplainBlock.gameObject.SetActive(false);
                if (ControlBlock1.gameObject.activeSelf == false)
                    ControlBlock1.gameObject.SetActive(true);
                TutorialRenderer.sprite = TutorialSprite[10];
                if (Input.GetButtonDown("RButton"))
                    ControlIndex++;
                break;
            case 1:
                TutorialRenderer.sprite = TutorialSprite[11];
                if (Input.GetButtonDown("LButton"))
                    ControlIndex--;
                if (Input.GetButtonDown("RButton"))
                    ControlIndex++;
                break;
            case 2:
                TutorialRenderer.sprite = TutorialSprite[12];
                if (MainScript.GetComponent<GameMain>().NormalCount == 0)
                {
                    TutorialRenderer.sprite = TutorialSprite[13];
                    if (Input.GetButtonDown("RButton"))
                        ControlIndex++;
                }
                if (Input.GetButtonDown("LButton"))
                    ControlIndex--;
                break;
            case 3:
                if (ControlBlock1.gameObject.activeSelf == true)
                    ControlBlock1.gameObject.SetActive(false);
                if (ControlBlock2.gameObject.activeSelf == false)
                    ControlBlock2.gameObject.SetActive(true);
                if (LifeStar.gameObject.activeSelf == false)
                {
                    LifeStar.gameObject.SetActive(true);
                    MainScript.GetComponent<GameMain>().Limit = 5;
                }

                TutorialRenderer.sprite = TutorialSprite[14];
                if (Input.GetButtonDown("RButton"))
                    ControlIndex++;
                break;
            case 4:
                TutorialRenderer.sprite = TutorialSprite[15];
                if (Input.GetButtonDown("LButton"))
                    ControlIndex--;
                if (Input.GetButtonDown("RButton"))
                    ControlIndex++;
                break;
            case 5:
                TutorialRenderer.sprite = TutorialSprite[16];
                if (Input.GetButtonDown("LButton"))
                    ControlIndex--;
                if (MainScript.GetComponent<GameMain>().NormalCount == 0)
                {
                    TutorialRenderer.sprite = TutorialSprite[17];
                    if (Input.GetButtonDown("RButton"))
                        ControlIndex++;
                }
                break;
            case 6:
                if (LifeStar.activeSelf == true)
                    LifeStar.SetActive(false);
                if (ControlBlock2.activeSelf == true)
                    ControlBlock2.SetActive(false);
                if (ControlBlock3.activeSelf == false)
                    ControlBlock3.SetActive(true);
                TutorialRenderer.sprite = TutorialSprite[18];
                if (Input.GetButtonDown("RButton"))
                    ControlIndex++;
                break;
            case 7:
                TutorialRenderer.sprite = TutorialSprite[19];
                if (Input.GetButtonDown("LButton"))
                    ControlIndex--;
                if (Input.GetButtonDown("RButton"))
                    ControlIndex++;
                break;
            case 8:
                TutorialRenderer.sprite = TutorialSprite[20];
                if(MainScript.GetComponent<GameMain>().NormalCount==0)
                {
                    TutorialRenderer.sprite = TutorialSprite[21];
                    if (Input.GetButtonDown("RButton"))
                    ControlIndex++;
                }
                if (Input.GetButtonDown("LButton"))
                    ControlIndex--;
                break;
            case 9:
                if (ControlBlock3.activeSelf == true)
                    ControlBlock3.SetActive(false);
                TutorialRenderer.sprite = TutorialSprite[22];
                if (Input.GetButtonDown("LButton"))
                    SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
                if (Input.GetButtonDown("RButton"))
                    SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
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
