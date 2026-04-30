using UnityEditor.Search;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    GameObject menupanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menupanel.SetActive(false);
    }

    public void Show()
    {
        menupanel.SetActive(true);
        gametime.instance.newtimescale(0);
    }

    public void Hide()
    {
        menupanel.SetActive(false);
        gametime.instance.backtimescale();
    }
}
