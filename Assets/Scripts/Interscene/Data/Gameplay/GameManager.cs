using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unoper.Unity.DragonGame {

    [RequireComponent(typeof(SingletonDataObject))]
    public class GameManager : MonoBehaviour {

        [SerializeField] private GameData gameData;
        [SerializeField] private PlayerStatsData[] playerStatsData;

        public int EntityCount { get { return gameData.HumanCount + gameData.AICount; } }
        public GameData GameData { get { return gameData; } }

        private PlayerStatsData initialStats;

        private void Awake() {
            initialStats = ConfigDirectory.LoadAsset<PlayerStatsData>(cd => cd.ASSET_DEFAULT_STATS);

            if(playerStatsData == null) {
                ResetStats();
            } else {
                InitialiseNullStats();
            }
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

        public void ResetStats() {
            playerStatsData = Enumerable.Range(0, GameConstants.Instance.MAX_PLAYERS)
                .Select(_ => Instantiate(initialStats))
                .ToArray();
        }

        public T QueryStats<T>(Func<IEnumerable<PlayerStatsData>, T> query) {
            return query(playerStatsData);
        }

        private void InitialiseNullStats() {
            Array.Resize(ref playerStatsData, GameConstants.Instance.MAX_PLAYERS);
            for (int i = 0; i < playerStatsData.Length; i++) {
                if (playerStatsData[i] == null) {
                    playerStatsData[i] = Instantiate(initialStats);
                }
            }
        }
    }
}
