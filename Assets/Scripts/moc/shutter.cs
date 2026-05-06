using UnityEngine;

public class shutter : MonoBehaviour
{
    private Animator animator_ = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator_ = GetComponent<Animator>();
        player player = FindAnyObjectByType<player>();
        player.Initialize();
    }

    public void CallPause()
    {
        pause pause = FindAnyObjectByType<pause>();
        pause.Initialize();
    }

}
