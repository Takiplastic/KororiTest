using UnityEngine;
using UnityEngine.EventSystems;

public class nokogiri : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialPanel;
    [SerializeField]
    private bool needTutorial = true;
    [SerializeField]
    private SpriteRenderer circleRenderer = null;

    private static bool hastutorialed = false;
    private EventTrigger eventTrigger;

    [SerializeField]
    private Transform[] movepoints = null;
    [SerializeField]
    private float speed = 1f;
    private int destinationID = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tutorialPanel)
            tutorialPanel.SetActive(false);

        nokogiri.hastutorialed = false;
        eventTrigger = GetComponent<EventTrigger>();

        if (needTutorial)
            eventTrigger.enabled = false;
        else
            eventTrigger.enabled = true;

        if (movepoints.Length > 0)
            destinationID = 0;
    }

    private void Update()
    {
        MoveAroundPoint();
    }

    private void MoveAroundPoint()
    {
        if (destinationID < 0) return;
        float dist = Vector2.Distance(transform.position, movepoints[destinationID].position);
        if (dist < 0.1f)
            destinationID = (destinationID + 1) % movepoints.Length;
        transform.position = Vector2.MoveTowards(transform.position, movepoints[destinationID].position, speed * Time.deltaTime);
        
    }

    public void OnPlayerInnokogiriArea(Collider2D collision)
    {
        if (!nokogiri.hastutorialed && needTutorial)
        {
            tutorialPanel.SetActive(true);
            eventTrigger.enabled = true;
            circleRenderer.sortingOrder = 1;
            gametime.instance.newtimescale(0);
            return;
        }
    }

    public void hidetutorial()
    {
        if (nokogiri.hastutorialed || !needTutorial)
            return;

        tutorialPanel.SetActive(false);
        gametime.instance.backtimescale();
        circleRenderer.sortingOrder = 0;
        nokogiri.hastutorialed = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if(player)
            Destroy(player.gameObject);
    }


}
