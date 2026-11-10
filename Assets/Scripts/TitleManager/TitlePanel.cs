using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Assertions;

public class TitlePanel : BasePanel
{
    [SerializeField] Button _confirmButton = null;  //確認ボタン
    [SerializeField] Button _optionButton = null;   //設定ボタン
    [SerializeField] Button _startButton = null;    //開始ボタン

    public event Action OnOption;   //設定通知
    public event Action OnConfirm;  //確認通知
    public event Action OnStart;    //ゲーム開始通知

    protected override void Awake()
    {
        Debug.Log("TitlePanel.Awake");
        base.Awake();
        Assert.IsNotNull(_confirmButton);
        Assert.IsNotNull(_optionButton);
        Assert.IsNotNull(_startButton);

        OnOption = null;
        OnConfirm = null;
        OnStart = null;
    }

    public override void Initialize()
    {
        if (OnOption == null)
            Debug.LogWarning("設定通知の送り先が設定されていません");
        if (OnConfirm == null)
            Debug.LogWarning("確認通知の送り先が設定されていません");
        if (OnStart == null)
            Debug.LogWarning("ゲーム開始通知の送り先が設定されていません");

        _confirmButton.onClick.AddListener(() => { Hide(); OnConfirm?.Invoke(); });
        _optionButton.onClick.AddListener(() => { Hide(); OnOption?.Invoke(); });
        _startButton.onClick.AddListener(() => OnStart?.Invoke()); 
    }
}
