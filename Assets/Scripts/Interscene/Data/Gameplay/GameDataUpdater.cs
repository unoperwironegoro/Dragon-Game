using UnityEngine;
using Unoper.Unity.Utils;

namespace Unoper.Unity.DragonGame {

    public class GameDataUpdater : MonoBehaviour {

        private GameManager gManager;

	    private void Start () {
            gManager = SingletonHelper.Find(SingletonEnums.GameManager).GetComponent<GameManager>();
	    }

        public void SetGameData(GameData gd) {
            gManager.SetGameData(gd);
        }
    }

}
