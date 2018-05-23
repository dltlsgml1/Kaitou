using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CSV読み込み処理
/// </summary>
public class CsvReader : MonoBehaviour
{

    private static CsvReader _instance = null;

    public static CsvReader Instance
    {
        get
        {
            if (_instance == null)
            {
                // 画面上に対象オブジェクトがなかった場合新規に作成
                if (_instance == null)
                {
                    _instance = new GameObject("CsvReader").AddComponent<CsvReader>();
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// 改行コード
    /// </summary>
    private const char COLUMN_BREAKE_CODE = ',';

    /// <summary>
    /// 拡張子
    /// </summary>
    private const string EXTENSION = ".csv";

    /// <summary>
    /// CSV読み込み処理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public List<T> Read<T>(string path, string fileName) where T : new()
    {

        // 値を返す用のリスト
        List<T> returnList = new List<T>();

        try
        {

            StreamReader streamReader = new StreamReader(path + "/" + fileName + EXTENSION);

            List<string> headerInfo = new List<string>();

            // ストリームの末尾まで繰り返す
            int lineIndex = 0;
            while (!streamReader.EndOfStream)
            {
                // ファイルから一行読み込む
                var line = streamReader.ReadLine();

                // 読み込んだ一行をカンマ毎に分けて配列に格納する
                var values = line.Split(',');

                // ヘッダー情報を格納
                if (lineIndex == 0)
                {
                    foreach (string value in values)
                    {
                        headerInfo.Add(value);
                    }
                }
                else
                {
                    T data = new T();
                    object paramObj = data;

                    for (int columnIndex = 0; columnIndex < values.Length; columnIndex++)
                    {
                        FieldInfo classData = data.GetType().GetField(headerInfo[columnIndex]);
                        classData.SetValue(paramObj, Convert.ChangeType(values[columnIndex], classData.FieldType));
                    }
                    returnList.Add((T)paramObj);
                }

                lineIndex++;
            }
        }
        catch
        {

        }

        return returnList;
    }
}