using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class goal : MonoBehaviour
{
    [SerializeField]
    private GameObject resultpanel = null;

    public void Start()
    {
        resultpanel.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
            resultpanel.SetActive(true);
    }
}
