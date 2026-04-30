using UnityEngine;

public class selectbuttonmanager : MonoBehaviour
{
    private selectbutton[] buttonlist = null;
    void Start()
    {
        buttonlist = GetComponentsInChildren<selectbutton>();
        StageDatas datas = DataHandler.instance_.stageDatas_;
        for (int i = 0; i < datas.stageDataList.Length; i++)
        {
            buttonlist[i].Initialize(datas.stageDataList[i]);
        }
    }

}
