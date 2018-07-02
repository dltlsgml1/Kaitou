using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class ControlCamera: MonoBehaviour
{
    Vector3 m_formatpos;                              //位置データ初期化用変数
    Vector3 m_formatrotation;                         //回転データ初期化用変数
    public float m_Movecameraspeed = 0.1f;            //移動のスピード
    public float m_Changecameraspeed = 0.5f;          //回転のスピード

    public Vector2 Movecamera(Vector2 position)       //位置データ入力
    {
        if (Input.GetKey(KeyCode.D))
        {
            position.x -= m_Movecameraspeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            position.x += m_Movecameraspeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            position.y -= m_Movecameraspeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            position.y += m_Movecameraspeed;
        }

        return position;
    }
    public Vector3 Rotationcamera(Vector3 rotation)   //回転データ入力
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotation.x += m_Changecameraspeed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rotation.x -= m_Changecameraspeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation.y -= m_Changecameraspeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation.y += m_Changecameraspeed;
        }
        return rotation;
    }

    public Vector3 Resetpos(Vector3 position)         //位置データ初期化
    {
        m_formatpos.x = 0;
        m_formatpos.y = 0;
        m_formatpos.z = 0;
        if (Input.GetKey(KeyCode.R))
        {
            position = m_formatpos;
        }
            return position;
    }
    public Vector3 ResetQuaternion(Vector3 rotation)  //回転データ初期化
    {

        m_formatrotation.x = 0;
        m_formatrotation.y = 0;
        m_formatrotation.z = 0;
        if (Input.GetKey(KeyCode.R))
        {
            rotation = m_formatrotation;
        }
        return rotation;
    }
     
}   

public class NewBehaviourScript : MonoBehaviour
{
    Vector3 rotation;                   //ゲームオブジェクトの回転データを変更用の変数に代入
    // Use this for initialization
    void Start () {
        rotation.x = 0;                 //X初期化
        rotation.y = 0;                 //Y初期化
        rotation.z = 0;                 //Z初期化

    }
	
	// Update is called once per frame
	void Update () {
        
        Vector3 camerapos = this.transform.position;                //ゲームオブジェクトの位置データを変更用の変数に代入
        Quaternion camerarotion = this.transform.rotation;          //ゲームオブジェクトの回転データを変更用の変数に代入
        Vector2 pos=transform.InverseTransformPoint(camerapos);     // ワールド座標をスクリーン座標に変更
        ControlCamera Cameracontrol = new ControlCamera();          //カメラクラスの読み込み
        pos = Cameracontrol.Movecamera(pos);                        //スクリーン座標のオブジェクトの平行移動
        camerapos = transform.TransformPoint(pos);                  //スクリーン座標をワールド座標に変換

        rotation = Cameracontrol.Rotationcamera(rotation);          //ゲームオブジェクトを回転
        camerapos = Cameracontrol.Resetpos(camerapos);              //Rを押したときゲームオブジェクトの位置の初期化
        rotation = Cameracontrol.ResetQuaternion(rotation);         //Rを押したときゲームオブジェクトの回転の初期化
        this.transform.position = camerapos;                        //ゲームオブジェクトに位置データの更新
        this.transform.rotation = Quaternion.Euler(rotation);       //ゲームオブジェクトに
    }

}
