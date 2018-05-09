using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ChangeSizeCamera : MonoBehaviour
{
    public float ChangeSize(float Size) //カメラサイズを変更してズームインズームアウトを表現
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (Size < 20)
            {
                Size += 0.1f;
            }
        }
        if (Input.GetKey(KeyCode.X))
        {
            if (Size > 1)
            {
                Size -= 0.1f;
            }
        }
        return Size;
    }
}


public class SizeChage : MonoBehaviour {
    Camera cam;
    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();   //カメラのデータを持ってくる
    }

	// Update is called once per frame
	void Update ()
    {
        ChangeSizeCamera changeSize = new ChangeSizeCamera();   //サイズ変える用のクラスをロード
        cam.orthographicSize = changeSize.ChangeSize(cam.orthographicSize); //カメラサイズの変更

    }
}
