using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour, IController {
    public string leftKey;
    public string rightKey;

    void Start() {
        GetComponent<DragonController>().ictrl = this;
        Destroy(GetComponent<AIInput>());
    }

    public ControlDir Flap() {
        if(leftKey != "" && Input.GetKeyDown(leftKey)) {
            return ControlDir.LEFT;
        } else if (rightKey != "" && Input.GetKeyDown(rightKey)) {
            return ControlDir.RIGHT;
        }
        return ControlDir.NONE;
    }

    public ControlDir Release() {
        if (leftKey != "" && Input.GetKeyUp(leftKey)) {
            return ControlDir.LEFT;
        } else if (rightKey != "" && Input.GetKeyUp(rightKey)) {
            return ControlDir.RIGHT;
        }
        return ControlDir.NONE;
    }
}
