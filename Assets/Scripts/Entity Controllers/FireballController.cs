using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {
    public float lifetime;
    public GameObject explosionPrefab;
    private bool removed = false;

    void Update() {
        lifetime -= Time.deltaTime;
        if(lifetime < 0) {
            Remove();
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        var dragon = col.gameObject;
        if (dragon.layer == LayerMask.NameToLayer("Dragon")) {
            Remove();
        }
    }

    private void Remove() {
        if (!removed) {
            KillParticles(transform.GetChild(0).gameObject);
            transform.DetachChildren();
            Destroy(gameObject);
        }
        removed = true;

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    private void KillParticles(GameObject particleContainer) {
        var ps = particleContainer.GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = false;
        Destroy(ps, ps.main.startLifetime.constantMax);
    }
}
