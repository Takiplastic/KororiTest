using System;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

public class JsonSaveSystem
{
    private const string STAGEDATA_FILE_NAME = "/stageDatas.json";  //ステージデータファイル名
    private const string OPTIONDATA_FILE_NAME = "/optionData.json"; //オプションデータファイル名

    private string _stageDataPath;   //ステージデータパス
    private string _optionDataPath;  //セーブデータパス

    [System.Serializable]
    private class JsonArrayWrapper<T>
    {
        public T[] data;
    }

    public JsonSaveSystem()
    {
        _stageDataPath = Application.persistentDataPath + STAGEDATA_FILE_NAME;
        _optionDataPath = Application.persistentDataPath + OPTIONDATA_FILE_NAME;
    }

    //Json形式で保存されたデータを読み込み、外部へ渡す
    public DataType Load<DataType>()
    {
        string pathstr = null;

        if (typeof(DataType) == typeof(StageData_new[]))
        {
            pathstr = _stageDataPath;
        }
        else if (typeof(DataType) == typeof(bool))
        {
            pathstr = _optionDataPath;
        }
        else
        {
            Assert.IsTrue(false);
            return default(DataType);
        }

        string json = File.ReadAllText(pathstr);

        if (typeof(DataType).IsArray)
        {
            Type elementType = typeof(DataType).GetElementType();
            Type wrapperType = typeof(JsonArrayWrapper<>).MakeGenericType(elementType);

            // JsonUtility.FromJson は実行時型を受け取れる
            string wrappedJson = "{\"data\":" + json + "}";
            object wrapper = JsonUtility.FromJson(wrappedJson, wrapperType);

            object array = wrapperType.GetField("data").GetValue(wrapper);
            return (DataType)array;
        }

        return JsonUtility.FromJson<DataType>(json);
    }

    //内部データJson形式で保存する
    public void Save<DataType>(DataType data)
    {
        string pathstr = string.Empty;

        // データタイプによって適切なファイルパスを設定
        if (typeof(DataType) == typeof(StageData_new[]))
        {
            pathstr = _stageDataPath;
        }
        else if (typeof(DataType) == typeof(bool))
        {
            pathstr = _optionDataPath;
        }
        else
        {
            Assert.IsTrue(false);
            return;
        }

        string jsonstr;

        // 配列の場合はラッパーに格納してからJSON化
        if (typeof(DataType).IsArray)
        {
            Type elementType = typeof(DataType).GetElementType();

            Type wrapperType = typeof(JsonArrayWrapper<>)
                .MakeGenericType(elementType);

            object wrapper = Activator.CreateInstance(wrapperType);
            wrapperType.GetField("data").SetValue(wrapper, data);

            jsonstr = JsonUtility.ToJson(wrapper);
        }
        else
        {
            jsonstr = JsonUtility.ToJson(data);
        }

        // ファイルに書き込む
        using (StreamWriter writer = new StreamWriter(pathstr, false))
        {
            writer.Write(jsonstr);
        }
    }

    //セーブファイルをすべて削除
    public void DeleteFile()
    {
        if (File.Exists(_stageDataPath))
            File.Delete(_stageDataPath);

        if (File.Exists(_optionDataPath))
            File.Delete(_optionDataPath);
    }
}
