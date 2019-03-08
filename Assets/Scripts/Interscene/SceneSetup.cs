using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetup : MonoBehaviour {
    [SerializeField] SceneSwitchData firstSceneData;
    [SerializeField] SceneSwitcher sceneSwitcher;

	private void Start () {
        sceneSwitcher.InstantSwitch(firstSceneData);
    }
}
