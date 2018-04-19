using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class Countdown : MonoBehaviour {
    [SerializeField]
    private int initialCount;
    [SerializeField]
    private float interval;
    [SerializeField]
    private bool realTime;
    [SerializeField]
    private GameObject[] countdowns;
    private IEnumerable<ICountdown> icountdowns;

    private void Start () {
        icountdowns = countdowns.ToList().Select(g => g.GetComponent<ICountdown>());
        StartCoroutine("BeginCountdown");
	}

    private void SendCount(int count) {
        foreach(ICountdown icountdown in icountdowns) {
            icountdown.Countdown(count, initialCount);
        }
    }

    private IEnumerator CountdownWait() {
        if(realTime) {
            yield return new WaitForSecondsRealtime(interval);
        } else {
            yield return new WaitForSeconds(interval);
        }
    }
	
	private IEnumerator BeginCountdown () {
        for (int count = initialCount; count >= 0; count--) {
            SendCount(count);
            yield return CountdownWait();
        }

        // First Drum
        //AudioSource.PlayClipAtPoint(readySound, Camera.main.transform.position);
        //countdownImage.sprite = spriteReady;
        // Second Drum
        //AudioSource.PlayClipAtPoint(readySound, Camera.main.transform.position);
        //countdownImage.sprite = spriteGo;
        //countdownImage.rectTransform.sizeDelta *= 1.2f;
        // Start Round
        //AudioSource.PlayClipAtPoint(startSound, Camera.main.transform.position);
        //countdownImage.color = new Color(0, 0, 0, 0);
    }
}
