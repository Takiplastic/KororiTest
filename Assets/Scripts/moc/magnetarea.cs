using UnityEngine;

public class magnetarea : MonoBehaviour
{
    [SerializeField]
    private magnet mymagnet = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mymagnet.isTouching = true;
        mymagnet.OnPlayerInMagnetArea(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        mymagnet.isTouching = false;
    }
}
