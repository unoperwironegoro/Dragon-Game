using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IColourable))]
public class StunColour : MonoBehaviour {
    [SerializeField]
    private Color stunColor;
    private DamageController dc;
    private IColourable c;
	
	protected virtual void Awake() {
        dc = GetComponent<DamageController>();
        c = GetComponent<IColourable>(); 

        dc.onStun += OnStun;
        dc.onUnstun += OnUnstun;
    }
	
	protected virtual void OnDestroy () {
        dc.onStun -= OnStun;
        dc.onUnstun -= OnUnstun;
    }

    private void OnStun(GameObject _, bool restun) {
        c.AddColour(stunColor);
    }

    private void OnUnstun(GameObject _) {
        c.ClearColour();
    }
}
