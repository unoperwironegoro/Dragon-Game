using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    public float health;
    public bool Dead { get; private set; }

    public delegate void StunChange(GameObject changedObject, bool restun);
    public event StunChange onStun;

    public delegate void StateChange(GameObject changedObject);
    public event StateChange onDeath;
    public event StateChange onUnstun;

    public delegate void Damage(float damage);
    public event Damage onDamage;

    public bool stunned { get; private set; }
    private float stunTimer;

    public bool invincible;
    public bool unhurtable { get { return Dead || invincible; } }

    void Update() {
        if (stunned) {
            stunTimer -= Time.deltaTime;
            if (stunTimer < 0) {
                stunned = false;
                onUnstun(gameObject);
            }
        }
    }

    public bool Hurt(float damage) {
        if(unhurtable) {
            return false;
        }

        onDamage(damage);

        health -= damage;
        if(health <= 0) {
            Dead = true;
            onDeath(gameObject);
        }

        return true;
    }

    public void Stun(float stunDuration) {
        bool wasStunned = stunned;
        stunned = true;
        stunTimer = Mathf.Max(stunDuration, stunTimer);
        onStun(gameObject, wasStunned);
    }
}
