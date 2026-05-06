using UnityEngine;
using UnityEngine.Rendering.Universal;

public class player : MonoBehaviour
{
    private SpriteRenderer render = null;
    private Rigidbody2D rb = null;
    private Animator animator_ = null;
    private Light2D light_ = null;
    public void Initialize()
    {
        render = GetComponentInChildren<SpriteRenderer>();
        light_ = GetComponentInChildren<Light2D>();
        rb = GetComponent<Rigidbody2D>();
        animator_ = GetComponent<Animator>(); 
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void StartGame()
    {
        animator_.SetTrigger("startrolling");
    }

    public void StartRolling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        goal goal = collision.gameObject.GetComponent<goal>();
        if (goal != null)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            rb.rotation = 0.0f;
            rb.freezeRotation = true;
            rb.bodyType = RigidbodyType2D.Static;
            animator_.SetTrigger("clear");
        }
    }
}
