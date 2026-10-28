using System.Collections;
using UnityEngine;

public class shutter : MonoBehaviour
{
    private Animator animator_ = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gametime.instance.Initialize();
        gametime.instance.newtimescale(0);
        animator_ = GetComponent<Animator>();
        player player = FindAnyObjectByType<player>();
        player.Initialize();

        StartCoroutine(StartOpenAnimation());
    }

    IEnumerator StartOpenAnimation()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        animator_.SetTrigger("Open");
    }

    public void CallPause()
    {
        pause pause = FindAnyObjectByType<pause>();
        pause.Initialize();
    }

}
