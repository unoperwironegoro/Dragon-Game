using UnityEngine;

namespace Unoper.Unity.DragonGame {

    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
    public class GameData : ScriptableObject {
        [SerializeField] public int HumanCount;
        [SerializeField] public int AICount;
        [SerializeField] public int RoundsToWin;
    }
}