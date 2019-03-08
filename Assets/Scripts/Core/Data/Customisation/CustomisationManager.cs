using UnityEditor;
using UnityEngine;

namespace Unoper.Unity.DragonGame {

    public class CustomisationManager : MonoBehaviour {

        [SerializeField] private PlayerCustomisationData[] PlayerCustomisations = new PlayerCustomisationData[4];

        private void Start() {
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
            
            var data = CreateDefaultPlayerCustomisation(index);
            JsonUtility.FromJsonOverwrite(value, data);

            return data;
        }

        private void SavePlayerCustomisation(int index, PlayerCustomisationData data) {
            string key = GetPrefsKeyForPlayer(index);
            string value = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, value);
        }

        private string GetPrefsKeyForPlayer(int index) {
            return PlayerPrefKeys.CUSTOMISATION_PLAYER_X + index;
        }

        private PlayerCustomisationData CreateDefaultPlayerCustomisation(int index)
        {
            return AssetDatabase.LoadAssetAtPath<PlayerCustomisationData>(
                string.Format("Assets/Config/Data/DefaultPlayerCustomisation/{0}.asset", index + 1));
        }
    }
}
