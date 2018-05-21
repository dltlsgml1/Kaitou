﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CsvLoad : MonoBehaviour {

	public string StageDatePath = "CSV/StageData.csv";

	//アビリティデータリスト
	//Characterのデータリスト
	public struct StageDate {
		public Nullable<int> StageID;       //CSVのステージID部分
		public string StageName;             //CSVのstagename部分
        public Nullable<int> UpperCunt;      //CSVの上限回数部分
        public Nullable<bool> ClearFlag;      //CSVのクリアチェック部分
        public string StageTitle;             //CSVのTitle部分
    }
	//実際のキャラデータ格納箇所
	public List<StageDate> StageDateList = new List<StageDate>();


// --- CSV読み込み関数変数 ----------
	//csv読み込みデータ格納用二次元配列
	private string[,] sData;
	private string[] fields;

	//読み込めたか確認の表示用の変数
	private int height = 0;    //行数

	//public string line = "";

	private void csvLoad(string path,ref string [ , ] sdata)
	{
		TextAsset csv = Resources.Load (path) as TextAsset;
        Debug.Log("resourceload直後" + csv);
		StringReader sr = new StringReader (csv.text);
		// ストリームリーダーをstringに変換
		string strStream    = sr.ReadToEnd( );
        Debug.Log(strStream.Length);
		// StringSplitOptionを設定(要はカンマとカンマに何もなかったら格納しないことにする)
		System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;
		// カンマ分けの準備(区分けする文字を設定する)
		char [ ] spliter = new char [1] { ',' };
        
        //行に分ける
        string [ ] lines = strStream.Split(new char [ ] { '\r', '\n' }, option);
        Debug.Log(lines.Length);
        // 行数設定
        int h = lines.Length;
        
        // 列数設定
		int w = lines[0].Split(spliter, option).Length;

		// 返り値の2次元配列の要素数を設定
		sdata = new string [h, w];

		// 行データを切り分けて,2次元配列へ変換する
		for(int i = 0; i < h; i++)
		{
			string [ ] splitedData = lines [i].Split(spliter, option);

			for(int j = 0; j < w; j++)
			{
				sdata [i, j] = splitedData [j];
			}
		}

		// 確認表示用の変数(行数、列数)を格納する
		this.height = h;    //行数   

		Resources.UnloadAsset (csv);
	}

//--- キャラデータコンバート
	private void charDataConvert(string[,] arrays,int h){
		for (int i = 0; i < h; i++) {
			if (i == 0) {

		        StageDateList.Add (new StageDate () {
					StageID = null,
					StageName = null,
					UpperCunt = null,
					ClearFlag = null,
					StageTitle = null,
				});
			} else {

                StageDateList.Add(new StageDate()
                {
                    StageID = int.Parse (arrays [i, 0]),
                    StageName = arrays [i, 1],
                    UpperCunt = int.Parse (arrays [i, 2]),
                    ClearFlag = bool.Parse (arrays [i, 3]),
                    StageTitle = arrays[i, 4],
                    
				});
			}
		}

		//デバッグログ確認
		for (int i = 0; i < h; i++) {
			Debug.Log(i + ":" + "StageID/" + StageDateList[i].StageID);        
			Debug.Log(i + ":" + "StageName/" + StageDateList[i].StageName);
			Debug.Log(i + ":" + "UpperCunt/" + StageDateList[i].UpperCunt);
			Debug.Log(i + ":" + "ClearFlag/" + StageDateList[i].ClearFlag);
			Debug.Log(i + ":" + "StageTitle/" + StageDateList[i].StageTitle);
		}
	}
	// Use this for initialization
	void Start () {
        //キャラクターデータ
        Debug.Log("スタート直後" + StageDatePath);
		csvLoad (StageDatePath, ref this.sData);
		charDataConvert (this.sData,this.height);

		Debug.Log ("CSVLoadStart");
	}



	// Update is called once per frame
	void Update () {
	
	}
}
