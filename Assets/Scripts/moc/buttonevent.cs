using UnityEngine;
using UnityEngine.UI;

public class buttonevent : MonoBehaviour
{
    [SerializeField]
    private Sprite normalSprite = null;
    [SerializeField]
    private Sprite pushedSprite = null;
    [SerializeField]
    private Sprite disabledSprite = null;
    private Image image = null;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void ToPushSprite()
    {
        Debug.Log("Called: ToPushSprite");
        image.sprite = pushedSprite;
    }

    public void ToNormalSprite()
    {
        Debug.Log("Called: ToNormalSprite");
        image.sprite = normalSprite;
    }

    public void ToDisabledSprite()
    {
        Debug.Log("Called: ToDisabledSprite");
        image.sprite = disabledSprite;
    }
}
