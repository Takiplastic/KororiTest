using UnityEngine;

public class score : MonoBehaviour
{
    private ScorePanel panel;

    private void Start()
    {
        panel = FindFirstObjectByType<ScorePanel>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            panel.addscore(1);
            Destroy(gameObject);
        }
            
    }
}
