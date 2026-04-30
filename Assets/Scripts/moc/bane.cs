using UnityEngine;
using UnityEngine.EventSystems;

public class bane : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialPanel;

    [SerializeField]
    private bool needTutorial = true;
    private static bool hastutorialed = false;
    private EventTrigger eventTrigger;
    public bool isBounding = false;
    public bool isTouching = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tutorialPanel)
            tutorialPanel.SetActive(false);

        bane.hastutorialed = false;
        eventTrigger = GetComponent<EventTrigger>();

        if(needTutorial)
            eventTrigger.enabled = false;
        else
            eventTrigger.enabled = true;
    }

    private void Update()
    {
        if (isBounding && isTouching)
            Push();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        isTouching = true;


        if (!bane.hastutorialed && needTutorial)
        {
            tutorialPanel.SetActive(true);
            GetComponent<SpriteRenderer>().sortingOrder = 1;
            gametime.instance.newtimescale(0);
            eventTrigger.enabled = true;
            return;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        isTouching = false;
    }


    public void hidetutorial()
    {
        if (!needTutorial || bane.hastutorialed) return;

        tutorialPanel.SetActive(false);
        gametime.instance.backtimescale();
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        bane.hastutorialed =true;
        bane[] banes = FindObjectsByType<bane>(FindObjectsSortMode.None);
        foreach(var instance in banes)
        {
            instance.eventTrigger.enabled = true;
        }
    }

    public void Bound()
    {
        isBounding = true;
    }

    private void Push()
    {
        player player = FindFirstObjectByType<player>();
        Rigidbody2D rigid_player = player.GetComponent<Rigidbody2D>();
        Vector2 add_vector = transform.up;
        rigid_player.AddForce(add_vector * 600f);
        isBounding = false;
    }
}
