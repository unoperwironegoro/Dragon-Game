using System.Collections;
using System.Linq;
using UnityEngine;

public class CountdownLogic : MonoBehaviour {
    public AudioClip readySound;
    public AudioClip startSound;

    private DamageController[] dcs;

    void Start () {
        dcs = FindObjectsOfType<DragonController>()
                .Select(drc => drc.GetComponent<DamageController>())
                .ToArray();
        foreach(var dc in dcs) {
            dc.invincible = true;
        }
        StartCoroutine("RoundCountdown");
	}
	
	private IEnumerator RoundCountdown () {
        Time.timeScale = 0;

        // First Drum
        AudioSource.PlayClipAtPoint(readySound, Camera.main.transform.position);
        yield return new WaitForSecondsRealtime(1);

        // Second Drum
        AudioSource.PlayClipAtPoint(readySound, Camera.main.transform.position);
        yield return new WaitForSecondsRealtime(1);

        // Start Round
        Time.timeScale = 1;
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        foreach(var dc in dcs) {
            dc.invincible = false;
        }
        AudioSource.PlayClipAtPoint(startSound, Camera.main.transform.position);
    }
}
