using System;
using System.Xml.Serialization;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public void Initialize(int stageNum, string stageDatasFileName, string optionDataFileName, IParser parser)
    {
        Debug.Log("Called: SaveDataManager.Initialize");
    }

    public bool IsSoundActive()
    {
        return true;
    }
}
