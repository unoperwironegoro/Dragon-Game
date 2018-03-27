using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
    public void SetPlayerCount(int count) {
        var gdata = FindObjectOfType<GameData>();
        gdata.playerCount = count;
        gdata.setup = 0;
    }
}
