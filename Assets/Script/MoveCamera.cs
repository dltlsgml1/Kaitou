using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveCamera : MonoBehaviour {
    public Camera MainCamera;
    public Camera Background;
    public static bool  ResetFlg = false;
    Vector3 OldPosition;                        //Moveフラグをどうにかするためのポジション
    Quaternion OldQuaternion;                   //Moveフラグ用の旧ポジション
    Vector3 FormatPosition;                     //位置の初期化用
    Vector3 FormatRotation;                     //回転の初期化用
    public Vector3 Position;                           //位置のデータ
    public Vector3 Rotation;                           //回転のデータ
    Vector2 ScreenPosition;                     //スクリーンの位置データ
    float Key;                                  //Axisで取られた正負の確認
    float DefaultKey=0.7f;                      //勝手に移動するの防ぐ用
    public bool CheckDebug;
    public bool HiSpeedChangeFlag;              //スピード切り替えのフラグ　早い場合
    public bool LowSpeedChangeFlag;             //スピード切り替えのフラグ　遅い場合
    public bool MoveFlag;                       //カメラを移動させているか
    public bool StopCamera = false;
    private float ChangeSpeedLow = 0.1f;         //スピードチェンジ時の速さ(遅い時)
    private float ChangeSpeedFast = 1.0f;        //スピードチェンジ時の速さ(早い時)
    private float MoveCameraSpeed = 0.1f;        //平行移動時のスピード
    private float RotationCameraSpeed = 0.5f;    //カメラ回転のスピード
    
    
    // Use this for initialization
    void Start() {
        FormatPosition = PassStageID.PassPosition();
        FormatRotation = PassStageID.PassRotation();
    }

    // Update is called once per frame
    void Update() {
        ChangeSpeed();
    }
    // Update is called once per frame
    void LateUpdate () {
        OldPosition = this.transform.position;
        OldQuaternion = this.transform.rotation;
        if (!StopCamera)
        {
            if (Pause.is_pause) { return; }
            if (!Input.GetButton("AButton"))
            {
             //   ParallelMove();
                RotationCamera();
                FormatDate();
                InputDate();
                BackStageSelect();
                KeyDebug();
            }
        }
	}
    public void ChangeSpeed()
    {
        HiSpeedChangeFlag = false;
        LowSpeedChangeFlag = false;
        Key = Input.GetAxisRaw("SpeedChange");
        if (Key!=0)
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
                MoveFlag = true;
                ScreenPosition.x += MoveCameraSpeed;
            }
            if (Key > -DefaultKey)
            {
                MoveFlag = true;
                ScreenPosition.x -= MoveCameraSpeed;
            }
        }
        Key = Input.GetAxisRaw("RightStick Y");
        if (Key != 0)
        {
            if (Key > -DefaultKey)
            {
                MoveFlag = true;
                ScreenPosition.y += MoveCameraSpeed;
            }
            else
            {

            }

            if (Key < DefaultKey)
            {
                MoveFlag = true;
                ScreenPosition.y -= MoveCameraSpeed;
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
                //if(Rotation.x < 85)
                if(Rotation.x < 90)
                {
                    if (HiSpeedChangeFlag)
                        Rotation.x += ChangeSpeedFast;

                    if (LowSpeedChangeFlag)
                        Rotation.x += ChangeSpeedLow;

                    if (!LowSpeedChangeFlag && !HiSpeedChangeFlag)
                        Rotation.x += RotationCameraSpeed;
                }
                else
                {

                }

            }
            if(Key > -DefaultKey)
            {
               // if (Rotation.x > -60)
                if (Rotation.x > -90)
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
            if (Key > -DefaultKey)
            {
                if (HiSpeedChangeFlag)
                    Rotation.y += ChangeSpeedFast;

                if (LowSpeedChangeFlag)
                    Rotation.y += ChangeSpeedLow;

                if (!LowSpeedChangeFlag && !HiSpeedChangeFlag)
                    Rotation.y += RotationCameraSpeed;
            }

            if (Key < DefaultKey)
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
        if (this.transform.position == OldPosition)
        {
            MoveFlag = false;
        }
        else
        {
            MoveFlag = true;
        }
        this.transform.rotation = Quaternion.Euler(Rotation);
        if (this.transform.rotation == OldQuaternion)
        {
            MoveFlag = false;
        }
        else
        {
            MoveFlag = true;
        }
    }
    public void BackStageSelect()
    {
       /* if (Input.GetButtonDown("StartButton"))
        {
            SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
        }*/
    }
    public void KeyDebug()          //keyの入力が出来てるかのデバッグ用(削除する予定)
    {

    } 
    public void correction()        //位置の補正
    {

    }
    public void StopCameraOn()      //カメラを止める
    {
        StopCamera = true;
    }

    public void StopCameraOff()     //カメラを動かす
    {
        StopCamera = false;
    }
}
