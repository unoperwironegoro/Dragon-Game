
using UnityEngine;

namespace Unoper.Unity.DragonGame {

    [CreateAssetMenu(fileName = "PlayerPrefKeys", menuName = "ScriptableObjects/PlayerPrefKeys", order = 1)]
    public class PlayerPrefKeys : ConfigScriptableObject<PlayerPrefKeys> {
        [SerializeField] public string CUSTOMISATION_PLAYER_X = "CUSTOMISATION_PLAYER_#";

        public static PlayerPrefKeys Instance { get { return GetOrLookup(cd => cd.ASSET_PLAYERPREF_KEYS); } }
    }
}