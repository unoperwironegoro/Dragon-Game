using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneData : SingletonBehaviour<SceneData> {
    // Created in SceneTransitionController
    [SerializeField]
    private static string lastScene = null;
    [SerializeField]
    private static string thisScene = null;

    public static string LastScene { private set; get; }
    public static string ThisScene { private set; get; }

    protected override void Awake() {
        if (AcquireSingletonStatus()) {
            UpdateData();
        }
    }

    public void UpdateData() {
        if (lastScene == null) {
            LastScene = thisScene;
        } else {
            LastScene = lastScene;
        }

        if (thisScene == null) {
            thisScene = SceneManager.GetActiveScene().name;
        } else {
            ThisScene = thisScene;
        }
    }
}
