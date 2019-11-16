using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public string leftKey;
    public string rightKey;
    public IFlapController flapController;

    void Start() {
        flapController = GetComponent<DragonController>();
        Destroy(GetComponent<AIInput>());
    }

    private void Update() {
        if (leftKey != default(string) && Input.GetKeyDown(leftKey)) {
            flapController.Flap(ControlDir.LEFT);
        }
        if (rightKey != default(string) && Input.GetKeyDown(rightKey)) {
            flapController.Flap(ControlDir.RIGHT);
        }

        if (leftKey != default(string) && Input.GetKeyUp(leftKey)) {
            flapController.Release(ControlDir.LEFT);
        }
        if (rightKey != default(string) && Input.GetKeyUp(rightKey)) {
            flapController.Release(ControlDir.RIGHT);
        }
    }
}
