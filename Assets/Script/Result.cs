using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Result : MonoBehaviour {


    public GameObject SetImage;
    public GameObject SetCursol;
    public GameObject[] StarObj = new GameObject[3];
    public GameObject[] Select=new GameObject[4];

    public Material[] starmat = new Material[3];
    public bool fadeInflg;
    public bool fadeOutflg;
    public bool resultAnimationStart;
    public bool result_ok;
    public bool bronzflg;
    public bool silverflg;
    public bool goldflg;

    Image fade;
    
    Transform KeyPosition;
    Transform[] SetStar = new Transform[3];
    Vector3[] StarSize = new Vector3[3];
    SpriteRenderer[] Selectfade = new SpriteRenderer[4];

    float[] size = new float[3]; 
    int Cursolnum;
    float fade_color_A;
    float select_color_A;

    //星サイズ　900
    const float SetCursol_x = -200;
    const float SetCursol_z = -150;
    const float Cursol1 = -220;
    const float Cursol2 = -320;
    const float Cursol3 = -420;

    const float fadespeed=0.05f;


	// Use this for initialization
	void Start () {


        fadeInflg = false;
        fadeOutflg = true;
        resultAnimationStart = false;
        result_ok = false;

        Cursolnum = 0;
        fade_color_A = 1;
        select_color_A = 0;
        size[0] = size[1] = 0; size[2] = 0;

        StarSize[0] = new Vector3(0,0,0);
        StarSize[1] = new Vector3(0, 0, 0);
        StarSize[2] = new Vector3(0, 0, 0);

        SetStar[0] = StarObj[0].GetComponent<Transform>();
        SetStar[1] = StarObj[1].GetComponent<Transform>();
        SetStar[2] = StarObj[2].GetComponent<Transform>();

        for (int i = 0; i < 4; i++)
        {
            Selectfade[i] = Select[i].GetComponent<SpriteRenderer>();
        }

            if (bronzflg)
            {
                SetStar[0].GetComponent<MeshRenderer>().material = starmat[0];
                SetStar[1].GetComponent<MeshRenderer>().material = starmat[0];
                SetStar[2].GetComponent<MeshRenderer>().material = starmat[0];
            }
            else if (silverflg)
            {
                SetStar[0].GetComponent<MeshRenderer>().material = starmat[1];
                SetStar[1].GetComponent<MeshRenderer>().material = starmat[1];
                SetStar[2].GetComponent<MeshRenderer>().material = starmat[1];
            }
            else if (goldflg)
            {
                SetStar[0].GetComponent<MeshRenderer>().material = starmat[2];
                SetStar[1].GetComponent<MeshRenderer>().material = starmat[2];
                SetStar[2].GetComponent<MeshRenderer>().material = starmat[2];
            } //public Material[] lightmat = new Material[10];
    //this.transform.GetChild(i).GetComponent<MeshRenderer>().material = lightmat[i];

        KeyPosition = SetCursol.GetComponent<Transform>();
        fade = SetImage.GetComponent<Image>();

        fade.color = new Color(1, 1, 1, fade_color_A);
        KeyPosition.localPosition = new Vector3(SetCursol_x, Cursol1, SetCursol_z);
	}
	
	// Update is called once per frame
	void Update () {
        if (result_ok)
        {
            //入力処理
            InputCollection();

            //カーソル位置更新
            KeyCheck();
            //ShowKey();

            //選択肢フェードイン
            SelectFadeIn();

        }
        //フェード更新
        ResultFade();

        //リザルトアニメーション更新
        ResultAnimation();

	}

    void InputCollection()
    {
        //カーソル移動
        if (Input.GetKeyDown("up"))
        {
            if (Cursolnum<1)
            {
                Cursolnum = 2;
            }
            else
            {
                Cursolnum--;
            }
        }
        if (Input.GetKeyDown("down"))
        {
            if (Cursolnum > 1)
            {
                Cursolnum = 0;
            }
            else
            {
                Cursolnum++;
            }
        }

        //決定
        //if (Input.GetKeyDown("SPACE"))
        //{
        //    switch (Cursolnum)
        //    {
        //        case 0:
        //            SceneManager.LoadScene("");
        //            break;
        //        case 1:
        //            SceneManager.LoadScene("");
        //            break;
        //        case 2:
        //            SceneManager.LoadScene("");
        //            break;
        //    }
        //}


    }

    void KeyCheck()
    {
        switch (Cursolnum)
        {
            case 0:
                KeyPosition.gameObject.transform.localPosition = new Vector3(SetCursol_x, Cursol1, SetCursol_z);
                break;
            case 1:
                KeyPosition.gameObject.transform.localPosition = new Vector3(SetCursol_x, Cursol2, SetCursol_z);
                break;
            case 2:
                KeyPosition.gameObject.transform.localPosition = new Vector3(SetCursol_x, Cursol3, SetCursol_z);
                break;
        }
    }



    void ResultFade()
    {
        if (fadeInflg)
        {
            if (fade_color_A > 1)
            {
                fadeInflg = false;
            }
            fade_color_A += fadespeed;
        }
        else if (fadeOutflg)
        {
            if (fade_color_A < 0)
            {
                fadeOutflg = false;
                resultAnimationStart = true;
            }
            fade_color_A -= fadespeed;

        }
        fade.color = new Color(1,1,1, fade_color_A);
    }

    void SelectFadeIn()
    {
        if (select_color_A < 255)
        {
            select_color_A += fadespeed;
            for (int i = 0; i < 4; i++)
            {
                Selectfade[i].color = new Color(255, 255, 255, select_color_A);
            }
        }
    }


    void ResultAnimation()
    {
        if (resultAnimationStart)
        {
            //-7
            if (size[0] >= -7)
            {
                size[0] -=0.5f;
                SetStar[0].transform.localPosition = new Vector3(-19.5f, -8.5f, size[0]);
            }
            else if (size[0] <= -7&&(silverflg == true || goldflg == true ) && size[1] >= -7)
            {
                size[1] -=1.0f;
                SetStar[1].transform.localPosition = new Vector3(3.3f, -8.5f, size[1]);
            }
            else if (size[1] <= -7 && goldflg == true && size[2] >= -7)
            {
                size[2] -=1.0f;
                SetStar[2].transform.localPosition = new Vector3(26.0f, -8.5f, size[2]);
            }
            else
            {
                resultAnimationStart = false;
                result_ok = true;
            }
        }
    }
   

}
