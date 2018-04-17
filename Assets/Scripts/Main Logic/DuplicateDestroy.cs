using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateDestroy : MonoBehaviour {
    private static Dictionary<string, GameObject> singletons;

    [SerializeField]
    private string id;
    
	private void Awake() {
        if(!singletons.ContainsKey(id)) {
            singletons[id] = gameObject;
        } else {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        if(singletons[id] == gameObject) {
            singletons.Remove(id);
        }
    }
}
