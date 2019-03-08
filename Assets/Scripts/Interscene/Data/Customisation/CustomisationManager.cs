using UnityEditor;
using UnityEngine;

namespace Unoper.Unity.DragonGame {

    [RequireComponent(typeof(SingletonDataObject))]
    public class CustomisationManager : MonoBehaviour {

        [SerializeField] private PlayerCustomisationData[] PlayerCustomisations;

        private void Start() {
            var maxPlayers = GameConstants.Instance.MAX_PLAYERS;
            PlayerCustomisations = new PlayerCustomisationData[maxPlayers];

            // Load Player Customisations
            for (int i = 0; i < PlayerCustomisations.Length; i++) {
                if (PlayerCustomisations[i] == null) {
                    PlayerCustomisations[i] = LoadPlayerCustomisation(i);
                }
            }
        }

        public PlayerCustomisationData GetPlayerCustomisation(int i) {
            return PlayerCustomisations[i];
        }

        public void SetPlayerCustomisation(int i, PlayerCustomisationData data) {
            PlayerCustomisations[i] = data;
        }

        public void SaveCustomisations() {
            for (int i = 0; i < PlayerCustomisations.Length; i++) {
                SavePlayerCustomisation(i, PlayerCustomisations[i]);
            }
        }

        private PlayerCustomisationData LoadPlayerCustomisation(int index) {
            string key = GetPrefsKeyForPlayer(index);
            string value = PlayerPrefs.GetString(key);
            
            var data = PlayerCustomisationData.CreateDefault(index);
            JsonUtility.FromJsonOverwrite(value, data);

            return data;
        }

        private void SavePlayerCustomisation(int index, PlayerCustomisationData data) {
            string key = GetPrefsKeyForPlayer(index);
            string value = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, value);
        }

        private string GetPrefsKeyForPlayer(int index) {
            return PlayerPrefKeys.Instance.CUSTOMISATION_PLAYER_X + index;
        }
    }
}
