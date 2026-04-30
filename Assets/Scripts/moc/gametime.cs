using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class gametime : MonoBehaviour
{
    public static gametime instance = null;
    private Stack<float> timescales = new Stack<float>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    public void Initialize()
    {
        Time.timeScale = 1.0f;
        timescales = new Stack<float>();
    }

    public void newtimescale(float value)
    {
        Debug.Log("Called:newtimescale:"+value);
        timescales.Push(Time.timeScale);
        Time.timeScale = value;
    }

    public void backtimescale()
    {
        Debug.Log("Called:backscale:"+ timescales.Last());
        Time.timeScale = timescales.Pop();
    }
}
