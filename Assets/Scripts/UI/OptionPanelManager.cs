using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

//パネルの表示/非表示アニメーションの実行
//設定の反映

public class OptionPanelManager : MonoBehaviour
{
    [SerializeField]
    private SwitchButton switchButton_;
    
    private Animator animator_;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        Assert.IsNotNull(switchButton_);
        switchButton_.Start();

        animator_ = GetComponent<Animator>();
        Assert.IsNotNull(animator_);
    }

    public void Show()
    {
        AllButtonsInteractability(false);
        animator_.SetTrigger("Show");
    }

    public void Hide()
    {
        AllButtonsInteractability(false);
        animator_.SetTrigger("Hide");
    }

    public void OnFinishShow()
    {
        ChildButtonsInteractability(true);
    }

    public void OnFinishHide()
    {
        AllButtonsInteractability(true);
        ChildButtonsInteractability(false);
    }

    void AllButtonsInteractability(bool isInteractable)
    {
        Button[] buttonList = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (var button in buttonList)
        {
            button.interactable = isInteractable;
        }
    }

    void ChildButtonsInteractability(bool isInteractable)
    {
        Button[] buttonList = gameObject.GetComponentsInChildren<Button>();
        foreach (var button in buttonList)
        {
            button.interactable = isInteractable;
        }
    }
}
