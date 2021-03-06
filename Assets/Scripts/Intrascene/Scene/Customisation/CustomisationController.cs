﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Unoper.Unity.Utils;

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

        private void Start () {
            CManager = SingletonHelper.Find(SingletonEnums.CustomisationManager)
                .GetComponent<CustomisationManager>();
            ChangePlayerCustomisationContext(0);
	    }

        public void UpdateDragon() {
            DragonHelper.SetDragonAsPlayer(customisationData, DemoDragon);
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
            DragonHelper.SetDragonAsPlayer(customisationData, DemoDragon);

            UpdateUI(playerID);

        }

        private void UpdateUI(int playerID) {
            int playerNum = playerID + 1;
            bool penultimate = playerNum == GameConstants.Instance.MAX_PLAYERS;
            bool first = playerNum == 1;

            next.enabled = !penultimate;
            prev.enabled = !first;

            title.text = "Player " + TextConstants.NUMBER_WORDS[playerID];
        }
    }
}