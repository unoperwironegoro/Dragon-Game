using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CDAnimator : MonoBehaviour, ICountdown {
    [SerializeField]
    private string[] countAnim;
    [SerializeField]
    private string finalAnim;

    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void Countdown(int count, int initialCount) {
        if (count > 0) {
            anim.SetTrigger(countAnim[(initialCount - count) % countAnim.Length]);
        } else {
            anim.SetTrigger(finalAnim);
        }
    }
}
