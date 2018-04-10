using UnityEngine;
using System.Collections;

public abstract class SingletonBehaviour<T> : MonoBehaviour {
    protected static T instance;
    private static SingletonBehaviour<T> subclass_instance;

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
        subclass_instance = this;
        DontDestroyOnLoad(gameObject);
        return true;
    }

    private void OnDestroy() {
        if(subclass_instance == this) {
            instance = default(T);
            subclass_instance = null;
        }
    }

    protected virtual void Singlify() { }
    protected abstract T GetInstance();
}
