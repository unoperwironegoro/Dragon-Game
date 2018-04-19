using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIInput : MonoBehaviour, IController {
    private Transform lava;
    private Transform floor;
    private Transform ceiling;
    private List<DamageController> dragons;

    [SerializeField]
    private float flapMinHeadroom;
    [SerializeField]
    private float flapMinElevation;
    [SerializeField]
    private float flapMinTowards;

    private ControlDir flap;
    private ControlDir release;

    private float releaseTime = 0;

    void Start() {
        //TODO extract
        // Find level layout
        lava = GameObject.Find("Lava").transform;
        floor = GameObject.Find("Floor").transform;
        ceiling = GameObject.Find("Ceiling").transform;

        // Find all other dragons
        dragons = new List<DamageController>(FindObjectsOfType<DamageController>());
        dragons.Remove(GetComponent<DamageController>());
    }

    public ControlDir Flap() {
        ControlDir flap = DecideFlap();
        release = flap;
        return flap;
    }

    private ControlDir DecideFlap() {
        // Avoid ceiling / floor / lava
        float y = transform.position.y;
        if (ceiling.position.y - y < flapMinHeadroom) {
            return ControlDir.NONE;
        } else if (y - Mathf.Max(floor.position.y, lava.position.y) < flapMinElevation) {
            return RandomFlap();
        }

        // AI
        return PlanFlap();
    }

    private ControlDir PlanFlap() {
        if(releaseTime > Time.time) {
            return ControlDir.NONE;
        }

        Transform closest = ClosestDragon();
        if(closest == null) {
            releaseTime = Time.time + 0.1f + Random.Range(0.0f, 1.0f);
            return RandomFlap();
        }

        Vector3 dp = transform.position - closest.position;
        bool close = Mathf.Abs(dp.x) < flapMinTowards;
        bool above = dp.y > 0;
        bool right = dp.x > 0;

        if(above) {
            if(close) {
                // Descend into a slam!
                return ControlDir.NONE;
            }
        } else {
            if (close) {
                // Flap away
                right = !right;
            }
        }

        releaseTime = Time.time + 0.1f + Random.Range(0.0f, 0.8f);
        return right ? ControlDir.LEFT : ControlDir.RIGHT;
    }

    private ControlDir RandomFlap() {
        return Random.value < 0.5f ? ControlDir.LEFT : ControlDir.RIGHT;
    }

    private Transform ClosestDragon() {
        if(dragons.Count == 0) {
            return null;
        }

        float dist = float.MaxValue;
        Transform closest = null;

        List<DamageController> deads = new List<DamageController>();
        foreach(DamageController dc in dragons) {
            if(dc.Dead) {
                deads.Add(dc);
                continue;
            }

            float newDist = (transform.position - dc.transform.position).magnitude;
            if(newDist < dist) {
                dist = newDist;
                closest = dc.transform;
            }
        }
        dragons.RemoveAll(dc => deads.Contains(dc));

        return closest;
    }

    public ControlDir Release() {
        if(Time.time > releaseTime) {
            ControlDir ret = release;
            release = ControlDir.NONE;
            return ret;
        }
        return ControlDir.NONE;
    }

    void OnDrawGizmosSelected() {
        Vector3 top = transform.position + Vector3.up * flapMinHeadroom;
        Vector3 bottom = transform.position - Vector3.up * flapMinElevation;
        Gizmos.DrawLine(top, bottom);

        Vector3 horizonal = Vector3.right * flapMinTowards;
        Gizmos.DrawLine(horizonal + transform.position, -horizonal + transform.position);
    }
}
