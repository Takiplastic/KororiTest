using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;

public class OptionPanel : BasePanel
{
    [SerializeField] SwitchButton _soundSwitch; //サウンド設定用スイッチ
    [SerializeField] Button _closeButton;  //ウィンドウを閉じるボタン

    protected override void Awake()
    {
        base.Awake();
        Assert.IsNotNull(_soundSwitch);
        Assert.IsNotNull(_closeButton);

    }

    public override void Initialize()
    {
        base.Initialize();

        _soundSwitch.Initialize(true, (isSoundActive) => Debug.Log("サウンド設定更新とセーブデータ更新")); //本当は直値ではなく、SaveDataManagerの値
        _closeButton.onClick.AddListener(() => Hide());
    }
}
