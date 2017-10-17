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
public class ArenaData : MonoBehaviour {
    public struct PlayerData {
        public DragonData ddata;
        public int score;
    }

    public PlayerData[] pdata;
    public int roundsRemaining;

	void Start () {
        DontDestroyOnLoad(gameObject);
	}
}
