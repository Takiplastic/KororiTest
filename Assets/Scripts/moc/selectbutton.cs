using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectbutton : MonoBehaviour
{
    [SerializeField]
    private Image scoreImg = null;
    [SerializeField]
    private Sprite[] scoreImageList = null;

    private Text text = null;
    private buttonevent eventcontroller = null;
    

    public void Initialize(StageData data)
    {
        foreach(Transform child in transform)
        {
            if(child.gameObject.GetComponent<Image>())
            {
                scoreImg = child.GetComponent<Image>();
                break;
            }
        }

        text = GetComponentInChildren<Text>();
        eventcontroller = GetComponent<buttonevent>();
        text.text = "stage" + data.stageId_.ToString();

        if (data.hasUnLocked_)
        {
            scoreImg.sprite = scoreImageList[data.score_];
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
            
    }

}
