using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static int CircleMax = 10;
    public enum CircleIndex {RightStick=0,LeftStick,BButton,SlowButton};
    public GameObject ScriptObject;
    public Text text;
    public GameObject CollapsBlock;
    public GameObject NormalBlock;
    public GameObject[] Circles = new GameObject[CircleMax];
    public int TutorialIndex = 0;
    public int ExplainIndex = 0;
    public int ControlIndex = 0;
    
    // Use this for initialization
    void Start()
    {
        ScriptObject.GetComponent<GameMain>().TutorialFlg = true;
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
                text.text = "このゲームはカメラを動かし、ブロックがくっついているようにし、燃え移すゲームです。";
                if(Input.GetKeyDown(KeyCode.Alpha2))  //Todo
                    ExplainIndex++;
                break;
            case 1:
                text.text = "すべてのブロックが\n燃え移されるとゲームクリアです。";
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))   //Todo
                    ExplainIndex--;
                if (Input.GetKeyDown(KeyCode.Alpha2))   //Todo
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
                if (Input.GetKeyDown(KeyCode.Alpha1))   //Todo
                    ExplainIndex--;
                if (Input.GetKeyDown(KeyCode.Alpha2))   //Todo
                    ExplainIndex++;
                break;
            case 3:
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
                if (Input.GetKeyDown(KeyCode.Alpha1))       //Todo
                    ExplainIndex--;
                if (Input.GetKeyDown(KeyCode.Alpha2))       //Todo
                    ExplainIndex++;
                break;
            case 4:
                text.text = "ゆっくりと動かしたいときは、Todoボタンを押しながら操作します。";
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
                if (Input.GetKeyDown(KeyCode.Alpha1))       //Todo
                    ExplainIndex--;
                if (Input.GetKeyDown(KeyCode.Alpha2))       //Todo
                    ExplainIndex++;
                break;
            case 5:
                text.text = "それでは、ブロックを動かし、くっつかしてみましょう";
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))          //Todo
                    ExplainIndex--;

                if(NormalBlock.GetComponent<Blocks>().NormalNowcol==true)
                {
                    text.text = "ブロックがくっつきました！そのままTodoボタンを押してください。";
                    if (Circles[(int)CircleIndex.BButton].activeSelf == false)
                        Circles[(int)CircleIndex.BButton].SetActive(true);
                }
                if (NormalBlock.GetComponent<Blocks>().BurnFlg == true)
                    ExplainIndex++;
                break;
            case 6:
                text.text = "よくできました！ブロックが燃え移りました。\nこれでクリアとなります。";
                for (int i = 0; i < Circles.Length; i++)
                {
                    if (Circles[i] != null)
                    {
                        if (Circles[i].activeSelf == true)
                            Circles[i].SetActive(false);
                    }
                }
                break;
                

        }
        
    }
    void Control()
    {

    }

    private void OnDestroy()
    {
        ScriptObject.GetComponent<GameMain>().TutorialFlg = false;
    }
}
