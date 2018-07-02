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
        public Nullable<int> MinCunt;      //CSVの最小Clear回数部分
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

   

}
