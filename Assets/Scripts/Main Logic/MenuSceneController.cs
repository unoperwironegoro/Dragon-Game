using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneController : MonoBehaviour {
    void Start() {
        // Wipe previous arena data
        Destroy(FindObjectOfType<ArenaData>());
    }

    public void SetPlayerCount(int count) {
        var gdata = FindObjectOfType<GameData>();
        gdata.playerCount = count;
        gdata.setup = 0;
    }
}
