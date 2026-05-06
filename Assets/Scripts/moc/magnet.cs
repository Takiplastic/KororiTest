using UnityEngine;
using UnityEngine.EventSystems;

public class magnet : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialPanel;
    [SerializeField]
    private bool needTutorial = true;
    [SerializeField]
    private Transform center = null;

    private Transform magnetarea = null;
    private static bool hastutorialed = false;
    private EventTrigger eventTrigger;
    public bool isPulling = false;
    public bool isTouching = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tutorialPanel)
            tutorialPanel.SetActive(false);

        magnet.hastutorialed = false;
        eventTrigger = GetComponent<EventTrigger>();
        
        if (needTutorial)
            eventTrigger.enabled = false;
        else
            eventTrigger.enabled = true;

            magnetarea = transform.GetChild(0);
        isTouching = false;
    }

    private void Update()
    {
        if (isPulling && isTouching)
            Push();
    }

    public void OnPlayerInMagnetArea(Collider2D collision)
    {
        if (!magnet.hastutorialed && needTutorial)
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
        if (magnet.hastutorialed || !needTutorial)
            return;

        tutorialPanel.SetActive(false);
        gametime.instance.backtimescale();
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        magnet.hastutorialed = true;
        magnet[] magnets = FindObjectsByType<magnet>(FindObjectsSortMode.None);
        foreach (var instance in magnets)
        {
            instance.eventTrigger.enabled = true;
        }
    }

    public void EnablePull()
    {
        Debug.Log("Called:EnablePull");
        isPulling = true;
    }

    public void EndPull()
    {
        Debug.Log("Called:EndPull");
        isPulling = false;
    }

    private void Push()
    {
        Debug.Log("Called:Push");
        //centerˆÚ“®
        float y = (float)(center.position.y + 0.01 * Mathf.Sin(Time.deltaTime));
        center.position = new Vector2(center.position.x, y);
        //player‚Ð‚«‚Â‚¯
        player player = FindFirstObjectByType<player>();
        Rigidbody2D rigid_player = player.GetComponent<Rigidbody2D>();
        Vector2 add_vector = (center.position - player.transform.position).normalized;
        rigid_player.AddForce(add_vector * 10f);
    }
}
