using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour {
    public static SceneTransitionController stc;

    private Animator anim;
    private string nextSceneName;

    void Awake() {
        if(stc != null && stc != this) {
            Destroy(gameObject);
            return;
        }
        stc = this;
        anim = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }
	
    /// Called from the Animator
	public void EventLoadScene () {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
	}

    public void Transition(string sceneName) {
        nextSceneName = sceneName;
        anim.SetTrigger("Start");
    }

    public static void ChangeScene(string sceneName) {
        stc.Transition(sceneName);
    }
}
