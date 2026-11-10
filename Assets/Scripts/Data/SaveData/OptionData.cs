using System;
using UnityEngine;

[Serializable]
public class OptionData
{
    public bool ActivateBGM = true;

    public OptionData()
    {
        ActivateBGM = true;
    }

    public OptionData(bool activateBGM)
    {
        ActivateBGM = activateBGM;
    }
}
