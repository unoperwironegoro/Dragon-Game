using UnityEditor;
using UnityEngine;

namespace Unoper.Unity.DragonGame {

    [CreateAssetMenu(fileName = "PlayerCustomisationData", menuName = "ScriptableObjects/PlayerCustomisation", order = 1)]
    public class PlayerCustomisationData : ScriptableObject {

        [SerializeField] public int ColourSetIndex;
        [SerializeField] public string LeftButton;
        [SerializeField] public string RightButton;

        public void SetDataToDragon(GameObject dragon) {
            PlayerInput pl = dragon.AddComponent<PlayerInput>();
            pl.leftKey = LeftButton;
            pl.rightKey = RightButton;

            var pal = dragon.GetComponent<Palette>();
            pal.ColourSet = ColourSets.colourSets[ColourSetIndex];
        }

        public static PlayerCustomisationData CreateDefault(int index) {
            return ConfigDirectory.LoadAsset<PlayerCustomisationData>(
                cd => cd.FOLDER_DEFAULT_CUSTOMISATION + (index + 1));
        }
    }

}