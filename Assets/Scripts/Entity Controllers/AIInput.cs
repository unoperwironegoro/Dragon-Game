using UnityEngine;
using System.Collections.Generic;

public class AIInput : MonoBehaviour {
    private enum ControlState {
        Charging,
        Idling
    }

    private Transform lava;
    private Transform floor;
    private Transform ceiling;
    private List<DamageController> dragons;
    private IFlapController flapController;

    [SerializeField] private float flapMinHeadroom;
    [SerializeField] private float flapMinElevation;
    [SerializeField] private float flapMinTowards;

    private ControlState state;
    private float chargeTime = 0;
    private float idleTime = 0;
    private ControlDir flapDir;

    private void Start() {
        //TODO extract
        // Find level layout
        lava = TryGetTransform(GameObject.Find("Moving Lava"));
        floor = TryGetTransform(GameObject.Find("Floor"));
        ceiling = TryGetTransform(GameObject.Find("Ceiling"));

        flapController = GetComponent<IFlapController>();

        // Find all other dragons
        dragons = new List<DamageController>(FindObjectsOfType<DamageController>());
        dragons.Remove(GetComponent<DamageController>());
    }

    private bool EnvironmentExists() {
        return lava && floor && ceiling;
    }

    private Transform TryGetTransform(GameObject obj) {
        if (obj == null) {
            Debug.LogWarning("AI Failed to find bounds.");
            Destroy(gameObject);
            return null;
        }
        return obj.transform;
    }

    private Transform ClosestDragon() {
        if (dragons.Count == 0) {
            return null;
        }

        float dist = float.MaxValue;
        Transform closest = null;

        List<DamageController> deads = new List<DamageController>();
        foreach (DamageController dc in dragons) {
            if (dc.Dead) {
                deads.Add(dc);
                continue;
            }

            float newDist = (transform.position - dc.transform.position).magnitude;
            if (newDist < dist) {
                dist = newDist;
                closest = dc.transform;
            }
        }
        dragons.RemoveAll(dc => deads.Contains(dc));

        return closest;
    }

    // AI Behaviour
    private void Update() {
        if(!EnvironmentExists()) {
            return;
        }
        if (!AvoidEnvironment()) {
            switch(state) {
                case ControlState.Idling:
                    if(idleTime < Time.time) {
                        Flap();
                        state = ControlState.Charging;
                        chargeTime = RandomFutureTime(0.1f, 0.8f);
                    }
                    break;
                case ControlState.Charging:
                    if(chargeTime < Time.time) {
                        flapController.Release(flapDir);
                        state = ControlState.Idling;
                        idleTime = RandomFutureTime(0.1f, 0.8f);
                    }
                    break;
            }
        }
    }

    private bool AvoidEnvironment() {
        float y = transform.position.y;
        float headroom = ceiling.position.y - y;
        float elevation = y - Mathf.Max(floor.position.y, lava.position.y);
        if (headroom < flapMinHeadroom) {
            state = ControlState.Idling;
            return true;
        } else if (elevation < flapMinElevation) {
            flapController.Flap(RandomDir());
            return true;
        }
        return false;
    }

    private void Flap() {
        Transform closest = ClosestDragon();
        if (closest == null) {
            flapController.Flap(RandomDir());
            return;
        }

        Vector3 dp = transform.position - closest.position;
        bool close = Mathf.Abs(dp.x) < flapMinTowards;
        bool above = dp.y > 0;
        bool right = dp.x > 0;

        if(above && close) {
            // Fall into a slam!
            return;
        } else if (!above && close) {
            // Flap away
            flapDir = right ? ControlDir.RIGHT : ControlDir.LEFT;
            flapController.Flap(flapDir);
        } else {
            // Far away, above or below doesn't matter
            flapDir = right ? ControlDir.LEFT : ControlDir.RIGHT;
            flapController.Flap(flapDir);
        }
    }

    private ControlDir RandomDir() {
        return Random.value < 0.5f ? ControlDir.LEFT : ControlDir.RIGHT;
    }

    private float RandomFutureTime(float min, float max) {
        return Time.time + min + Random.Range(0f, max);
    }

    // Debug
    void OnDrawGizmosSelected() {
        float scale = 0.5f;

        Vector3 top = transform.position + Vector3.up * flapMinHeadroom;
        Vector3 bottom = transform.position - Vector3.up * flapMinElevation;
        Gizmos.DrawLine(top, bottom);

        Vector3 horizonal = Vector3.right * flapMinTowards;
        Gizmos.DrawLine(horizonal + transform.position, -horizonal + transform.position);

        Vector3 indicatorPos = transform.position + Vector3.up;
        if (chargeTime > Time.time) {
            Gizmos.DrawSphere(indicatorPos, 0.1f);
        }
        if (idleTime > Time.time) {
            Gizmos.DrawSphere(indicatorPos + Vector3.right, 0.1f);
        }
    }
}
