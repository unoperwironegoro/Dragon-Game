using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* - What is my purpose?
 * You pass data
 * - Oh my god 
 */
/// Player Data carrier. 
/// pdata is set from another Component and read at the start of new scenes 
/// to load in players' colours and controls.
public class ArenaData : SingletonBehaviour<ArenaData> {
    public struct PlayerData {
        public int score;
        public int fireballs;
        public int flaps;
        public int stomps;
    }

    public PlayerData[] pdata;
    public int roundsRemaining;

    private void Awake() {
        var gdata = FindObjectOfType<GameData>();
        if(gdata && SAwake()) {
            pdata = new PlayerData[gdata.playerCount];
        }    
    }

    protected override void Singlify() {
        Destroy(this);
    }

    protected override ArenaData GetInstance() {
        return this;
    }
}
