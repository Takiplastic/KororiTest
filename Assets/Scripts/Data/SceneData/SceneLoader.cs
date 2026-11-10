using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance = null;

    public static SceneLoader Instance => _instance;

    [SerializeField] private SceneCatelog _sceneCatalog = null; //シーンカタログ
    [SerializeField] private int[] _openingInsertSceneId = null;//オープニングを差し込むシーンのID

    private int _currentSceneId = -1;           //現在のシーンID

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

        Assert.IsNotNull(_openingInsertSceneId);
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

}
