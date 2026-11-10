using UnityEngine.Assertions;
using UnityEngine;


//ゲーム全体を管理するクラス
//ゲーム全体のエントリポイント
public class AppManager : MonoBehaviour
{
    [SerializeField] MasterDataProvider _masterDataProvider;
    [SerializeField] SaveDataManager _saveDataManager;
    [SerializeField] AudioManager _audioManager;

    [SerializeField] string _stageDatasFileName = "stageDatas.json";
    [SerializeField] string _optionDataFileName = "optionData.json";

    [SerializeField] private string _sceneControllerTag = null;


    private static AppManager _instance = null;
    public static AppManager Instance => _instance;

    public void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //必要なコンポーネントの確認
        Assert.IsNotNull(_masterDataProvider);
        Assert.IsNotNull(_saveDataManager);
        Assert.IsNotNull(_audioManager);
        Assert.IsNotNull(_sceneControllerTag);
    }

    public void Start()
    {
        //ゲーム全体の初期化(マネージャー間の通知設定もここでやる)
        _saveDataManager.Initialize(_masterDataProvider.GetStageNum(),
                                    _stageDatasFileName,
                                    _optionDataFileName,
                                    GetParser());
        _audioManager.Initialize(!_saveDataManager.IsSoundActive());

        //シーンコントローラーを取得
        GameObject obj = GameObject.FindGameObjectWithTag(_sceneControllerTag);
        if (obj == null)
        {
            Debug.LogError("シーンコントローラータグが設定されたオブジェクトが存在しません");
            return;
        }

        var sceneController = obj.GetComponent<ISceneController>();
        if(sceneController == null)
        {
            Debug.LogError("シーンコントローラークラスがアタッチされていません");
            return;
        }

        sceneController.Boot();
    }

    //Json形式で保存するパーサーを生成して返す
    private IParser GetParser()
    {
        return new JsonParser();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        // Unityエディター上で再生を停止する場合
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // ビルドした実機（Windows, Mac, Android等）で終了する場合
            Application.Quit();
#endif

    }
}
