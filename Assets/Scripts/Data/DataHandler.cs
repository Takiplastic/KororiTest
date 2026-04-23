using System.IO;
using UnityEngine;

//起動時にデータを読み込む
//データ更新時にJson形式で保存

public class DataHandler : MonoBehaviour
{
    private static DataHandler instance_ = null;
    private bool hasInitLoaded = false;

    private void Awake()
    {
        if (!instance_)
        {
            instance_ = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        OptionData optionData = new OptionData();
        optionData.ActivateBGM = false;
        SaveOptionData(optionData);

        OptionData loadedOptionData = LoadOptioinData();

        Debug.Log(loadedOptionData.ActivateBGM);

        StageDatas stageDatas = new StageDatas();
        stageDatas.stageDataList = new StageData[3];
        stageDatas.stageDataList[0] = new StageData(1, true, 0);
        stageDatas.stageDataList[1] = new StageData(2, true, 0);
        stageDatas.stageDataList[2] = new StageData(3, true, 0);
        SaveStageDatas(stageDatas);

        StageDatas loadedStageDatas = LoadStageDatas();
        loadedStageDatas.ShowParams();

    }

    //オプションデータにはBGMを有効にするかどうかのみ保存
    public OptionData LoadOptioinData()
    {
        string datastr = string.Empty; ;
        StreamReader reader;
        reader = new StreamReader(Application.dataPath + "/optionData.json");
        datastr = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<OptionData>(datastr);
    }

    //オプションデータを保存
    public void SaveOptionData(OptionData opitonData)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(opitonData);

        writer = new StreamWriter(Application.dataPath + "/optionData.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    public StageDatas LoadStageDatas()
    {
        string datastr = string.Empty; ;
        StreamReader reader;
        reader = new StreamReader(Application.dataPath + "/stageDatas.json");
        datastr = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<StageDatas>(datastr);
    }

    public void SaveStageDatas(StageDatas stageDatas)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(stageDatas);

        writer = new StreamWriter(Application.dataPath + "/stageDatas.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }
}
