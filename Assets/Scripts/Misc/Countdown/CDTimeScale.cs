using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDTimeScale : MonoBehaviour, ICountdown {
    [SerializeField]
    private float timeScale;

    private float originalTime;

    public void Countdown(int count, int initialCount) {
        if (count == initialCount) {
            originalTime = Time.timeScale;
            Time.timeScale = timeScale;
        }

        if (count == 0) {
            Time.timeScale = originalTime;
        }
    }
}
