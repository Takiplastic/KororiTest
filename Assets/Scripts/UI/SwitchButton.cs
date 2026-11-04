using System;
using UnityEngine;
using UnityEngine.UI;

//スイッチクラス
[RequireComponent(typeof(Button))]
public class SwitchButton: MonoBehaviour
{
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    private bool isOn;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Initialize(bool initial, Action<bool> OnClickAction)
    {
        if (OnClickAction == null)
            Debug.LogWarning("スイッチ切り替え時の通知先が設定されていません");

        isOn = initial;
        SetSprite();
        _button.onClick.AddListener(() =>
        {
            isOn = !isOn;
            SetSprite();
            OnClickAction?.Invoke(isOn);
        });
    }

    private void SetSprite()
    {
        if(isOn)
            _button.image.sprite = _onSprite;
        else
            _button.image.sprite = _offSprite;
    }
}
