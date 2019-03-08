using UnityEngine;

namespace Unoper.Unity.DragonGame {

    [CreateAssetMenu(fileName = "PlayerStatsData", menuName = "ScriptableObjects/PlayerStatsData", order = 1)]
    public class PlayerStatsData : ScriptableObject {
        [SerializeField] public int Wins;
        [SerializeField] public int Fireballs;
        [SerializeField] public int Flaps;
        [SerializeField] public int Stomps;
    }
}
