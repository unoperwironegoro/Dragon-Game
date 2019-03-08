using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Unoper.Unity.Utils;

namespace Unoper.Unity.DragonGame {

    public class ArenaSceneController : MonoBehaviour {

        [SerializeField] private UnityEvent OnGameEnd;
        [SerializeField] private UnityEvent OnRoundEnd;

        public GameObject dragonPrefab;
        public GameObject victoryPrefab;

        public AudioClip victorySound;

        private GameManager gManager;
        private CustomisationManager cManager;

        private GameObject[] dragons;
        private List<GameObject> alivePlayers = new List<GameObject>();

        private const float minSpawnSpaceX = 2f;

	    private void Start () {
            gManager = SingletonHelper.Find(SingletonEnums.GameManager).GetComponent<GameManager>();
            cManager = SingletonHelper.Find(SingletonEnums.CustomisationManager).GetComponent<CustomisationManager>();

            if (gManager) {

                var humanCount = gManager.GameData.HumanCount;
                var aiCount = gManager.GameData.AICount;

                var playersDragons = Enumerable
                    .Range(0, humanCount)
                    .Select(i => {
                        var dragon = CreateDragon(i);
                        DragonHelper.SetDragonAsPlayer(
                            cManager.GetPlayerCustomisation(i),
                            dragon);
                        return dragon;
                    });

                var aiDragons = Enumerable
                    .Range(humanCount, aiCount)
                    .Select(i => CreateDragon(i));

                dragons = playersDragons.Concat(aiDragons).ToArray();

            } else /* Standalone Scene Mode */ {
                dragons = FindObjectsOfType<DragonController>()
                    .Select(drc => drc.gameObject)
                    .ToArray();

                foreach(var dragon in dragons) {
                    alivePlayers.Add(dragon);
                    dragon.GetComponent<DamageController>().onDeath += OnDragonDeath;
                }
            }

            PositionPlayers();
	    }

        private GameObject CreateDragon(int playerID) {
            GameObject newDragon = Instantiate(dragonPrefab, Vector2.zero, Quaternion.identity);

            // Scene Logic
            alivePlayers.Add(newDragon);
            newDragon.GetComponent<DamageController>().onDeath += OnDragonDeath;

            // Populating dragon
            var dc = newDragon.GetComponent<DragonController>();
            dc.playerID = playerID;
            newDragon.GetComponent<Palette>().ColourSet = ColourSets.RandomColourSet();

            TrackDragonStats(playerID, dc);

            return newDragon;
        }

        private void TrackDragonStats(int playerID, DragonController dc) {
            var psd = gManager.GetPlayerStatsData(playerID);
            dc.OnFireball += () => psd.Fireballs++;
            dc.OnStomp += () => psd.Stomps++;
            dc.OnFlap += () => psd.Flaps++;
        }

        private void PositionPlayers() {
            Vector2[] positions = new Vector2[dragons.Length];
            for (int i = 0; i < positions.Length; i++) {
                var player = dragons[i];

                Vector2 spawnPos = Vector2.zero;
                for(int tries = 0; tries < 25; tries++) {
                    spawnPos = new Vector2(Random.Range(-5, 5), Random.Range(-4, 4));
                    float spawnSpaceX = float.MaxValue;
                    for (int j = 0; j < i; j++) {
                        spawnSpaceX = Mathf.Min(spawnSpaceX, Mathf.Abs((spawnPos - positions[j]).x));
                    }
                    if(spawnSpaceX >= minSpawnSpaceX) {
                        break;
                    }
                }

                positions[i] = spawnPos;
                player.transform.position = spawnPos;
            }
        }

        private void OnDragonDeath(GameObject deadDragon) {
            alivePlayers.Remove(deadDragon);
            deadDragon.GetComponent<DamageController>().onDeath -= OnDragonDeath;

            switch (alivePlayers.Count) {
                case 1:
                    var winnerDragon = alivePlayers[0];
                    winnerDragon.GetComponent<DamageController>().onDeath -= OnDragonDeath;
                    StartCoroutine("EndRound", winnerDragon);
                    break;
                case 0:
                    StartCoroutine("EndRound", null);
                    break;
            }
        }

        private IEnumerator EndRound(GameObject winnerDragon) {
            RewardWinner(winnerDragon);
            yield return new WaitForSeconds(3f);

            if(gManager != null && gManager.IsGameFinished()) {
                OnGameEnd.Invoke();
                yield break;
            }
            
            OnRoundEnd.Invoke();
        }

        private void RewardWinner(GameObject winnerDragon) {
            AudioSource.PlayClipAtPoint(victorySound, Camera.main.transform.position);

            if (winnerDragon != null) {
                Instantiate(victoryPrefab, winnerDragon.transform);
                var winnerID = winnerDragon.GetComponent<DragonController>().playerID;
                gManager.GetPlayerStatsData(winnerID).Wins += 1;
            }
        }
    }

}
