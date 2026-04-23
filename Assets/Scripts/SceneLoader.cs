using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//string指定でシーンロード/シーンリロード
//遅延ありでシーンロード

public class SceneLoader : MonoBehaviour
{

    private const float WAIT_TIME = 1.0f;

    private static SceneLoader instance_ = null;

    private void Awake()
    {
        if (!instance_)
        {
            instance_ = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
    }

    public void LoadScene(string sceneName)
    {
        if (SceneExists(sceneName))
            SceneManager.LoadScene(sceneName);
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
}
