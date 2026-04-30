using System.IO;
using UnityEngine;

//起動時にデータを読み込む
//データ更新時にJson形式で保存

public class DataHandler : MonoBehaviour
{
    public static DataHandler instance_ = null;
    private bool hasInitLoaded = false;

    public OptionData optionData_ = null;
    public StageDatas stageDatas_ = null;

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
        DeleteAllData();
        if (hasInitLoaded) return;

        if (!LoadOptioinData())
            CreateInitOptionData();
        if (!LoadStageDatas())
            CreateInitStageDatas();
    }

    //オプションデータにはBGMを有効にするかどうかのみ保存
    public bool LoadOptioinData()
    {
        string datastr = string.Empty; ;
        string pathstr = Application.persistentDataPath + "/optionData.json";
        if (!File.Exists(pathstr))
            return false;

        StreamReader reader = null; 
        reader = new StreamReader(pathstr);
        datastr = reader.ReadToEnd();
        reader.Close();
        optionData_ = JsonUtility.FromJson<OptionData>(datastr);

        return true;
    }

    //オプションデータを保存
    public void SaveOptionData()
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(optionData_);

        writer = new StreamWriter(Application.persistentDataPath + "/optionData.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    public void CreateInitOptionData()
    {
        optionData_ = new OptionData();
        SaveOptionData();
    }

    public bool LoadStageDatas()
    {
        string datastr = string.Empty;
        string pathstr = Application.persistentDataPath + "/stageDatas.json";
        if (!File.Exists(pathstr))
            return false;

        StreamReader reader;
        reader = new StreamReader(pathstr);
        if (reader == null)
            return false;

        datastr = reader.ReadToEnd();
        reader.Close();
        stageDatas_ = JsonUtility.FromJson<StageDatas>(datastr);

        return true;
    }

    public void SaveStageDatas(StageDatas stageDatas)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(stageDatas);

        writer = new StreamWriter(Application.persistentDataPath + "/stageDatas.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    public void CreateInitStageDatas()
    {
        int NUM_STAGE = 20;
        stageDatas_ = new StageDatas();
        stageDatas_.stageDataList = new StageData[NUM_STAGE];
        for (int i = 0; i < NUM_STAGE; i++)
        {
            stageDatas_.stageDataList[i] = new StageData(i + 1, true, Mathf.Min(i, 3));
        }
        stageDatas_.stageDataList[0].hasUnLocked_ = true;
    }

    public void DeleteAllData()
    {
        string optionpath = Application.persistentDataPath + "optionData.json";
        if (File.Exists(optionpath))
        {
            File.Delete(optionpath);
        }
        string stagedataspath = Application.persistentDataPath + "stageDatas.json";
        if (File.Exists(stagedataspath))
        {
            File.Delete(stagedataspath);
        }
    }
}
