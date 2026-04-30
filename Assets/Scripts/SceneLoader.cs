using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//string指定でシーンロード/シーンリロード
//遅延ありでシーンロード

public class SceneLoader : MonoBehaviour
{

    private const float WAIT_TIME = 0.2f;

    private static SceneLoader instance_ = null;

    private void Awake()
    {
        if (!instance_)
        {
            instance_ = this;
        }
        else
        {
            Destroy(gameObject);
        }    
    }

    public void LoadScene(string sceneName)
    {
        if (SceneExists(sceneName))
        {
            gametime.instance.Initialize();
            SceneManager.LoadScene(sceneName);
        }
            
        else
            Debug.LogError(sceneName + "：該当のシーンが存在しません。");

    }

    public void LoadsceneWithDelay(string sceneName)
    {
        if (SceneExists(sceneName))
            StartCoroutine(DelayLoad(sceneName));
        else
            Debug.LogError(sceneName + "：該当のシーンが存在しません。");
    }

    private IEnumerator DelayLoad(string sceneName)
    {
        yield return new WaitForSeconds(WAIT_TIME);
        gametime.instance.Initialize();
        SceneManager.LoadScene(sceneName);
    }

    private bool SceneExists(string sceneName)
    {
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);

        if (sceneIndex == -1)
            return false;
        else
            return true;
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
