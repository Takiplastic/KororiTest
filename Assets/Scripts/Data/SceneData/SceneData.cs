using UnityEngine;

[CreateAssetMenu(fileName = "NewScene", menuName = "Scene/SceneData")]
public class SceneData : ScriptableObject
{
    [SerializeField] private int _sceneId;      //シーンID
    [SerializeField] private string _sceneName; //シーン名
    [SerializeField] private int _nextSceneId;  //次のシーンID

    public int SceneId => _sceneId;
    public string SceneName => _sceneName;
    public int NextSceneId => _nextSceneId;
}
