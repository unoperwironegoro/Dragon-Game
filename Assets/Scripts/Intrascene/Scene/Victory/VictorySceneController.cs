using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Unoper.Unity.DragonGame;
using Unoper.Unity.Utils;

public class VictorySceneController : MonoBehaviour {

    [SerializeField] private GameObject dragon;
    [SerializeField] private Text titleText;
    [SerializeField] private Text statsText;

    private GameManager gManager;
    private CustomisationManager cManager;

    private void Start() {
        gManager = SingletonHelper.Find(SingletonEnums.GameManager).GetComponent<GameManager>();
        cManager = SingletonHelper.Find(SingletonEnums.CustomisationManager).GetComponent<CustomisationManager>();

        var winnerID = gManager.QueryStats(GetWinningOrder).First();

        if (winnerID < gManager.GameData.HumanCount) {
            DragonHelper.SetDragonAsPlayer(cManager.GetPlayerCustomisation(winnerID), dragon);
        }

        UpdateDisplay(winnerID, gManager.GetPlayerStatsData(winnerID));
    }

    private void UpdateDisplay(int winnerID, PlayerStatsData psd) {
        statsText.text = string.Format(
            "{0}\n{1}\n{2}",
            psd.Fireballs,
            psd.Flaps,
            psd.Stomps);

        titleText.text = string.Format("Player {0} Wins", TextConstants.NUMBER_WORDS[winnerID]);
    }

    private void SetDragon() {

    }

    private IEnumerable<int> GetWinningOrder(IEnumerable<PlayerStatsData> psds) {
        var winningOrder = psds
            .Select((V, Index) => new { V.Wins, Index })
            .ToList();

        winningOrder.Sort((b, a) => (a.Wins).CompareTo(b.Wins));

        return winningOrder.Select(res => res.Index);
    }
}
