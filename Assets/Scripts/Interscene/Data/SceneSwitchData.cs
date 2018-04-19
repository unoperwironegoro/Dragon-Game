using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneSwitchData : ScriptableObject {
    public LoadSceneMode sceneMode;
    public string sceneName;
    public string unloadScene;

    public static SceneSwitchData Create(string sceneName, string unloadSceneName = null, LoadSceneMode sceneMode = LoadSceneMode.Single) {
        var ssd = CreateInstance<SceneSwitchData>();
        ssd.sceneName = sceneName;
        ssd.unloadScene = unloadSceneName;
        ssd.sceneMode = sceneMode;
        return ssd;
    }
}
