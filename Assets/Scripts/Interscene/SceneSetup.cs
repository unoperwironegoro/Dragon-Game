using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetup : MonoBehaviour {
    [SerializeField]
    private string baseScene;
    [SerializeField]
    private string[] extraScenes;

	private void Start () {
        if(baseScene != null) {
            SceneSwitcher.InstantSwitch("ArenaMenu", null, LoadSceneMode.Single);
        }

        foreach(string s in extraScenes) {
            SceneSwitcher.InstantSwitch(s, null, LoadSceneMode.Additive);
        }
    }
}
