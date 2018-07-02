using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class ExportCsvScript : MonoBehaviour
{
    private int[] ClearNum;     // クリア手数
    private int MaxData;        // 要素数

    private string FileName = "SaveData.csv";   // セーブデータのファイル名
    private bool CreateFileFlg = false;

    private int NowStageId;     // 現在のステージ番号

    // 初期化
    public void Init(int max)
    {
        ClearNum = new int[max];
        MaxData = max;
        Cursor.visible = false;
        // ファイルがなければ作る
        CreateSaveFile();

        // ファイルを元にデータの初期化
        ReadFile();
    }

    // 現在の利用しているステージID
    public void SetStageId(int id)
    {
        NowStageId = id;
    }

    public int GetNowStageId()
    {
        return NowStageId;
    }

    // クリア手数のセット
    public void SetClearData(int clearnum = 100)
    {
        ClearNum[NowStageId] = clearnum;
    }

    private void SetClearData(int id, int clearnum = 100)
    {
        ClearNum[id] = clearnum;
    }


    // クリア手数の取得
    public int GetClearData(int id)
    {
        return ClearNum[id];
    }

    // データ要素数の取得
    public int GetMaxData()
    {
        return MaxData;
    }

    // セーブデータ作成(データが無い場合のみ)
    private void CreateSaveFile()
    {
        string path = Application.dataPath + "/" + FileName;

        // ファイルがなければ作る
        if (System.IO.File.Exists(path) == false)
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("UTF-8"));
            // ヘッダー出力
            string[] s1 = { "ステージ番号", "クリア回数" };
            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);
            // データ出力
            for (int i = 0; i < MaxData; i++)
            {
                string[] str = { "Stage" + IdToString(i), "" + 100 };
                string str2 = string.Join(",", str);
                sw.WriteLine(str2);
            }
            // StreamWriterを閉じる
            sw.Close();

        }
        CreateFileFlg = true;
    }

    // ファイルの読み込みとデータのセット
    private void ReadFile()
    {
        // ファイル読み込み
        // 引数説明：第1引数→ファイル読込先, 第2引数→エンコード
        StreamReader sr = new StreamReader(Application.dataPath + "/" + FileName, Encoding.GetEncoding("UTF-8"));
        string line;

        int idx;
        string id, clear;
        int idData, clearData;
        int cnt = 0;

        // 行がnullじゃない間(つまり次の行がある場合は)、処理をする
        while ((line = sr.ReadLine()) != null)
        {
            idx = line.IndexOf(",");
            clear = line.Substring(idx + 1);
            id = line.Substring(5, 2);

            // セーブデータがデータ最大数を超えた場合の例外処理
            cnt++;
            if (cnt > MaxData + 1)
                break;

            if(clear != "クリア回数")
            {

                // 文字列をintにキャスト
                clearData = Int32.Parse(clear);
                idData = Int32.Parse(id);

                // コンソールに出力
                //Debug.Log("id:" + id + "," + clear);
                //Debug.Log("id:" + idData + "," + clearData);

                SetClearData(idData, clearData);
                
            }

        }
        // StreamReaderを閉じる
        sr.Close();

    }

    // ファイル書き出し
    public void WriteFile()
    {
        // ファイル書き出し
        // 現在のフォルダにsaveData.csvを出力する(決まった場所に出力したい場合は絶対パスを指定してください)
        // 引数説明：第1引数→ファイル出力先, 第2引数→ファイルに追記(true)or上書き(false), 第3引数→エンコード
        StreamWriter sw = new StreamWriter(Application.dataPath + "/" + FileName, false, Encoding.GetEncoding("UTF-8"));
        // ヘッダー出力
        string[] s1 = { "ステージ番号", "クリア回数" };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
        // データ出力
        for (int i = 0; i < MaxData; i++)
        {
            string[] str = { "Stage"+ IdToString(i), "" + ClearNum[i] };
            string str2 = string.Join(",", str);
            sw.WriteLine(str2);
        }
        // StreamWriterを閉じる
        sw.Close();

    }

    // ファイル削除
    public void DeleteFile()
    {
        string path = Application.dataPath + "/" + FileName;
        if (System.IO.File.Exists(path) == true)
        {
            System.IO.File.Delete(path);
        }
    }

    private string IdToString(int id)
    {
        string str;

        if (id >= 0 && id <= 9)
        {
            str = "0" + id;
        }
        else
        {
            str = "" + id;
        }

        return str;
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        if (CreateFileFlg)
        {
            WriteFile();
        }
    }


}