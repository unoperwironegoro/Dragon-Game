using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls switching between scenes, using animations.
/// </summary>
public class SceneSwitcher : SingletonBehaviour<SceneSwitcher> {
    [SerializeField]
    private IResponder transitioner;
    private SceneSwitchData sceneSwitchData;
	
    /// <summary>
    /// Actually loads the Scene. May be called directly or as a Response.
    /// </summary>
	public void LoadScene () {
        if(sceneSwitchData.unloadScene != null) {
            SceneManager.UnloadSceneAsync(sceneSwitchData.unloadScene);
        }
        if(sceneSwitchData.sceneName != null) {
            SceneManager.LoadScene(sceneSwitchData.sceneName, sceneSwitchData.sceneMode);
        }
        sceneSwitchData = null;
	}

    private void StartLoadingScene() {
        transitioner.Request(LoadScene);
    }
    
    private static SceneSwitchData CreateSSD(string sceneName, string unloadSceneName, LoadSceneMode sceneMode) {
        return SceneSwitchData.Create(sceneName, unloadSceneName, sceneMode);
    }

#region API

    //TODO unload scene, builder?

    public static void Transition(string sceneName, string unloadSceneName = null, LoadSceneMode sceneMode = LoadSceneMode.Single) {
        Instance.Transition(CreateSSD(sceneName, unloadSceneName, sceneMode));
    }

    public void Transition(SceneSwitchData sceneSwitchData) {
        this.sceneSwitchData = sceneSwitchData;
        StartLoadingScene();
    }

    public static void InstantSwitch(string sceneName, string unloadSceneName = null, LoadSceneMode sceneMode = LoadSceneMode.Single) {
        Instance.InstantSwitch(CreateSSD(sceneName, unloadSceneName, sceneMode));
    }

    public void InstantSwitch(SceneSwitchData sceneSwitchData) {
        this.sceneSwitchData = sceneSwitchData;
        LoadScene();
    }

#endregion
}
