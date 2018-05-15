using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SizeChage : MonoBehaviour {
    Camera Camera;
    int FormatSize = 5;
    // Use this for initialization
    void Start()
    {
        Camera = GetComponent<Camera>();   //カメラのデータを持ってくる
    }

	// Update is called once per frame
	void Update ()
    {
        ChangeSize(); //カメラサイズの変更
        Format();   //カメラサイズの初期化

    }
    void ChangeSize() //カメラサイズを変更してズームインズームアウトを表現
    {
        if (Input.GetButton("LButton"))
        {
            if (Camera.orthographicSize < 20)
            {
                Camera.orthographicSize += 0.1f;
            }
        }
        if (Input.GetButton("RButton"))
        {
            if (Camera.orthographicSize > 1)
            {
                Camera.orthographicSize -= 0.1f;
            }
        }
    }
    void Format()//カメラサイズの初期化
    {
        if (Input.GetButton("SelectButton"))
        {
            Camera.orthographicSize = FormatSize;
        }
    }
}
