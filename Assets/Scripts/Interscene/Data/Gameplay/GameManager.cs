using System.Linq;
using UnityEngine;

namespace Unoper.Unity.DragonGame {

    [RequireComponent(typeof(SingletonDataObject))]
    public class GameManager : MonoBehaviour {

        [SerializeField] private GameData gameData;
        [SerializeField] private PlayerStatsData[] playerStatsData;

        public int EntityCount { get { return gameData.HumanCount + gameData.AICount; } }
        public GameData GameData { get { return gameData; } }

        private void Awake() {
            var initialStats = ConfigDirectory.LoadAsset<PlayerStatsData>(cd => cd.ASSET_DEFAULT_STATS);

            playerStatsData = Enumerable.Range(0, GameConstants.Instance.MAX_PLAYERS)
                .Select(_ => Instantiate(initialStats))
                .ToArray();
        }

        public PlayerStatsData GetPlayerStatsData(int i) {
            return playerStatsData[i];
        }

        public bool IsGameFinished() {
            return playerStatsData.Any(s => s.Wins >= gameData.RoundsToWin);
        }

        public void SetGameData(GameData gd) {
            gameData = gd;
        }
    }
}
