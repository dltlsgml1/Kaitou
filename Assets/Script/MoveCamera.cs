using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    public Camera MainCamera;
    public Camera Background;

    Vector3 FormatPosition;                     //位置の初期化用
    Vector3 FormatRotation;                     //回転の初期化用
    Vector3 Position;                           //位置のデータ
    Vector3 Rotation;                           //回転のデータ
    Vector2 ScreenPosition;                     //スクリーンの位置データ
    public float MoveCameraSpeed = 0.1f;        //平行移動時のスピード
    public float RotationCameraSpeed = 0.5f;    //カメラ回転のスピード
    
    // Use this for initialization
    void Start() {
        FormatPosition.x = 0;
        FormatPosition.y = 0;
        FormatPosition.z = 0;
        FormatRotation.x = 0;
        FormatRotation.y = 0;
        FormatRotation.z = 0;
        Position = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        ParallelMove();
        RotationCamera();
        FormatDate();
        InputDate();
        
	}

    public void ParallelMove()      //平行移動関数
    {
        ScreenPosition = transform.InverseTransformPoint(Position);     //スクリーン座標に現在の座標の逆行列をセット
        //カメラのスクリーン座標変換
        if (Input.GetKey(KeyCode.D))
        {
            ScreenPosition.x -= MoveCameraSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            ScreenPosition.x += MoveCameraSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            ScreenPosition.y -= MoveCameraSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            ScreenPosition.y += MoveCameraSpeed;
        }


        Position = transform.TransformPoint(ScreenPosition);       //返還されたスクリーン座標をワールドに戻してセット
    }

    public void RotationCamera()    //カメラ回転関数
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(Rotation.x<85)
            Rotation.x += RotationCameraSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if(Rotation.x>-60)
            Rotation.x -= RotationCameraSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rotation.y += RotationCameraSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Rotation.y -= RotationCameraSpeed;
        }
    }

    void ChangeSize() //カメラサイズを変更してズームインズームアウトを表現
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (MainCamera.orthographicSize < 20)
            {
                MainCamera.orthographicSize += 0.1f;
                Background.orthographicSize += 0.1f;
            }
        }
        if (Input.GetKey(KeyCode.X))
        {
            if (MainCamera.orthographicSize > 1)
            {
                MainCamera.orthographicSize -= 0.1f;
                Background.orthographicSize -= 0.1f;
            }
        }
    }

    public void FormatDate()        //初期化関数
    {
        if (Input.GetKey(KeyCode.R))
        {
            Position = FormatPosition;
            Rotation = FormatRotation;
        }
    }
    public void InputDate()         //データの入れ込み
    {
        this.transform.position = Position;
        this.transform.rotation = Quaternion.Euler(Rotation);
    }
}
