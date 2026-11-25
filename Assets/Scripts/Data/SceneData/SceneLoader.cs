using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Serializable]
    private struct InsertScenePair //シーン挿入の設定クラス
    {
        public int NextSceneId;    //従来の遷移先のシーンID
        public int InsertSceneId;  //挿入するシーンのID
    }

    private static SceneLoader _instance = null;    //シングルトンインスタンス
    public static SceneLoader Instance => _instance;

    [SerializeField] private SceneCatelog _sceneCatalog = null; //シーンカタログ
    [SerializeField] private InsertScenePair[] _insertScenePairs; //シーンを挿入するための設定項目

    private int _currentSceneId = INVALID_SCENE_ID;           //現在のシーンID
    private int _sceneIdAfterInserted = INVALID_SCENE_ID;     //シーン挿入後の次のシーンID
    private const int INVALID_SCENE_ID = -1;
    public enum LoadType    //ロードの種類
    {
        Initial,     //初期シーンをロード
        StageSelect, //選択シーンをロード
        Next,        //次のシーンをロード
        Reload       //リロード

    }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Assert.IsNotNull(_sceneCatalog);
    }

    //現在のシーンIDを設定
    //これをもとにシーンロード要求を処理
    public void SetSceneId(int sceneId)
    {
        if(_sceneCatalog.GetSceneName(sceneId) == null)
        {
            Debug.LogError("存在しないシーンのIDはSceneLoaderに登録できません");
            return;
        }
        _currentSceneId = sceneId;
    }

    //ロードタイプごとの遷移を制御
    public void LoadScene(LoadType loadType)
    {
        if (_sceneIdAfterInserted != INVALID_SCENE_ID)
        {
            AfterInsertLoad();
            return;
        }

        int sceneId = INVALID_SCENE_ID;     //遷移先ID(インサートチェック前)
        int nextSceneId = INVALID_SCENE_ID; //遷移先ID(インサートチェック後)

        switch (loadType)
        {
            case LoadType.Initial:
                sceneId = _sceneCatalog.GetInitialSceneId;
                nextSceneId = InsertCheck(sceneId);

                break;

            case LoadType.StageSelect:
                sceneId = _sceneCatalog.GetSelectSceneId;
                nextSceneId = InsertCheck(sceneId);

                break;

            case LoadType.Next:
                sceneId = _sceneCatalog.GetNextSceneId(_currentSceneId);
                nextSceneId = InsertCheck(sceneId);

                break;

            case LoadType.Reload:
                nextSceneId = _currentSceneId;
                break;
        }

        LoadSceneById(nextSceneId);

    }

    //特定のシーンを指定して遷移(ステージ選択シーンでのみ利用可能)
    public void SelectLoad(int sceneId)
    {
        if(_currentSceneId != _sceneCatalog.GetSelectSceneId)
        {
            Debug.LogError("選択シーン以外で、指定型ロードは使用できません");
            return;
        }

        int nextSceneId = InsertCheck(sceneId);
        LoadSceneById(nextSceneId);
    }

    //ロードしたいシーンがビルド内に含まれているかのチェック
    private bool SceneExists(string sceneName)
    {
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);

        if (sceneIndex == -1)
        {
            Debug.LogError(sceneName + "：該当のシーンがビルドパス上に存在しません。");
            return false;
        }
        else
            return true;
    }

    //挿入が必要かチェックし、必要なら遷移先を保存して
    //遷移先を挿入シーンに変更
    private int InsertCheck(int sceneId)
    {
        //指定のシーンの前に挿入が必要かどうかをチェック
        foreach (var scenePair in _insertScenePairs)
        {
            if (scenePair.NextSceneId == sceneId)
            {
                _sceneIdAfterInserted = sceneId; //挿入した後のシーンIDを保持しておく
                return scenePair.InsertSceneId;  //挿入シーンのIDを遷移先として返す
            }
        }

        return sceneId;
    }

    //挿入したシーンから通常のシーケンスに戻る
    private void AfterInsertLoad()
    {
        if(LoadSceneById(_sceneIdAfterInserted))
        {
            _sceneIdAfterInserted = INVALID_SCENE_ID; //遷移が成功したらリセット
        }
    }

    //IDを受け取り、シーンをロード
    //成功か失敗かを返す
    private bool LoadSceneById(int sceneId)
    {
        string sceneName = _sceneCatalog.GetSceneName(sceneId);
        if (SceneExists(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            return true;
        }
        else
        {
            return false;
        }

    }
}
