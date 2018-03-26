using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour, IController {
    public string leftKey;
    public string rightKey;

    void Start() {
        Destroy(gameObject.GetComponent<AIInput>());
    }
    
    public ControlDir Flap() {
        if(Input.GetKeyDown(leftKey)) {
            return ControlDir.LEFT;
        } else if (Input.GetKeyDown(rightKey)) {
            return ControlDir.RIGHT;
        }
        return ControlDir.NONE;
    }

    public ControlDir Release() {
        if (Input.GetKeyUp(leftKey)) {
            return ControlDir.LEFT;
        } else if (Input.GetKeyUp(rightKey)) {
            return ControlDir.RIGHT;
        }
        return ControlDir.NONE;
    }
}
