using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : SingletonBehaviour<GameData> {
    public int playerCount;
    public int setup;
    // Arbitrary # players
    public DragonData[] ddata = new DragonData[4];

    protected override void Singlify() {
        Destroy(this);
    }

    protected override GameData GetInstance() {
        return this;
    }
}
