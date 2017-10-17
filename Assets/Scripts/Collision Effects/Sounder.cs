using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Sounder : MonoBehaviour {
    public LayerMask toSound;
    public AudioClip sound;
    public float volume = 1f;

    void OnCollisionEnter2D(Collision2D col) {
        if ((toSound.value & (1 << col.gameObject.layer)) == 0) {
            return;
        }

        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, volume);
    }
}
