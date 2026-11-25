using System;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class Movie : MonoBehaviour
{
    [SerializeField] private string _animTriggerName = "Play"; //アニメーション再生用のトリガー名

    protected Animator _animator = null;    //アニメーター
    public event Action OnFinished;         //アニメーション終了通知

    private const float ANIM_DEFAULT_SPEED = 1.0f;
    private const float ANIM_STOP_SPEED = 0.0f;

    public virtual void Awake()
    {
        _animator = GetComponent<Animator> ();
        _animator.speed = ANIM_DEFAULT_SPEED;
    }

    //アニメーション再生
    public virtual void Play()
    {
        _animator.SetTrigger(_animTriggerName);
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

    //アニメーション終了時通知
    public virtual void OnAnimationFinished()
    {
        if(OnFinished == null)
        {
            Debug.LogWarning("終了時通知が設定されていません");
            return;
        }

        OnFinished.Invoke();
    }


}
