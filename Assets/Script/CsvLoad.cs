using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CsvLoad : MonoBehaviour {
    CSVData CSVPass;
	public string StageDatePath = "CSV/StageData.csv";

	//アビリティデータリスト
	//Characterのデータリスト
	public struct StageDate {
		public Nullable<int> StageID;       //CSVのステージID部分
		public string StageName;             //CSVのstagename部分
        public Nullable<int> UpperCunt;      //CSVの上限回数部分
		public Nullable<int> MinCunt;      //CSVの最小Clear回数部分
		public Nullable<int> GoldCunt;      //CSVの金評価Clear回数部分
		public Nullable<int> SilverCunt;      //CSVの銀評価Clear回数部分
		public Nullable<int> BronzeCunt;      //CSVの同評価Clear回数部分
        public Nullable<int> ClearFlag;      //CSVのクリアチェック部分
        public Nullable<float> Pos_X;        //カメラのポジションX
        public Nullable<float> Pos_Y;        //カメラのポジションY
        public Nullable<float> Pos_Z;        //カメラのポジションZ
        public Nullable<float> Rot_X;        //カメラの角度X
        public Nullable<float> Rot_Y;        //カメラの角度Y

		public Nullable<float> Rot_Z;        //カメラの角度Z
    }
	//実際のキャラデータ格納箇所
	public List<StageDate> StageDateList = new List<StageDate>();


// --- CSV読み込み関数変数 ----------
	//csv読み込みデータ格納用二次元配列
	private string[,] sData;
	private string[] fields;

	//読み込めたか確認の表示用の変数
	public static int height = 0;    //行数

	//public string line = "";

	private void csvLoad(string path,ref string [ , ] sdata)
	{
		TextAsset csv = Resources.Load (path) as TextAsset;
		StringReader sr = new StringReader (csv.text);
		// ストリームリーダーをstringに変換
		string strStream    = sr.ReadToEnd( );
		// StringSplitOptionを設定(要はカンマとカンマに何もなかったら格納しないことにする)
		System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;
		// カンマ分けの準備(区分けする文字を設定する)
		char [ ] spliter = new char [1] { ',' };
        
        //行に分ける
        string [ ] lines = strStream.Split(new char [ ] { '\r', '\n' }, option);

        // 行数設定
        int h = lines.Length;
        //int h = 32;
        // 列数設定
        int w = lines[0].Split(spliter, option).Length;
        //int w = 5;
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
		height = h;    //行数   

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
                    MinCunt = null,
					GoldCunt = null,
					SilverCunt = null,
					BronzeCunt = null,
                    ClearFlag = null,
                    Pos_X = null,
                    Pos_Y = null,
                    Pos_Z = null,
                    Rot_X=null,
                    Rot_Y=null,
                    Rot_Z=null,
				});
			} else {
               
                StageDateList.Add(new StageDate()
                {
                    StageID = int.Parse (arrays [i, 0]),
                    StageName = arrays [i, 1],
                    UpperCunt = int.Parse (arrays [i, 2]),
					MinCunt = int.Parse(arrays[i, 3]),
					GoldCunt = int.Parse(arrays[i, 4]),
					SilverCunt = int.Parse(arrays[i, 5]),
					BronzeCunt = int.Parse(arrays[i, 6]),
                    ClearFlag = int.Parse (arrays [i, 7]),
                    Pos_X = float.Parse(arrays[i, 8]),
                    Pos_Y = float.Parse(arrays[i, 9]),
                    Pos_Z = float.Parse(arrays[i, 10]),
                    Rot_X = float.Parse(arrays[i, 11]),
                    Rot_Y = float.Parse(arrays[i, 12]),
                    Rot_Z = float.Parse(arrays[i, 13]), 
                    
				});
                CSVData.GetData().Add(new CSVData.StageDate()
                {
						StageID = int.Parse (arrays [i, 0]),
						StageName = arrays [i, 1],
						UpperCunt = int.Parse (arrays [i, 2]),
						MinCunt = int.Parse(arrays[i, 3]),
						GoldCunt = int.Parse(arrays[i, 4]),
						SilverCunt = int.Parse(arrays[i, 5]),
						BronzeCunt = int.Parse(arrays[i, 6]),
						ClearFlag = int.Parse (arrays [i, 7]),
						Pos_X = float.Parse(arrays[i, 8]),
						Pos_Y = float.Parse(arrays[i, 9]),
						Pos_Z = float.Parse(arrays[i, 10]),
						Rot_X = float.Parse(arrays[i, 11]),
						Rot_Y = float.Parse(arrays[i, 12]),
						Rot_Z = float.Parse(arrays[i, 13]),
                });
            }
		}

       

    }
    private void Awake()
    {
        csvLoad(StageDatePath, ref this.sData);
        charDataConvert(this.sData, height);
    }
    // Use this for initialization
    void Start () {

	}



	// Update is called once per frame
	void Update () {
	
	}
}
