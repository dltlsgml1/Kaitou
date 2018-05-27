using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    public Camera MainCamera;
    public Camera Background;
    public Pause pause;              //ポーズスクリプト宣言

    Vector3 FormatPosition;                     //位置の初期化用
    Vector3 FormatRotation;                     //回転の初期化用
    Vector3 Position;                           //位置のデータ
    Vector3 Rotation;                           //回転のデータ
    Vector2 ScreenPosition;                     //スクリーンの位置データ
    float Key;                                  //Axisで取られた正負の確認
    float DefaultKey=0.5f;                      //勝手に移動するの防ぐ用
    public bool CheckDebug;
    public bool HiSpeedChangeFlag;              //スピード切り替えのフラグ　早い場合
    public bool LowSpeedChangeFlag;             //スピード切り替えのフラグ　遅い場合
    public float ChangeSpeedLow = 0.1f;         //スピードチェンジ時の速さ(遅い時)
    public float ChangeSpeedFast = 3.0f;        //スピードチェンジ時の速さ(早い時)
    public float MoveCameraSpeed = 0.5f;        //平行移動時のスピード
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
        this.pause = GameObject.Find("CameraObejct/Main Camera").GetComponent<Pause>();//ポーズスクリプト呼び出し
       }

    // Update is called once per frame
    void Update() {
        ChangeSpeed();
    }
    // Update is called once per frame
    void LateUpdate () {
        if (this.pause.is_pause)    //ポーズ中ゲーム維持　時間停止
        {
            return;
        }
        ParallelMove();
        RotationCamera();
        FormatDate();
        InputDate();
        KeyDebug();
        
	}
    public void ChangeSpeed()
    {
        HiSpeedChangeFlag = false;
        LowSpeedChangeFlag = false;
        Key = Input.GetAxisRaw("SpeedChange");
        if(Key!=0)
        ScreenPosition = transform.InverseTransformPoint(Position);     //スクリーン座標に現在の座標の逆行列をセット
        //カメラのスクリーン座標変換
        if (Input.GetKey(KeyCode.D))
        {
            ScreenPosition.x -= MoveCameraSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Key < DefaultKey)
            {
                HiSpeedChangeFlag = true;
            }
            if (Key > -DefaultKey)
            {
                LowSpeedChangeFlag = true;
            }
        }
    }
    public void ParallelMove()      //平行移動関数
    {
        ScreenPosition = transform.InverseTransformPoint(Position); //ワールド座標をスクリーン座標に変換
       

        Key = 0;
        Key = Input.GetAxisRaw("RightStick X");
        if (Key != 0)
        {
            if (Key < DefaultKey)
            {

                ScreenPosition.x -= MoveCameraSpeed;
            }
            if (Key > -DefaultKey)
            {
                ScreenPosition.x += MoveCameraSpeed;
            }
        }
        Key = Input.GetAxisRaw("RightStick Y");
        if (Key != 0)
        {
            if (Key < DefaultKey)
            {

                ScreenPosition.y -= MoveCameraSpeed;
            }
            if (Key > -DefaultKey)
            {
                ScreenPosition.y += MoveCameraSpeed;
            }
        }

        Position = transform.TransformPoint(ScreenPosition);    //スクリーン座標をワールド座標に変換
    }

    public void RotationCamera()    //カメラ回転関数
    {
        Key = 0;

        Key = Input.GetAxisRaw("LeftStick Y");
        if (Key != 0)
        {
            if (Key < DefaultKey)
            {
                if(Rotation.x < 85)
                {
                    if (HiSpeedChangeFlag)
                        Rotation.x += ChangeSpeedFast;

                    if (LowSpeedChangeFlag)
                        Rotation.x += ChangeSpeedLow;

                    if (!LowSpeedChangeFlag && !HiSpeedChangeFlag)
                        Rotation.x += RotationCameraSpeed;
                }
               
            }
            if(Key > -DefaultKey)
            {
                if (Rotation.x > -60)
                {
                    if (HiSpeedChangeFlag)
                        Rotation.x -= ChangeSpeedFast;

                    if (LowSpeedChangeFlag)
                        Rotation.x -= ChangeSpeedLow;

                    if (!LowSpeedChangeFlag && !HiSpeedChangeFlag)
                        Rotation.x -= RotationCameraSpeed;
                }
            }
        }

        Key = Input.GetAxisRaw("LeftStick X");
        if (Key != 0)
        {
            if (Key < DefaultKey)
            {
                if (HiSpeedChangeFlag)
                    Rotation.y += ChangeSpeedFast;

                if (LowSpeedChangeFlag)
                    Rotation.y += ChangeSpeedLow;

                if (!LowSpeedChangeFlag && !HiSpeedChangeFlag)
                    Rotation.y += RotationCameraSpeed;
            }
            if (Key > -DefaultKey)
            {

                if (HiSpeedChangeFlag)
                    Rotation.y -= ChangeSpeedFast;

                if (LowSpeedChangeFlag)
                    Rotation.y -= ChangeSpeedLow;

                if (!LowSpeedChangeFlag && !HiSpeedChangeFlag)
                    Rotation.y -= RotationCameraSpeed;
            }
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
        if (Input.GetButton("SelectButton"))
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

    public void KeyDebug()          //keyの入力が出来てるかのデバッグ用(削除する予定)
    {
        CheckDebug = false;
        if (CheckDebug = Input.GetButtonDown("StartButton"))
        {
            Debug.Log("スタートボタン");
        }
        if (CheckDebug = Input.GetButtonDown("AButton")) 
        {
            Debug.Log("Aボタン");
        }
        if (CheckDebug = Input.GetButtonDown("BButton")) 
        {
            Debug.Log("Bボタン");
        }
    } 
}
