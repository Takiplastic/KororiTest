
using UnityEngine;
using UnityEngine.UI;

public class pause : MonoBehaviour
{
    [SerializeField]
    private bool needHilightObj = false;
    [SerializeField]
    private Image restartButton = null;
    [SerializeField]
    private Image menuButton = null;
    [SerializeField]
    private nokogiri nokogiri = null;

    private player player_ = null;
    private goal goal_ = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize()
    {
        player_ = FindAnyObjectByType<player>();
        goal_ = FindAnyObjectByType<goal>();

        if (needHilightObj)
            HilightOnPlayerAndGoal();
        if (nokogiri)
            nokogiri.HilightOn();
    }

    public void StartGame()
    {
        gametime.instance.backtimescale();
        gameObject.SetActive(false);
        restartButton.enabled = true;
        menuButton.enabled = true;

        if (needHilightObj)
        {
            HilightOffPlayerAndGoal();
        }
        if (nokogiri)
        {
            nokogiri.HilightOff();
        }
        player_.StartGame();
    }

    private void HilightOnPlayerAndGoal()
    {
        player_.HilightOn();
        goal_.HilightOn();
    }

    private void HilightOffPlayerAndGoal()
    {
        player_.HilightOff();
        goal_.HilightOff();
    }

}
