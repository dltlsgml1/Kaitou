using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRank : MonoBehaviour {

    private RANK Rank;

    public enum RANK{
        NORMAL = 0,
        BRONZE,
        SILVER,
        GOLD,
    }

    public void CheckRank(int id)
    {
        RANK sts;

        int clearCnt = GameObject.Find("SaveData").GetComponent<ExportCsvScript>().GetClearData(id);
        int minCnt = (int)CSVData.StageDateList[id].MinCunt;

        int goldRank = (int)CSVData.StageDateList[id].GoldCunt;
        int silverRank = (int)CSVData.StageDateList[id].SilverCunt;
        int bronzeRank = (int)CSVData.StageDateList[id].BronzeCunt;



        if (minCnt < clearCnt)
        {
            sts = RANK.NORMAL;
        }
        else if (clearCnt <= goldRank)
        {
            sts = RANK.GOLD;
        }
        else if (clearCnt <= silverRank)
        {
            sts = RANK.SILVER;
        }
        else
        {
            sts = RANK.BRONZE;
        }

        SetRank(sts);     
    }

    private void SetRank(RANK rank)
    {
        Rank = rank;
    }

    public RANK GetRank()
    {
        return Rank;
    }

    public RANK CheckRank_old(int id)
    {
        RANK sts;

        int clearCnt = GameObject.Find("SaveData").GetComponent<ExportCsvScript>().GetClearData(id);
        int minCnt = (int)CSVData.StageDateList[id].MinCunt;
        int upperCnt = (int)CSVData.StageDateList[id].UpperCunt;
        float minTmp = clearCnt - minCnt;
        float limTmp = (upperCnt - minCnt) / 2;

        if (minTmp < 0)
        {
            sts = RANK.NORMAL;
        }
        else if (minTmp == 0)
        {
            sts = RANK.GOLD;
        }
        else if (minTmp <= limTmp && clearCnt != upperCnt)
        {
            sts = RANK.SILVER;
        }
        else
        {
            sts = RANK.BRONZE;
        }

        return sts;
    }
}
