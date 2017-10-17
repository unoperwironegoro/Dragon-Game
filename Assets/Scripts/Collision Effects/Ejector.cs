using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Ejector : MonoBehaviour {
    public LayerMask toEject;
    public float ejectionVelocity;

    void OnCollisionEnter2D(Collision2D col) {
        if((toEject.value & (1 << col.gameObject.layer)) == 0) {
            return;
        }

        var v = col.rigidbody.velocity;
        Vector2 perpComp = Vector3.Project(v, transform.right);
        Vector2 ejComp = transform.up * ejectionVelocity;
        col.rigidbody.velocity = ejComp + perpComp;
    }
}
