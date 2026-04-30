using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    [SerializeField]
    private Text resulttext;

    private Text text;
    private int score;
    private void Start()
    {
        score = 0;
        text = gameObject.GetComponentInChildren<Text>();
        text.text = score.ToString();
    }

    public void addscore(int value)
    {
        score = Mathf.Min(3, score+value);
        text.text = score.ToString();
        resulttext.text = score.ToString();
    }
}
