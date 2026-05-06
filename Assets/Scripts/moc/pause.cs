
using UnityEngine;
using UnityEngine.UI;

public class pause : MonoBehaviour
{
    [SerializeField]
    private bool needHilightObj = false;
    [SerializeField]
    private CanvasGroup restartButton = null;
    [SerializeField]
    private CanvasGroup menuButton = null;

    private player player_ = null;
    private goal goal_ = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize()
    {
        gametime.instance.Initialize();
        gametime.instance.newtimescale(0);

        player_ = FindAnyObjectByType<player>();
        goal_ = FindAnyObjectByType<goal>();

        if (needHilightObj)
            HilightOnPlayerAndGoal();
    }

    public void StartGame()
    {
        gametime.instance.backtimescale();
        gameObject.SetActive(false);
        restartButton.alpha = 1.0f;
        restartButton.interactable = true;
        menuButton.alpha = 1.0f;
        menuButton.interactable = true;

        if (needHilightObj)
        {
            HilightOffPlayerAndGoal();
            player_.StartGame();
        }
            
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
