using UnityEngine;
using UnityEngine.EventSystems;

public class winder : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialPanel;
    [SerializeField]
    private bool needTutorial = true;

    private Transform windarea = null;
    private static bool hastutorialed = false;
    private EventTrigger eventTrigger;
    private bool isBlowing = false;
    public bool isTouching = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (tutorialPanel)
            tutorialPanel.SetActive(false);

        winder.hastutorialed = false;
        eventTrigger = GetComponent<EventTrigger>();
        if(needTutorial)
            eventTrigger.enabled = false;
        else
            eventTrigger.enabled = true;

        windarea = transform.GetChild(0);
        isTouching = false;
    }

    private void Update()
    {
        if (isBlowing && isTouching)
            Push();
    }

    public void OnPlayerInWindArea(Collider2D collision)
    {
        if (!winder.hastutorialed && needTutorial)
        {
            tutorialPanel.SetActive(true);
            eventTrigger.enabled = true;
            GetComponent<SpriteRenderer>().sortingOrder = 1;
            gametime.instance.newtimescale(0);
            return;
        }
    }

    public void hidetutorial()
    {
        if (winder.hastutorialed || !needTutorial)
            return;

        tutorialPanel.SetActive(false);
        gametime.instance.backtimescale();
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        winder.hastutorialed = true;
        winder[] winders = FindObjectsByType<winder>(FindObjectsSortMode.None);
        foreach (var instance in winders)
        {
            instance.eventTrigger.enabled = true;
        }
    }

    public void Blow()
    {
        Debug.Log("Called:Blow");
        isBlowing = true;
    }

    public void EndBlow()
    {
        Debug.Log("Called:EndBlow");
        isBlowing = false;
    }

    private void Push()
    {
        Debug.Log("Called:Push");
        player player = FindFirstObjectByType<player>();
        Rigidbody2D rigid_player = player.GetComponent<Rigidbody2D>();
        Vector2 add_vector = (windarea.position - transform.position).normalized;
        rigid_player.AddForce(add_vector * 10f);
    }
}
