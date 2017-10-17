using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour {
    public LayerMask toBurn;
    public GameObject fireTrailPrefab;

    void OnCollisionEnter2D(Collision2D col) {
        if ((toBurn.value & (1 << col.gameObject.layer)) == 0) {
            return;
        }

        Instantiate(fireTrailPrefab, col.gameObject.transform);
    }
}
