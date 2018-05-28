using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSelect : MonoBehaviour
{

    public enum PouseState { Back, Restart, Stageselect };
    PouseState state;
    public int move = 0;
    public float outside = 20.0f;

    Vector3 vec_Cursor;//= Cursor.transform.localPosition;    
    GameObject Cursor;

	// Use this for initialization
	void Start () {
        this.Cursor = GameObject.Find("CameraObejct/Pause/Pause_Cursor");
        move = 0;
	}
	
	// Update is called once per frame
	void Update () {
        MoveSelect();
	}

    

     private void MoveSelect()
    {
        //ある座標に向かって移動アニメーション追加予定


        //移動先
        if(Input.GetKeyDown("up"))
        {
            move -= 1;
            //SE追加
        }
        if(Input.GetKeyDown("down"))
        {
            move += 1;
            //SE追加
        }
        //Pause選択数分超えないようにループ
        if (move > 2)
        {
            move = 0;
        }
        if(move<0)
        {
            move = 2;
        }
 

       
        vec_Cursor = Cursor.transform.localPosition;
        //Pause画面セレクト指移動
        switch (move) {
            case 0://バック位置
                //指定
                vec_Cursor.y = 1.5f;     //仮置き
                //selectFlg = ture;
                break;
            case 1://リスタート位置
                vec_Cursor.y = 0f;     //仮置き
                //selectFlg = ture;
                break;
            case 2://ステセレ位置
                vec_Cursor.y = -1.0f;     //仮置き
                //selectFlg = ture;
                break;
        }

        Cursor.transform.localPosition = vec_Cursor;

    }

}
