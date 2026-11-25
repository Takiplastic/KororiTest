using UnityEngine;
using UnityEngine.Assertions;

//シーンを管理するクラスの抽象テンプレートクラス
//派生クラスでは、具体的なステートとその制御方法を定義する
public abstract class SceneController<SceneState> : MonoBehaviour, ISceneController where SceneState: System.Enum
{
    [SerializeField] private SceneData _sceneData; //担当するシーンのデータ
    protected SceneState _currentState;            //現在のシーンステート

    public virtual void Awake()
    {
        Assert.IsNotNull(_sceneData);
    }

    //シーン制御を開始
    public virtual void Boot()
    {
        //現在のシーンIDをシーンローダーに登録
        SceneLoader.Instance.SetSceneId(_sceneData.SceneId);
    }

    //ステートの変更
    protected virtual void ChangeState(SceneState nextState)
    {
        if(nextState.Equals(_currentState))
        {
            Debug.LogError("同じステートには遷移できません。:" + nextState);
            return;
        }

        _currentState = nextState;  //ステートを変更
        DoWork();                   //ステートごとの制御を実行
    }

    //ステートごとの制御を行う関数
    //純粋仮想関数として派生クラスで必ず定義
    protected abstract void DoWork(); 
}
