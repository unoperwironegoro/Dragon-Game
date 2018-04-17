using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonConfig {
    public Color[] colourset;
    public string leftButton;
    public string rightButton;

    public DragonConfig() {
        colourset = ColourSets.devilSet;
        leftButton = "z";
        rightButton = "x";
    }

    public void SetDataToDragon(GameObject dragon) {
        PlayerInput pl = dragon.AddComponent<PlayerInput>();
        pl.leftKey = leftButton;
        pl.rightKey = rightButton;

        var pal = dragon.GetComponent<Palette>();
        pal.ColourSet = colourset; 
    }
}