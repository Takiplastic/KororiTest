using System;
using UnityEngine;

[Serializable]
public class StageDatas
{
    public StageData[] stageDataList;

    public void ShowParams()
    {
        foreach (var stageData in stageDataList)
            stageData.ShowParams();    
    }
}

[Serializable]
public class StageData
{
    public int stageId_ = -1;
    public bool hasUnLocked_ = false;
    public int score_ = -1;

    public StageData(int id, bool hasUnLocked, int score)
    {
        stageId_ = id;
        hasUnLocked_ = hasUnLocked;
        score_ = score;
    }

    public void ShowParams()
    {
        Debug.Log("StageID:" + stageId_);
        Debug.Log("hasUnLocked:" + hasUnLocked_);
        Debug.Log("score:" + score_);
    }
}

