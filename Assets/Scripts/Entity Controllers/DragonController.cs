using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour {
    [HideInInspector]
    public int playerID = 0;

    public Vector2 flapVelocity;
    public Vector2 recoilVelocity;
    public Vector2 projectileVelocity;

    [SerializeField]
    private GameObject projectilePrefab;
    private ParticleSystem smoulder;

    [SerializeField]
    private AudioClip roarSound;
    [SerializeField]
    private AudioClip flapSound;
    [SerializeField]
    private AudioClip fireSound;
    private float roarVolume = 1.0f;
    private float flapVolume = 1.0f;
    private float fireVolume = 0.3f;

    private Rigidbody2D rb2d;
    private Animator anim;
    private DamageController dc;
    private IController ictrl;

    private float minChargeTime;
    private float chargeTimer = -100;
    private bool Charged {
        set { if(value){ Charge(); } else { Discharge(); } charged = value; }
        get { return charged; }
    }
    private bool charged;

    private float stompStun = 0.5f;
    private float stompDamage = 1.0f;
    private float stompBoost = 10f;

    private Vector2 projSpawnpoint = new Vector2(0.6f, 0.0f);

    private bool readInput = true;

	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dc = GetComponent<DamageController>();
        ictrl = GetComponent<IController>();
        smoulder = GetComponentInChildren<ParticleSystem>();
	}

    void Start() {
        RegisterCallbacks();

        // Shoot a fireball only after reaching the apex of a flap
        float g = rb2d.gravityScale * Physics2D.gravity.y;
        float apexTime = Mathf.Abs(flapVelocity.y / g);
        float leeway = 0.05f;
        minChargeTime = apexTime - leeway;
    }

	void Update () {
        if(!dc.stunned && readInput) {
            ControlDragon();
        }
	}

    private void ControlDragon() {
        // Flap
        if (Flap()) {
            chargeTimer = 0;
        }

        // Release
        int dirRelease = Release();
        if (dirRelease != 0) {
            if(charged) {
                ShootProjectile(dirRelease);
                Charged = false;
            }
            anim.SetBool("Flap", false);
            chargeTimer = -1;
        // Charge
        } else if (chargeTimer > -1) {
            chargeTimer += Time.deltaTime;
            if (!charged && chargeTimer > minChargeTime) {
                Charged = true;
            }
        }
    }

    #region Player Actions
    private bool Flap() {
        int dir = 0;
        switch(ictrl.Flap()) {
            case ControlDir.NONE:
                return false;
            case ControlDir.LEFT:
                dir = -1;
                break;
            case ControlDir.RIGHT:
                dir = 1;
                break;
        }

        // Physics
        Vector2 flap = flapVelocity;
        flap.x *= dir;
        rb2d.velocity = flap;

        // Art Effects
        Turn(dir);
        anim.SetBool("Flap", true);
        AudioSource.PlayClipAtPoint(flapSound, Camera.main.transform.position, flapVolume);
        return true;
    }

    private int Release() {
        switch (ictrl.Release()) {
            case ControlDir.LEFT:
                return -1;
            case ControlDir.RIGHT:
                return 1;
            default:
                return 0;
        }
    }

    private void Charge() {
        smoulder.Play();
    }

    private void Discharge() {
        smoulder.Stop();
    }

    private void ShootProjectile(int dir) {
        // Projectile Physics
        Vector2 spawnpointOffset = projSpawnpoint;
        spawnpointOffset.x *= dir;
        Vector2 spawnpoint = (Vector2)transform.position + spawnpointOffset;
        Vector2 pVelocity = projectileVelocity;
        pVelocity.x *= dir;

        GameObject projectile = Instantiate(projectilePrefab, spawnpoint, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = pVelocity;

        // Recoil Physics
        Vector2 recoil = recoilVelocity;
        recoil.x *= -dir;
        rb2d.velocity = recoil;

        // Art Effects
        Discharge();
        anim.SetTrigger("Fire");
        AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position, fireVolume);
        Turn(dir);
    }
    #endregion

    void OnCollisionEnter2D(Collision2D col) {
        int layer = col.gameObject.layer;

        if(layer == LayerMask.NameToLayer("Dragon")) {
            if(AboveDragon(col.gameObject)) {
                OnStomp();
            } else {
                OnStomped();
            }
        }
        if(layer == LayerMask.NameToLayer("Environment")) {
            if(col.gameObject.tag == "Lava" && dc.Dead) {
                Physics2D.IgnoreCollision(col.collider, col.otherCollider);
            }
        }
    }

    // Jump Attack
    private bool AboveDragon(GameObject otherDragon) {
        Vector2 disp = transform.position - otherDragon.transform.position;
        return disp.y > 0.2f;
    }

    #region OnEvent Logic
    private void OnStun(GameObject _) {
        anim.SetBool("Stunned", true);
    }

    private void OnUnstun(GameObject _) {
        anim.SetBool("Stunned", false);
    }

    private void OnStomp() {
        if(!dc.Dead) {
            return;
        }
        var v = rb2d.velocity;
        v.y = stompBoost;
        rb2d.velocity = v;
    }

    private void OnStomped() {
        if(dc.Hurt(stompDamage)) {
            dc.Stun(stompStun);
        }
    }

    private void OnDeath(GameObject _) {
        dc.Stun(float.MaxValue);
    }

    private void OnDamage(float damage) {
        if(damage > 0) {
            AudioSource.PlayClipAtPoint(roarSound, Camera.main.transform.position, roarVolume);
        }
    }
    #endregion

    #region Callback Subscription
    private void RegisterCallbacks() {
        dc.onStun += OnStun;
        dc.onUnstun += OnUnstun;
        dc.onDeath += OnDeath;
        dc.onDamage += OnDamage;
    }
    void OnDestroy() {
        dc.onStun -= OnStun;
        dc.onUnstun -= OnUnstun;
        dc.onDeath -= OnDeath;
        dc.onDamage -= OnDamage;
    }
    #endregion

    #region Graphics
    private void Turn(int dir) {
        var scale = transform.localScale;
        scale.x = dir * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
    #endregion

    public void SwitchController(IController ictrl) {
        this.ictrl = ictrl;
    }
}
