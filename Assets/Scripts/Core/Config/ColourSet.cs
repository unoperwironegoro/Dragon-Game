using UnityEngine;

namespace Unoper.Unity.DragonGame {

    [CreateAssetMenu(fileName = "ColourSet", menuName = "ScriptableObjects/ColourSet", order = 1)]
    public class ColourSet : ScriptableObject {
        [SerializeField] public Color colour1;
        [SerializeField] public Color colour2;
        [SerializeField] public Color colour3;
        [SerializeField] public Color colour4;

        public Color[] ToArray() {
            return new[] {
                colour1,
                colour2,
                colour3,
                colour4
            };
        }
    }
}