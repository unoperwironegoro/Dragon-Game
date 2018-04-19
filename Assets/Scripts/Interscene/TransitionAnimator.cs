using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TransitionAnimator : MonoBehaviour, IResponder {
    private Animator anim;
    
    private void Start () {
        anim = GetComponent<Animator>();
    }

    private Response response;

    public bool Request(Response response) {
        if(this.response != null) {
            return false;
        }
        this.response = response;
        anim.SetTrigger("Start");
        return true;
    }

    /// <summary>
    /// Called from the Animator exactly once per Request.
    /// </summary>
    public void AnimationComplete() {
        response();
        response = null;
    }
}
