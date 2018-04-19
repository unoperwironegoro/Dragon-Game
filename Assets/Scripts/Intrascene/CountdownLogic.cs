using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CountdownLogic : MonoBehaviour {
    public AudioClip readySound;
    public AudioClip startSound;
    public Image countdownImage;
    public Sprite spriteReady;
    public Sprite spriteGo;

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
        countdownImage.sprite = spriteReady;
        yield return new WaitForSecondsRealtime(1);

        // Second Drum
        AudioSource.PlayClipAtPoint(readySound, Camera.main.transform.position);
        countdownImage.sprite = spriteGo;
        countdownImage.rectTransform.sizeDelta *= 1.2f;
        yield return new WaitForSecondsRealtime(1);

        // Start Round
        Time.timeScale = 1;
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        foreach(var dc in dcs) {
            dc.invincible = false;
        }
        AudioSource.PlayClipAtPoint(startSound, Camera.main.transform.position);
        countdownImage.color = new Color(0, 0, 0, 0);
    }
}
