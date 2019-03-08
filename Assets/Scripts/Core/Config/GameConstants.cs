using UnityEditor;
using UnityEngine;

namespace Unoper.Unity.DragonGame {

    [CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
    public class GameConstants : ConfigScriptableObject<GameConstants> {
        [SerializeField] public int MAX_PLAYERS = 4;
        
        public static GameConstants Instance { get { return GetOrLookup(cd => cd.ASSET_GAME_CONSTANTS); } }
    }
}
