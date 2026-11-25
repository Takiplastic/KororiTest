using UnityEngine;
using UnityEngine.UI;

public class OpeningButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => SceneLoader.Instance.LoadScene(SceneLoader.LoadType.Next));        
    }

}
