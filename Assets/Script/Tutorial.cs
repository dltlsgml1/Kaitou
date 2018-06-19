using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public static int CircleMax = 10;
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

    public GameObject[] Circles = new GameObject[CircleMax];


    public int TutorialIndex = 0;
    public int ExplainIndex = 0;
    public int ControlIndex = 0;
    
    // Use this for initialization
    void Start()
    {
        MainScript.GetComponent<GameMain>().TutorialFlg = true;
    }

    // Update is called once per frame
    void Update()
    {
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
                text.text = "このゲームはカメラを動かし、ブロックを画面上で繋げて光を移すパズルです。";
                NormalBlock.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f));
                NormalBlock.GetComponent<Blocks>().BurnFlg = false;
                NormalBlock.GetComponent<Blocks>().canburn = false;
                if(Input.GetButtonDown("RButton"))  //Todo
                    ExplainIndex++;
                break;
            case 1:
                text.text = "すべてのブロックが規定回数内に\n移され光るとクリアです。";
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Input.GetButtonDown("LButton"))   //Todo
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))   //Todo
                    ExplainIndex++;
                break;
            case 2:
                text.text = "コントローラの左スティックでカメラを移動します。";
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
                if (Input.GetButtonDown("LButton"))   //Todo
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))   //Todo
                    ExplainIndex++;
                break;
            case 3:
                text.fontSize = 70;
                text.text = "コントローラの右スティックでカメラを回転します。";
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
                if (Input.GetButtonDown("LButton"))       //Todo
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))       //Todo
                    ExplainIndex++;
                break;
            case 4:
                text.fontSize = 60;
                text.text = "ゆっくりと動かしたいときは、L2トリガーを押しながら操作します。\n早く動かしたいときは、R2トリガーを押しながら操作します。";
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
                if (Input.GetButtonDown("LButton"))       //Todo
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))       //Todo
                    ExplainIndex++;
                break;
            case 5:
                text.fontSize = 70;
                text.text = "スタートボタンで、ゲームをポーズすることができます。\nチュートリアルではバックメニューのみ機能します。";
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
                if (Input.GetButtonDown("LButton"))       //Todo
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))       //Todo
                    ExplainIndex++;
                break;
            case 6:
                text.text = "バックボタンで、カメラの位置、角度を\n初期位置にリセットすることができます。";
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
                if (Input.GetButtonDown("LButton"))       //Todo
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))       //Todo
                    ExplainIndex++;
                break;
            case 7:
                text.text = "それでは、ブロックを動かし、繋げてみましょう";
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

                if (Input.GetButtonDown("LButton"))          //Todo
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
                    text.text = "ブロックがくっつきました！そのままAボタンを押し続けてください。";
                    if (Circles[(int)CircleIndex.BButton].activeSelf == false)
                        Circles[(int)CircleIndex.BButton].SetActive(true);
                }
                if (NormalBlock.GetComponent<Blocks>().BurnFlg == true)
                    ExplainIndex++;
                break;
            case 8:
                text.text = "よくできました！ブロックが燃え移りました。\nこれでクリアとなります。";
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                //if (Input.GetButtonDown("LButton"))          //Todo
                //    ExplainIndex = 0;
                if (Input.GetButtonDown("RButton"))          //Todo
                    ExplainIndex++;
                break;
            case 9:
                if (ExplainBlock.activeSelf == true)
                    ExplainBlock.SetActive(false);
                text.text = "移したあと、移されたブロックは更に他のブロックに\n光をを移すことができます。\n" +
                              "このことを用いて、連続で移すことができます。\n\n" +
                              "ブロックの中では、面が黒く塗られてあるものがあります。\n" +
                              "これは、その面からは移すことはできないことを表します。\n\n";
   

                if (Input.GetButtonDown("LButton"))          //Todo
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))          //Todo
                    ExplainIndex++;
                break;
            case 10:
                text.text = "背景に星と星をつなぐ星座があります。（黄色い線）\n" +
                              "星座の線は１回ブロックを移すと、１個ずつ減り、\n" +
                              "すべてなくなるとゲームオーバーとなります。\n\n" +
                              "それでは、次ボタンを押してゲームを始めます！";
                if (Input.GetButtonDown("LButton"))          //Todo
                    ExplainIndex--;
                if (Input.GetButtonDown("RButton"))          //Todo
                    SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
                break;

        }
        
    }
    void Control()
    {
        switch (ControlIndex)
        {
            case 0:
                MainScript.GetComponent<GameMain>().SetBlock();
                if (ExplainBlock.gameObject.activeSelf == true)
                    ExplainBlock.gameObject.SetActive(false);
                if (ControlBlock1.gameObject.activeSelf == false)
                    ControlBlock1.gameObject.SetActive(true);
                text.text = "Control1";
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }

    }

    private void OnDestroy()
    {
        MainScript.GetComponent<GameMain>().TutorialFlg = false;
    }
}
