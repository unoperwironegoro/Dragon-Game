using UnityEngine;


namespace Unoper.Unity.DragonGame {

    [CreateAssetMenu(fileName = "ColourCatalogue", menuName = "ScriptableObjects/ColourCatalogue", order = 1)]
    public class ColourCatalogue : ConfigScriptableObject<ColourCatalogue> {

        [SerializeField] private ColourSet[] Sets;

        public static ColourSet[] ColourSets { get { return Instance.Sets; } }

        public static Color[] GetColourSet(int index) {
            return ColourSets[index % ColourSets.Length].ToArray();
        }

        public static Color[] RandomColourSet() {
            return ColourSets[Random.Range(0, ColourSets.Length)].ToArray();
        }

        public static ColourCatalogue Instance { get { return GetOrLookup(cd => cd.ASSET_COLOURSET); ; } }
    }
}