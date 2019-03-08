using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Unoper.Unity.DragonGame {

    public class CustomisationController : MonoBehaviour {
        [SerializeField] GameObject DemoDragon;

        // Navigation
        [SerializeField] private Text next;
        [SerializeField] private Text prev;

        // Current player being customised
        [SerializeField] private Text title;
        [SerializeField] private int playerID;

        [Serializable] public class PlayerCustomisationContextChangeEvent : UnityEvent<PlayerCustomisationData> {}
        [SerializeField] private PlayerCustomisationContextChangeEvent OnPlayerChange;

        private CustomisationManager CManager;
        private PlayerCustomisationData customisationData;

        private static string[] numberWords = { "One", "Two", "Three", "Four" };

        private void Start () {
            CManager = FindObjectOfType<CustomisationManager>();
            ChangePlayerCustomisationContext(0);
	    }

        public void UpdateDragon() {
            customisationData.SetDataToDragon(DemoDragon);
        }
	
	    public void Back() {
            if(!prev.enabled) {
                return;
            }
            ChangePlayerCustomisationContext(playerID - 1);
	    }

        public void Next() {
            if (!next.enabled) {
                return;
            }
            ChangePlayerCustomisationContext(playerID + 1);
        }

        public void Save() {
            CManager.SaveCustomisations();
        }

        private void ChangePlayerCustomisationContext(int playerID) {
            this.playerID = playerID;

            customisationData = CManager.GetPlayerCustomisation(playerID);
            OnPlayerChange.Invoke(customisationData);
            customisationData.SetDataToDragon(DemoDragon);

            UpdateUI(playerID);

        }

        private void UpdateUI(int playerID) {
            int playerNum = playerID + 1;
            bool penultimate = GameData.PlayerCount == playerNum;
            bool first = playerNum == 1;

            next.enabled = !penultimate;
            prev.enabled = !first;

            title.text = "Player " + numberWords[playerID];
        }
    }
}