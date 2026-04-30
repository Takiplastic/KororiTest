using UnityEngine;

public class windarea : MonoBehaviour
{
    [SerializeField]
    private winder mywinder = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mywinder.isTouching = true;
        mywinder.OnPlayerInWindArea(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        mywinder.isTouching = false;
    }
}
