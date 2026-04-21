using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

//パネルの表示/非表示アニメーションの実行
//設定の反映

public class OptionPanelManager : MonoBehaviour
{
    private Animator animator_;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator_ = GetComponent<Animator>();
        Assert.IsNotNull(animator_);
    }

    public void ShowOptionPanel()
    {
        Button[] buttonList = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach(var button in buttonList)
        {
            button.interactable = false;
        }
        animator_.SetTrigger("Show");
    }

    public void HideOptionPanel()
    {
        Button[] buttonList = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (var button in buttonList)
        {
            button.interactable = true;
        }
        animator_.SetTrigger("Hide");
    }

}
