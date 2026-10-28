using UnityEngine;

[System.Serializable]
public class StageData_new
{
    [SerializeField] int _stageId;       //ステージID、初期値は"1"
    [SerializeField] bool _isUnLocked;   //解放済みかどうか
    [SerializeField] int _score;         //獲得済みスコア

    public int StageId => _stageId;
    public bool IsUnLocked => _isUnLocked;
    public int Score => _score;

    //セーブデータを変更したい場合はこのコンストラクタを使用
    public StageData_new(int id, bool isUnLocked, int score) {
        _stageId = id;
        _isUnLocked = isUnLocked;
        _score = score;
    }

    public void ShowParams()
    {
        Debug.Log("StageID:" + _stageId);
        Debug.Log("hasUnLocked:" + _isUnLocked);
        Debug.Log("score:" + _score);
    }
}
