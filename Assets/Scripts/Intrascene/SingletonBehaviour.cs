using UnityEngine;
using System.Collections;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    /* This Behaviour is designed for scripts that should transcend scenes.
     * 
     * Scripts that extend this are to be referenced via Instance.
     * 
     * The main advantage of this as opposed to static references is that
     * the values will only be accessible during the lifespan of the script.
     */

    public static T Instance { get; private set; }
    private bool isSingleton;

    protected virtual void Awake() {
        AcquireSingletonStatus();
    }

    // Return true if it is indeed the one true instance
    // Call when overriding Awake() behaviour
    protected bool AcquireSingletonStatus() {
        if (InstanceExists()) {
            OnInstanceFound();
            return false;
        }
        Instance = FindObjectOfType<T>();
        DontDestroyOnLoad(gameObject);
        isSingleton = true;
        return true;
    }

    protected virtual bool InstanceExists() {
        return Instance != null;
    }

    private void OnDestroy() {
        if(isSingleton) {
            Instance = default(T);
        }
    }

    protected virtual void OnInstanceFound() { }
}
