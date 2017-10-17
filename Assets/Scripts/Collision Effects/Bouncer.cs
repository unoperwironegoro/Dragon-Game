using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bouncer : MonoBehaviour {
    public LayerMask toBounce;
    public float bounceMultiplier;

    void OnCollisionEnter2D (Collision2D col) {
        if ((toBounce.value & (1 << col.gameObject.layer)) == 0) {
            return;
        }

        col.rigidbody.velocity *= bounceMultiplier;
	}
}
