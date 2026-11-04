using System;
using UnityEngine;

//パネルの共通基底クラス
public class BasePanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup = null; //下位のUI要素へのアクセスを制御する用
    private Animator _animator = null;

    public event Action OnHideFinished;

    const string SHOW_ANIM_TRIGGER = "Show";
    const string HIDE_ANIM_TRIGGER = "Hide";

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();

        OnHideFinished = null;
    }
    public virtual void Initialize()
    {
        //用途によって通知を送る必要がない場合があるため、イベントがnullの場合には警告だけして実行時はnull許容
        if (OnHideFinished == null)
            Debug.LogWarning("非表示通知の送り先が設定されていません");

        //派生クラスで所有するUI要素の初期化を行う
    }

    //UI要素へのインタラクトができるかを切り替え
    protected void SetInteractable(bool flag)
    {
        if(_canvasGroup == null)
        {
            Debug.LogError("CanvasGroupが設定されていません");
            return;
        }
        _canvasGroup.blocksRaycasts = flag;
        _canvasGroup.interactable = flag;
    }

    //パネル表示
    //アニメータがついていたらそれを利用し、それがない場合はインタラクトを切り替えるだけ
    public virtual void Show()
    {
        if(_animator == null)
        {
            SetInteractable(true);
        }
        else //アニメーションがあるならアニメーションを再生
        {
            SetInteractable(false); //アニメーション中はUIを操作できないようにする
            _animator.SetTrigger(SHOW_ANIM_TRIGGER);
        }
    }

    //パネル非表示
    //アニメータがついていたらそれを利用し、それがない場合はインタラクトを切り替えるだけ
    public virtual void Hide()
    {
        SetInteractable(false);

        if(_animator == null)
        {
            OnHideFinished?.Invoke();
        }
        if(_animator != null)
        {
            _animator.SetTrigger(HIDE_ANIM_TRIGGER);
        }

    }

    //表示アニメーション終了時に行う処理(AnimationEventで呼び出し)
    public void OnFinishedShowAnim()
    {
        SetInteractable(true);
    }

    //非表示アニメーション終了時に行う処理(AnimationEventで呼び出し)
    public void OnFinishedHideAnim()
    {
        OnHideFinished?.Invoke(); //非表示になったことを通知
    }
}
