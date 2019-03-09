using UnityEngine;

namespace Unoper.Unity.DragonGame {

    public static class DragonHelper {

        public static void SetDragonAsPlayer(
            PlayerCustomisationData cData, 
            GameObject dragon, 
            bool changeColour = true) {

            PlayerInput pl = dragon.AddComponent<PlayerInput>();
            pl.leftKey = cData.LeftButton;
            pl.rightKey = cData.RightButton;

            if(changeColour) {
                Palette pal = dragon.GetComponent<Palette>();
                pal.ColourSet = ColourCatalogue.GetColourSet(cData.ColourSetIndex);
            }
        }

    }
}