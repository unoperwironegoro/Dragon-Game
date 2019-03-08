using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls switching between scenes, using animations.
/// </summary>
public class SceneSwitcher : MonoBehaviour {
    [SerializeField] private GameObject transitionObject;

    private IResponder transitioner;
    private SceneSwitchData sceneSwitchData;

    private void Start() {
        transitioner = transitionObject.GetComponent<IResponder>();
    }

    // Uses Transition object
    public void Transition(string[] loadedScenes, string[] unloadScenes) {
        Transition(CreateSSD(loadedScenes, unloadScenes));
    }

    public void Transition(SceneSwitchData ssd) {
        sceneSwitchData = ssd;
        StartLoadingScene();
    }

    // Does not use Transition object
    public void InstantSwitch(string[] loadedScenes, string[] unloadScenes) {
        InstantSwitch(CreateSSD(loadedScenes, unloadScenes));
    }

    public void InstantSwitch(SceneSwitchData ssd) {
        sceneSwitchData = ssd;
        LoadScene();
    }


    /// <summary>
    /// Actually loads the Scene. May be called directly or as a Response.
    /// </summary>
    public void LoadScene() {

        if (sceneSwitchData.unloadedScenes.Length > 0 && sceneSwitchData.unloadedScenes[0] == "*") {
            SceneManager.LoadScene(sceneSwitchData.loadedScenes[0], LoadSceneMode.Single);
            LoadNewScenes(sceneSwitchData.loadedScenes.Skip(1).ToArray());
            return;
        }

        LoadNewScenes(sceneSwitchData.loadedScenes);
        UnloadScenes(sceneSwitchData.unloadedScenes);
    }

    private void UnloadScenes(string[] sceneNames) {
        if (sceneNames != null) {
            foreach (var sceneName in sceneNames) {
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }
    }

    private void LoadNewScenes(string[] sceneNames) {
        if (sceneNames != null) {
            foreach (var sceneName in sceneNames) {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }
    }

    private void StartLoadingScene() {
        transitioner.Request(LoadScene);
    }

    private static SceneSwitchData CreateSSD(string[] loadedScenes, string[] unloadScenes) {
        var ssd = ScriptableObject.CreateInstance<SceneSwitchData>();
        ssd.loadedScenes = loadedScenes;
        ssd.unloadedScenes = unloadScenes;
        return ssd;
    }
}
