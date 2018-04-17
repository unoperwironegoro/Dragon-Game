﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : SingletonBehaviour<GameData> {
    [SerializeField]
    private int playerCount;
    private const int maxPlayers = 4;
    public static int PlayerCount { set { Instance.playerCount = value; } get { return Instance.playerCount; } }

    // Arbitrary # players
    public DragonConfig[] dcfg = new DragonConfig[maxPlayers];

    protected override void Awake() {
        if(!AcquireSingletonStatus()) {
            return;
        }
        for(int i = 0; i < dcfg.Length; i++) {
            dcfg[i] = new DragonConfig();
        }
    }

    public static GameObject InjectPlayerDataToDragon(int playerID, GameObject dragon) {
        Instance.dcfg[playerID].SetDataToDragon(dragon);
        return dragon;
    }

    public static DragonConfig GetPlayerData(int playerID) {
        return Instance.dcfg[playerID];
    }

    protected override void OnInstanceFound() {
        Destroy(this);
    }
}
