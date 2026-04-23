using NUnit.Framework;
using UnityEngine;

//On/Offを切り替えるボタンのアニメーション連携用クラス
public class SwitchButton: MonoBehaviour
{
    private bool isOn;
    private Animator animator_;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        animator_ = GetComponent<Animator>();
        Assert.IsNotNull(animator_);

        //データ保持クラスからもらって初期化するように変更
        isOn = true;
        Switch();
    }

    public void Switch()
    {
        isOn = !isOn;
        animator_.SetBool("Switch", isOn);

    }
}
