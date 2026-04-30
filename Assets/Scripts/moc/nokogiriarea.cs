using UnityEngine;

public class nokogiriarea : MonoBehaviour
{
    [SerializeField]
    private nokogiri mynokogiri = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mynokogiri.OnPlayerInnokogiriArea(collision);
    }
}
