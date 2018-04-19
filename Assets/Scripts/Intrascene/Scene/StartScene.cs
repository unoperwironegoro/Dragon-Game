using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {
	void Start () {
        SceneSwitcher.InstantSwitch("ArenaMenu", null, LoadSceneMode.Single);
        SceneSwitcher.InstantSwitch("MenuMain", null, LoadSceneMode.Additive);
    }
}
