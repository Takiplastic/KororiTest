using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    private SaveDataManager _instance = null;   //シングルトンインスタンス

    private StageData_new[] _stageDatas;        //各ステージのセーブデータ

    private bool _isSoundEnable;                //サウンド有効化フラグ

    public SaveDataManager Instance() => _instance;
    public bool IsSoundEnable() => _isSoundEnable;

    private JsonSaveSystem _saveSystem = null;      //外部への保存・読み取り担当

    private void Awake()
    {
        //シングルトン処理
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _stageDatas = null;
        _saveSystem = new JsonSaveSystem();
    }

    private void Start()
    {
        Test();
    }

    //ゲーム開始時にデータをロード(外部からステージ数を受け取る)
    public void Initialize(int stageNum)
    {
        if (_stageDatas != null) return; //既に初期化されているなら何もしない

        
        if (!LoadData())                   //セーブデータを取得
            CreateInitialData(stageNum);   //失敗したら初期データを作成
    }

    //セーブデータをの初期生成
    private void CreateInitialData(int stageNum)
    {
        //ステージのデータは与えられたステージ数の分だけ作成
        _stageDatas = new StageData_new[stageNum];

        //ステージIDは"1"始まりで初期化
        for (int i = 0; i < stageNum; i++)
        {
            //ステージ1のみ解放
            if (i == 0)
                _stageDatas[i] = new StageData_new(i + 1, true, 0);

            _stageDatas[i] = new StageData_new(i + 1, false, 0);
        }

        //サウンド初期設定は"有効"
        _isSoundEnable = true;

        //ファイルへ保存
        _saveSystem.Save<StageData_new[]>(_stageDatas);
        _saveSystem.Save<bool>(_isSoundEnable);
    }

    //ステージデータと設定データをロード
    private bool LoadData()
    {
        try
        {
            _stageDatas = _saveSystem.Load<StageData_new[]>();
            _isSoundEnable = _saveSystem.Load<bool>();
        }
        catch (FileNotFoundException ex)
        {
            return false;
        }

        return true;
    }

    //ステージデータの更新
    public void UpdateStageData(int id, bool isUnLocked, int score)
    {
        _stageDatas[id - 1] = new StageData_new(id, isUnLocked, score);
        _saveSystem.Save<StageData_new[]>(_stageDatas);
    }

    //サウンド設定の更新
    public void UpdateIsSoundEnable(bool newFlag)
    {
        if (_isSoundEnable == newFlag) return;

        _isSoundEnable = newFlag;
        _saveSystem.Save<bool>(_isSoundEnable);
    }

    //ステージデータを外部に渡す
    public StageData_new GetStageData(int stageId)
    {
        return _stageDatas[stageId];
    }

    //サウンド有効フラグを外部に渡す
    public bool GetIsSoundEnable()
    {
        return _isSoundEnable;
    }

    //データの削除
    public void DeleteSaveData()
    {
        _saveSystem.DeleteFile();
    }

    public void Test()
    {
        Initialize(30);
        UpdateIsSoundEnable(false);
        UpdateStageData(1, true, 3);
        UpdateStageData(2, true, 0);
        UpdateStageData(3, true, 1);
    }
}
