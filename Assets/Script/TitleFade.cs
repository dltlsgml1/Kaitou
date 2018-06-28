using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFade : MonoBehaviour
{
	public GameObject TitleObj;
	public GameObject FogObj;

	public bool TitleStartFlag = false;
	public bool TitleEndFlag = false;

	public bool alphaFlag = true;
	public bool emissionUpFlag = false;
	public bool emissionDownFlag = false;

	public bool endFlag = false;

	public float FadeS = 0.2f;
	public float FogEmissionFadeUp = 0.3f;
	public float FogEmissionFadeDown = 0.1f;

	private Color TitleFadeSpeed;
	private Color FogFadeSpeed;


	public bool ChangeEmissionFlag = false;      //点滅し始めるフラグ
	float time = 0;                             //秒数計算用
	float timeCount = 0;                        //秒数計算用

	public float MiniEmission = 0.0f;
	public float MaxEmission = 0.3f;

	public double UpSpeed = 0.1f;
	public double DownSpeed = 0.1f;

	//時間確認
	public bool stopFlag = false;     //次のシーン行くまで待機的なフラグ
	private float countTime = 0.0f;
	private float endTime = 2.0f;
	public float TitleFadeTime = 4.0f;
	bool endChake = false;

	//Fadeアウトする
	public bool SceneChangeFlag = false;

	//ゲームのClear判定
	public GameMain MainScript;
	public MoveCamera moveCamera;
	public failed titleFade;

	//クリア・失敗のときのSEフラグ
	bool PlayedSE = false;

	// Use this for initialization
	void Start()
	{
		TitleFadeSpeed = new Color(TitleObj.GetComponent<SpriteRenderer>().color.r, TitleObj.GetComponent<SpriteRenderer>().color.g, TitleObj.GetComponent<SpriteRenderer>().color.b, FadeS);
		FogFadeSpeed = new Color(FogObj.GetComponent<Renderer>().material.color.r, FogObj.GetComponent<Renderer>().material.color.g, FogObj.GetComponent<Renderer>().material.color.b, FadeS);

		//カメラ操作を止める
		moveCamera.StopCameraOn();
	}

	// Update is called once per frame
	void Update()
	{
		//サウンド再生
		Sound.StopBgm();
		if (PlayedSE == false)
		{
			PlayedSE = true;
			//タイトル表示SE
			Sound.PlaySe("SE_CLEAR");
		}
		//タイトルアニメーションstart
		TitleAnima();

		if (TitleEndFlag) {
			SceneChange ();
		}

	}

	//タイトルアニメーション関数
	//タイトルを表示させる。
	void TitleAnima()
	{
		//タイトルの表示が一定時間立ったら
		//alphaフラグが立ったらを上げる
		if (alphaFlag)
		{
			//文字をフェードINしていく
			TitleFadeSpeed.a += FadeS;
			FogFadeSpeed.a += FadeS;

			//オブジェクトにカラーを適用する。
			TitleObj.GetComponent<SpriteRenderer>().color = TitleFadeSpeed;
			FogObj.GetComponent<Renderer>().material.color = FogFadeSpeed;

			Debug.Log ("fog"+FogObj.GetComponent<Renderer>().material.color);

			//alphaが1.0f以下いなったら入る。
			if (TitleObj.GetComponent<SpriteRenderer>().color.a >= 1.0f && FogObj.GetComponent<Renderer>().material.color.a >= 1.0f)
			{
				//alphaフラグを止める。→減少ストップ
				alphaFlag = false;

				ChangeEmissionFlag = true;
				emissionUpFlag = true;

			}
		}

		//Fogを光らせる。
		if (ChangeEmissionFlag)
		{

			time = Mathf.PingPong(timeCount, MaxEmission + 0.5f); //これでマックスエミッションまで行き来するようにして

			//ここでスピード調整して行き来する。
			if (emissionUpFlag)
			{
				timeCount += (float)UpSpeed;
			}
			if (emissionDownFlag)
			{
				timeCount -= (float)DownSpeed;
			}


			float val = time;
			float num = val * val;
			// Color color = new Color(val * val, val * val, val * val);
			Color color = new Color(val, val, val); //エミッションの光度を変えてる。
			FogObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", color); //ここで色を入れ込む。



			//光るか光らなくなるかを見てる
			if (time <= MaxEmission && !emissionDownFlag)
			{
			}
			else
			{
				if (emissionDownFlag)
				{

				}
				else
				{
					emissionUpFlag = false;
					emissionDownFlag = true;
				}
			}
			if (time >= MiniEmission && !emissionUpFlag)
			{
			}
			else
			{
				if (emissionUpFlag)
				{

				}
				else
				{
					emissionUpFlag = true;
					emissionDownFlag = false;
					ChangeEmissionFlag = false;
					TitleEndFlag = true;
				}
			}
		}
	}
		

	//表示が終わったら
	void TitleOutAnima()
	{
		Debug.Log ("タイトル消してるなう");
		//alphaを下げてフェードアウトさせる
		TitleFadeSpeed.a -= FadeS;
		FogFadeSpeed.a -= FadeS;

		//数値お適用する
		TitleObj.GetComponent<SpriteRenderer>().color = TitleFadeSpeed;
		FogObj.GetComponent<Renderer>().material.color = FogFadeSpeed;


		//フェードアウトしたらチェンジフラグを入れる。
		if (TitleObj.GetComponent<SpriteRenderer>().color.a <= 0.0f && FogObj.GetComponent<Renderer>().material.color.a <= 0.0f)
		{
			//シーンのチェンジ用のフラグを立てる。
			SceneChangeFlag = true;
		}

	}

	void SceneChange()
	{
		//タイトル表示時間の計算
		countTime += Time.deltaTime;
		//タイトルおendTime時間幼児したらアウトする。
		if(endTime <= countTime)
		{
			stopFlag = false;
			if(TitleEndFlag)
			{
				TitleOutAnima();

			}
		}

		//タイトルアウトアニメが終わったら入る。
		if(SceneChangeFlag)
		{
			//ここでフェードインさせる。
			titleFade.FadeOut_On();
			//カメラ操作をOnにする
			moveCamera.StopCameraOff();
			TitleObj.SetActive (false);
			FogObj.SetActive (false);
			countTime = 0.0f;
		}

	}
}
