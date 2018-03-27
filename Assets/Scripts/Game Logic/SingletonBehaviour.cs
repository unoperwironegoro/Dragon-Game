using UnityEngine;
using System.Collections;

public abstract class SingletonBehaviour<T> : MonoBehaviour {
    protected static T instance;

    void Awake() {
        SAwake();
    }

    // Return true if it is indeed the one true instance
    // Useful for overriding Start() behaviour
    protected bool SAwake() {
        if (instance != null) {
            Singlify();
            return false;
        }
        instance = GetInstance();
        DontDestroyOnLoad(gameObject);
        return true;
    }

    protected virtual void Singlify() { }
    protected abstract T GetInstance();
}
