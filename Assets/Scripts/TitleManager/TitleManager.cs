using UnityEngine;
using UnityEngine.Assertions;

public class TitleManager : MonoBehaviour, ISceneController
{ 
    //タイトルシーンの状態
    private enum TitleState{
        None,       //空値(初期化前の仮置き)
        Intialize,  //初期化
        Normal,     //通常
        Option,     //設定
        ExitConfirm //終了確認
    }

    private TitleState _currenState;    
    [SerializeField] BasePanel _titlePanel;
    [SerializeField] BasePanel _optionPanel;
    [SerializeField] BasePanel _exitConfirmPanel;
    
    public void Awake()
    {
        //必要なUIが用意されているかチェック
        Assert.IsNotNull(_titlePanel); 
        Assert.IsNotNull(_optionPanel);
        Assert.IsNotNull(_exitConfirmPanel);

        _currenState = TitleState.None;
    }

    //起動
    public void Boot()
    {
        //通知登録
        var titlePanel = _titlePanel as TitlePanel;
        if (titlePanel != null)
        {
            titlePanel.OnOption += () => ChangeState(TitleState.Option);
            titlePanel.OnConfirm += () => ChangeState(TitleState.ExitConfirm);
            titlePanel.OnStart += () => SceneLoader.Instnace.LoadScene("Stage_1"); //本当はMasterDataProviderからもらう

            Debug.Log("TitlePanel イベント登録完了");
        }

        var confirmPanel = _exitConfirmPanel as ExitConfirmPanel;
        if (confirmPanel != null)
        {
            confirmPanel.OnHideFinished += () => ChangeState(TitleState.Normal);
            confirmPanel.OnExit += () => AppManager.Instance.Quit(); //本当はAppManagerから呼び出し
        }

        _optionPanel.OnHideFinished += () => ChangeState(TitleState.Normal);

        //ステートを"初期化状態"に変更
        ChangeState(TitleState.Intialize);
    }

    //ステートを変更(同じステートへの遷移は認めない)
    private void ChangeState(TitleState newState)
    {
        if(_currenState == newState)
        {
            Debug.LogWarning("同じステートへの遷移, TitleState:" + _currenState);
            return;
        }

        _currenState = newState;
        doWork();
    }

    //ステートごとの制御を実行
    private void doWork()
    {
        switch (_currenState)
        {
            case TitleState.Intialize:
                _titlePanel.Initialize();
                _optionPanel.Initialize();
                _exitConfirmPanel.Initialize();

                ChangeState(TitleState.Normal);
                break;

            case TitleState.Normal:
                _titlePanel.Show();
                break;

            case TitleState.Option:
                _optionPanel.Show();
                break;
                
            case TitleState.ExitConfirm:
                _exitConfirmPanel.Show();
                break;

            default:
                Debug.LogError("意図しないステート:" + _currenState);
                break;
        }
    }
}
