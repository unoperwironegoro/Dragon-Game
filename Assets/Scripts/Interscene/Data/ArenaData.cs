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
    public int dragonCount { get { return pdata.Length; } }

    protected override void Awake() {
        var gdata = GameData.Instance;
        if(gdata && AcquireSingletonStatus()) {
            pdata = new PlayerData[GameData.PlayerCount];
        }    
    }

    protected override void OnInstanceFound() {
        Destroy(this);
    }
}
