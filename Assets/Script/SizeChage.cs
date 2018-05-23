using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SizeChage : MonoBehaviour {
    Camera Camera;
    // Use this for initialization
    void Start()
    {
        Camera = GetComponent<Camera>();   //カメラのデータを持ってくる
    }

	// Update is called once per frame
	void Update ()
    {
        ChangeSize(); //カメラサイズの変更

    }
    void ChangeSize() //カメラサイズを変更してズームインズームアウトを表現
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (Camera.orthographicSize < 20)
            {
                Camera.orthographicSize += 0.1f;
            }
        }
        if (Input.GetKey(KeyCode.X))
        {
            if (Camera.orthographicSize > 1)
            {
                Camera.orthographicSize -= 0.1f;
            }
        }
    }
}
