using SceneState;
using UnityEngine;
using UnityEngine.Assertions;

namespace SceneState
{
    public enum OpeningState
    {
        None,
        Shuttuer_Open,
        Movie,
        Shuttuer_Close,
    }
}
public class OpeningManager :SceneController<SceneState.OpeningState>
{
    [SerializeField] private Movie _openingMovie = null; //ムービーオブジェクト
    [SerializeField] private Shutter _shutter = null;    //開閉シャッターオブジェクト

    public override void Awake()
    {
        base.Awake();
        Assert.IsNotNull(_openingMovie);
        Assert.IsNotNull(_shutter);
    }

    public override void Boot()
    {
        base.Boot();
        //通知設定
        _openingMovie.OnFinished += () => ChangeState(OpeningState.Shuttuer_Close);     //ムービーが再生し終わったら、シャッターを閉じる
        _shutter.OnOpened += () => ChangeState(OpeningState.Movie);                     //シャッターが開ききったら、ムービーを再生
        _shutter.OnClosed += () => SceneLoader.Instance.LoadScene(SceneLoader.LoadType.Next); //シャッターが閉じたら次のシーンへ

        //オープニング開始
        ChangeState(OpeningState.Shuttuer_Open);
    }

    protected override void DoWork()
    {
        switch (_currentState)
        {
            case OpeningState.Shuttuer_Open:
                _shutter.Open();
                break;
            case OpeningState.Movie:
                _openingMovie.Play();
                break;
            case OpeningState.Shuttuer_Close:
                _shutter.Close();
                break;
        }
    }
}
