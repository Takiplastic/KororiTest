using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class goal : MonoBehaviour
{
    [SerializeField]
    private GameObject resultpanel = null;
    [SerializeField]
    private int currentStageId;
    [SerializeField]
    private int nextStageId;

    private Light2D light_ = null;
    private Animator animator_ = null;
    private SpriteRenderer render = null;

    public void Start()
    {
        resultpanel.SetActive(false);
        animator_ = GetComponent<Animator>();
        light_ = GetComponentInChildren<Light2D>();
        render = GetComponentInChildren<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            animator_.SetTrigger("clear");
        }      
    }

    
    public void ShowResult()
    {
        resultpanel.SetActive(true);

        ScorePanel scorePanel = FindAnyObjectByType<ScorePanel>();
        DataHandler.instance_.ChangeStageData(currentStageId, true, scorePanel.Score());
        DataHandler.instance_.ChangeStageData(nextStageId, true, 0);
    }

    public void HilightOn()
    {
        render.sortingOrder = 1;
        light_.enabled = true;
    }

    public void HilightOff()
    {
        render.sortingOrder = 0;
        light_.enabled = false;
    }
}
