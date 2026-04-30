using UnityEngine;
using UnityEngine.AI;

public class pause : MonoBehaviour
{ 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gametime.instance.Initialize();
        gametime.instance.newtimescale(0);
    }

    public void StartGame()
    {
        gametime.instance.backtimescale();
        gameObject.SetActive(false);
    }
}
