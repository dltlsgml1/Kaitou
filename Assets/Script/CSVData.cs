using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CSVData {
    public struct StageDate
    {
        public Nullable<int> StageID;       //CSVのステージID部分
        public string StageName;             //CSVのstagename部分
        public Nullable<int> UpperCunt;      //CSVの上限回数部分
        public Nullable<int> ClearFlag;      //CSVのクリアチェック部分
        public Nullable<float> Pos_X;        //カメラのポジションX
        public Nullable<float> Pos_Y;        //カメラのポジションY
        public Nullable<float> Pos_Z;        //カメラのポジションZ
        public Nullable<float> Rot_X;        //カメラの角度X
        public Nullable<float> Rot_Y;        //カメラの角度Y
        public Nullable<float> Rot_Z;        //カメラの角度Z
        public string StageTitle;             //CSVのTitle部分
    }
    public static List<StageDate> StageDateList = new List<StageDate>();

    public static List<StageDate> GetData()
    {
        return StageDateList;
    }

    public static void test()
    {
        for (int i = 0; i < 31; i++)
        {
            Debug.Log(i + ":" + "StageID/" + StageDateList[i].StageID);
            Debug.Log(i + ":" + "StageName/" + StageDateList[i].StageName);
            Debug.Log(i + ":" + "UpperCunt/" + StageDateList[i].UpperCunt);
            Debug.Log(i + ":" + "Pos_X/" + StageDateList[i].Pos_X);
            Debug.Log(i + ":" + "Pos_Y/" + StageDateList[i].Pos_Y);
            Debug.Log(i + ":" + "Pos_Z/" + StageDateList[i].Pos_Z);
            Debug.Log(i + ":" + "Rot_X/" + StageDateList[i].Rot_X);
            Debug.Log(i + ":" + "Rot_Y/" + StageDateList[i].Rot_Y);
            Debug.Log(i + ":" + "Rot_Z/" + StageDateList[i].Rot_Z);
            Debug.Log(i + ":" + "StageTitle/" + StageDateList[i].StageTitle);
        }
    }

}
