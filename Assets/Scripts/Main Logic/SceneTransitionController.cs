using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : SingletonBehaviour<SceneTransitionController> {
    // TODO rethink this class' singleton usage - singletonBehaviours transcend scenes
    private Animator anim;
    private string nextSceneName;

    protected override void Awake() {
        if(AcquireSingletonStatus()) {
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
        Instance.nextSceneName = sceneName;
        Instance.EventLoadScene();
    }

    // Use fader
    public void TransitionSceneUI(string sceneName) {
        Instance.Transition(sceneName);
    }

    // Instantly load scene
    public static void InstantScene(string sceneName) {
        Instance.nextSceneName = sceneName;
        Instance.EventLoadScene();
    }

    // Use fader
    public static void TransitionScene(string sceneName) {
        Instance.Transition(sceneName);
    }
}
