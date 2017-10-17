using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaSceneController : MonoBehaviour {
    public GameObject dragonPrefab;
    public GameObject victoryPrefab;

    public AudioClip victorySound;

    private ArenaData gm; 
    private GameObject[] players;
    private List<GameObject> alivePlayers = new List<GameObject>();

    private const float minSpawnSpaceX = 2f;

	void Start () {
        // Read information from the previous scene held by the ArenaGameManager
        gm = FindObjectOfType<ArenaData>();

        if(gm) {
            players = new GameObject[gm.pdata.Length];

            for(int i = 0; i < players.Length; i++) {
                GameObject newDragon = Instantiate(dragonPrefab, Vector2.zero, Quaternion.identity);

                // Scene Logic
                players[i] = newDragon;
                alivePlayers.Add(newDragon);
                newDragon.GetComponent<DamageController>().onDeath += OnDragonDeath;

                // Populating dragon
                newDragon.GetComponent<DragonController>().playerID = i;
                gm.pdata[i].ddata.SetDataTo(newDragon);
            }
        } else /* Standalone Scene Mode */ {
            players = FindObjectsOfType<DragonController>()
                .Select(drc => drc.gameObject)
                .ToArray();

            foreach(var dragon in players) {
                alivePlayers.Add(dragon);
                dragon.GetComponent<DamageController>().onDeath += OnDragonDeath;
            }
        }

        PositionPlayers();
	}

    private void PositionPlayers() {
        Vector2[] positions = new Vector2[players.Length];
        for (int i = 0; i < positions.Length; i++) {
            var player = players[i];

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
        AudioSource.PlayClipAtPoint(victorySound, Camera.main.transform.position);
        if(winnerDragon != null) {
            Instantiate(victoryPrefab, winnerDragon.transform);
        }

        yield return new WaitForSeconds(3f);

        if (gm) {
            if (winnerDragon != null) {
                gm.pdata[winnerDragon.GetComponent<DragonController>().playerID].score += 1;
            }
            gm.roundsRemaining -= 1;
            if(gm.roundsRemaining == 0) {
                SceneTransitionController.ChangeScene("ArenaEnd");
                yield break;
            }
        }

        SceneTransitionController.ChangeScene("Arena1");
    }
}
