using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneCatalog", menuName = "Scene/SceneCatalog")]
public class SceneCatelog : ScriptableObject
{
    [SerializeField] List<SceneData> _sceneList = null; //インスペクタ上で設定する用のシーンデータリスト
    [SerializeField] int _initialSceneId = 0;           //初期シーンのID
    [SerializeField] int _selectSceneId = 1;            //選択シーンのID

    private Dictionary<int, SceneData> _sceneDict = null; //シーン辞書(プログラム内部ではこの辞書を利用する)

    public void OnEnable()
    {
        Debug.Log("Called: SceneCatalog.OnEnable");
        //内部で高速アクセスを行うため、辞書に変換
        _sceneDict = new Dictionary<int, SceneData>();
        foreach (var sceneData in _sceneList)
        {
            _sceneDict.Add(sceneData.SceneId, sceneData);
        }
    }

    //シーンデータを辞書から探索
    //ない場合は、エラーメッセージを出力してnullを返す
    private SceneData GetSceneData(int sceneId)
    {
        SceneData data = null;
        if (!_sceneDict.TryGetValue(sceneId, out data))
            Debug.LogError("渡されたSceneIdに該当するシーンが存在しません。SceneId：" + sceneId);

        return data;
    }

    //シーン名を取得
    public string GetSceneName(int sceneId)
    {
        var sceneData = GetSceneData(sceneId);

        if (sceneData == null) return null;
        else return sceneData.SceneName;
    }

    //次のシーンIDを取得
    public int GetNextSceneId(int sceneId)
    {
        var sceneData = GetSceneData(sceneId);
        if (sceneData != null)
            return sceneData.NextSceneId;
        else
            return -1;
    }

    //次のシーン名を取得
    public string GetNextSceneName(int sceneId)
    {
        var sceneData = GetSceneData(sceneId);
        if (sceneData != null)
            return GetSceneName(sceneData.NextSceneId);
        else
            return null;
    }

    //初期シーンIDを取得
    public int GetInitialSceneId => _initialSceneId;

    //初期シーン名を取得
    public string GetInitialSceneName()
    {
        return GetSceneName(_initialSceneId);
    }

    //選択シーンIDを取得
    public int GetSelectSceneId => _selectSceneId;

    //選択シーン名を取得
    public string GetSelectSceneName()
    {
        return GetSceneName(_selectSceneId);
    }
}
