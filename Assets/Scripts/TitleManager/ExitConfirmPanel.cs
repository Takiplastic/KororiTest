using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class ExitConfirmPanel : BasePanel
{
    [SerializeField] Button _yesButton; //ゲーム終了ボタン
    [SerializeField] Button _noButton;  //ウィンドウを閉じるボタン

    public event Action OnExit; //終了通知

    protected override void Awake()
    {
        base.Awake();
        Assert.IsNotNull(_yesButton);
        Assert.IsNotNull(_noButton);

        OnExit = null;
    }

    public override void Initialize()
    {
        base.Initialize();
        if (OnExit == null)
            Debug.LogWarning("終了通知の送り先が設定されていません");

        _yesButton.onClick.AddListener(() => OnExit?.Invoke());
        _noButton.onClick.AddListener(() => Hide());
    }
}
