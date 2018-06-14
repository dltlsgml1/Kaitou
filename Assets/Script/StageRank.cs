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

        int clearCnt = (int)CSVData.StageDateList[id].ClearFlag;
        int minCnt = (int)CSVData.StageDateList[id].MinCunt;
        int upperCnt = (int)CSVData.StageDateList[id].UpperCunt;
        float minTmp = clearCnt - minCnt;
        float limTmp = (upperCnt - minCnt) / 2;
        
        if(minTmp < 0)
        {
            sts = RANK.NORMAL;
        }
        else if(minTmp == 0)
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
}
