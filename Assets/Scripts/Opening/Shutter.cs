using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Shutter : MonoBehaviour
{
    [SerializeField] private string _openTriggerName = "Open";   //開くアニメーションのトリガー名
    [SerializeField] private string _closeTriggerName = "Close"; //閉じるアニメーションのトリガー名

    protected Animator _animator = null;  //アニメーター
    public event Action OnOpened;         //開き終わった通知
    public event Action OnClosed;         //閉じ終わった通知

    private const float ANIM_DEFAULT_SPEED = 1.0f;
    private const float ANIM_STOP_SPEED = 0.0f;

    public virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.speed = ANIM_DEFAULT_SPEED;
    }

    //シャッターを開く
    public void Open()
    {
        _animator.SetTrigger(_openTriggerName);
    }

    //シャッターを閉じる
    public void Close()
    {
        _animator.SetTrigger(_closeTriggerName);
    }

    //アニメーション停止
    public virtual void Stop()
    {
        _animator.speed = ANIM_STOP_SPEED;
    }

    //アニメーション再開
    public virtual void Resume()
    {
        _animator.speed = ANIM_DEFAULT_SPEED;
    }

    //開ききった際に実行する関数
    public virtual void OnOpenedEvent()
    {
        if (OnOpened == null)
        {
            Debug.LogWarning("開ききった際の通知が設定されていません");
            return;
        }

        OnOpened.Invoke();
    }

    //閉じ切った際に実装する関数
    public virtual void OnClosedEvent()
    {
        if (OnClosed == null)
        {
            Debug.LogWarning("閉じきった際の通知が設定されていません");
            return;
        }

        OnClosed.Invoke();
    }

}
