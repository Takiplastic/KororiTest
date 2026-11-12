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

    private int _currentSceneId = -1;           //現在のシーンID
    private int _sceneIdAfterInserted = -1;     //シーン挿入後の次のシーンID

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
        if (_sceneIdAfterInserted != -1)
        {
            AfterInsertLoad();
            return;
        }   

        string sceneName = null; //遷移先のシーン名

        switch (loadType)
        {
            case LoadType.Initial:
                sceneName = _sceneCatalog.GetInitialSceneName();
                break;

            case LoadType.StageSelect:
                sceneName = _sceneCatalog.GetSelectSceneName();
                break;

            case LoadType.Next:
                //シーン挿入が必要かを判定
                var nextSceneId = _sceneCatalog.GetNextSceneId(_currentSceneId);
                if (IsInsertTarget(nextSceneId))
                {
                    InsertScene(nextSceneId);
                    return;
                }
                sceneName = _sceneCatalog.GetNextSceneName(_currentSceneId);
                break;

            case LoadType.Reload:
                sceneName = _sceneCatalog.GetSceneName(_currentSceneId);
                break;
        }

        //シーンがビルドに含まれているかを確認してからロード
        //ビルドに含まれていない場合はロードしないでエラーログ出力
        if (SceneExists(sceneName))
            SceneManager.LoadScene(sceneName);
    }

    //特定のシーンを指定して遷移(ステージ選択シーンでのみ利用可能)
    public void SelectLoad(int sceneId)
    {
        if(_currentSceneId != _sceneCatalog.GetSelectSceneId)
        {
            Debug.LogError("選択シーン以外で、指定型ロードは使用できません");
            return;
        }
        
        //シーン挿入が必要かを判定
        if(IsInsertTarget(sceneId))
        {
            InsertScene(sceneId);
            return;
        }

        string sceneName = _sceneCatalog.GetSceneName(sceneId);
        if (SceneExists(sceneName))
            SceneManager.LoadScene(sceneName);
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

    //指定のシーンの前に挿入が必要かどうかをチェック
    private bool IsInsertTarget(int sceneId)
    {
        foreach(var scenePair in _insertScenePairs)
        {
            if (scenePair.NextSceneId == sceneId)
            {
                return true;
            }
                
        }
        return false;
    }

    //通常のシーケンスに追加のシーンを挿入
    private void InsertScene(int sceneId)
    {
        int insertSceneId = -1;

        //対応するシーンペアが存在することはIsInsertTargetで確約済み
        foreach (var scenePair in _insertScenePairs)
        {
            if (scenePair.NextSceneId == sceneId)
            {
                insertSceneId = scenePair.InsertSceneId;
            }
        }

        _sceneIdAfterInserted = sceneId; //挿入した後のシーンIDを保持しておく
        string sceneName = _sceneCatalog.GetSceneName(insertSceneId);
        if (SceneExists(sceneName))
            SceneManager.LoadScene(sceneName);
    }

    //挿入したシーンから通常のシーケンスに戻る
    private void AfterInsertLoad()
    {
        string sceneName = _sceneCatalog.GetSceneName(_sceneIdAfterInserted);
        _sceneIdAfterInserted = -1; //挿入後のシーンIDを初期化
        if (SceneExists(sceneName))
            SceneManager.LoadScene(sceneName);
    }
}
