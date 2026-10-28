using UnityEngine;
using UnityEngine.EventSystems;

public class nokogiri : MonoBehaviour
{
    [SerializeField]
    private bool needTutorial = true;
    [SerializeField]
    private SpriteRenderer circleRenderer = null;

    private static bool hastutorialed = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nokogiri.hastutorialed = false;
    }

    public void showTutorial()
    {
        if (nokogiri.hastutorialed || !needTutorial)
            return;
        HilightOn();
    }

    public void hidetutorial()
    {
        if (nokogiri.hastutorialed || !needTutorial)
            return;

        HilightOff();
        nokogiri.hastutorialed = true;
    }

    //"Gimmick"レイヤーにすることで専用ライトが適用+UIよりも前に来る
    public void HilightOn()
    {
        circleRenderer.sortingLayerName = "Gimmick";
    }

    public void HilightOff()
    {
        circleRenderer.sortingLayerName = "Default";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if (player)
            SceneLoader.instance_.Reload();
    }


}
