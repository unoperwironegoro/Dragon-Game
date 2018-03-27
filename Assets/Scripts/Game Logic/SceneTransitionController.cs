using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : SingletonBehaviour<SceneTransitionController> {
    private Animator anim;
    private string nextSceneName;

    void Awake() {
        if(SAwake()) {
            anim = GetComponent<Animator>();
        }
    }
	
    /// Called from the Animator
	public void EventLoadScene () {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
	}

    private void Transition(string sceneName) {
        nextSceneName = sceneName;
        anim.SetTrigger("Start");
    }

    // Instantly load scene
    public void InstantSceneUI(string sceneName) {
        instance.nextSceneName = sceneName;
        instance.EventLoadScene();
    }

    // Use fader
    public void TransitionSceneUI(string sceneName) {
        instance.Transition(sceneName);
    }

    // Instantly load scene
    public static void InstantScene(string sceneName) {
        instance.nextSceneName = sceneName;
        instance.EventLoadScene();
    }

    // Use fader
    public static void TransitionScene(string sceneName) {
        instance.Transition(sceneName);
    }

    protected override SceneTransitionController GetInstance() {
        return this;
    }
}
