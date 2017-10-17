using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damager : MonoBehaviour {
    public LayerMask toDamage;
    public float damage;
    public float stun;

    void OnCollisionEnter2D(Collision2D col) {
        if ((toDamage.value & (1 << col.gameObject.layer)) == 0) {
            return;
        }

        var dc = col.gameObject.GetComponent<DamageController>();
        if(dc.Hurt(damage) && stun > 0) {
            dc.Stun(stun);
        }
    }
}
