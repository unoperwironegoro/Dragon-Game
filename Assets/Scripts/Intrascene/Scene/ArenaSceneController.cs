using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaSceneController : MonoBehaviour {

    public GameObject dragonPrefab;
    public GameObject victoryPrefab;

    public AudioClip victorySound;

    private ArenaData adata;
    private GameData gdata;
    private GameObject[] dragons;
    private List<GameObject> alivePlayers = new List<GameObject>();

    private const float minSpawnSpaceX = 2f;

	void Start () {
        // Read information from the previous scene held by the ArenaData
        adata = FindObjectOfType<ArenaData>();
        gdata = GameData.Instance;

        if (gdata && adata) {
            int playerCount = GameData.PlayerCount;
            //int aiCount = adata.dragonCount - playerCount;
            //dragons = new GameObject[adata.dragonCount];
            
            //TEMP 
            int aiCount = GameData.PlayerCount == 1? 1 : 0;
            dragons = new GameObject[adata.dragonCount + aiCount];
            
            for (int i = 0; i < playerCount; i++) {
                dragons[i] = GameData.InjectPlayerDataToDragon(i, CreateDragon(i));
            }
            for (int i = playerCount; i < dragons.Length; i++) {
                dragons[i] = CreateDragon(1);
            }

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
        dragons[playerID] = newDragon;
        alivePlayers.Add(newDragon);
        newDragon.GetComponent<DamageController>().onDeath += OnDragonDeath;

        // Populating dragon
        newDragon.GetComponent<DragonController>().playerID = playerID;
        newDragon.GetComponent<Palette>().ColourSet = ColourSets.RandomColourSet();
        return newDragon;
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
        AudioSource.PlayClipAtPoint(victorySound, Camera.main.transform.position);
        if(winnerDragon != null) {
            Instantiate(victoryPrefab, winnerDragon.transform);
        }

        yield return new WaitForSeconds(3f);

        if (adata) {
            if (winnerDragon != null) {
                adata.pdata[winnerDragon.GetComponent<DragonController>().playerID].score += 1;
            }
            adata.roundsRemaining -= 1;
            if(adata.roundsRemaining == 0) {
                SceneSwitcher.Transition("MenuMain");
                //TEMP
                Destroy(adata);
                yield break;
            }
        }

        SceneSwitcher.Transition("Arena1");
    }
}
