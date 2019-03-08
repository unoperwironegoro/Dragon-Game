using UnityEngine;

public class PlayerInput : MonoBehaviour, IController {
    public string leftKey;
    public string rightKey;

    void Start() {
        GetComponent<DragonController>().ictrl = this;
        Destroy(GetComponent<AIInput>());
    }

    public ControlDir Flap() {
        if(leftKey != default(string) && Input.GetKeyDown(leftKey)) {
            return ControlDir.LEFT;
        } else if (rightKey != default(string) && Input.GetKeyDown(rightKey)) {
            return ControlDir.RIGHT;
        }
        return ControlDir.NONE;
    }

    public ControlDir Release() {
        if (leftKey != default(string) && Input.GetKeyUp(leftKey)) {
            return ControlDir.LEFT;
        } else if (rightKey != default(string) && Input.GetKeyUp(rightKey)) {
            return ControlDir.RIGHT;
        }
        return ControlDir.NONE;
    }
}
