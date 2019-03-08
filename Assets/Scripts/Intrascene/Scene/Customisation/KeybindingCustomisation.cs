using UnityEngine;
using UnityEngine.UI;

namespace Unoper.Unity.DragonGame {

    public class KeybindingCustomisation : MonoBehaviour {

        [SerializeField] private Text keyText;
        [SerializeField] private Text leftText;
        [SerializeField] private Text rightText;

        [SerializeField] private CustomisationController customisationController;
        private PlayerCustomisationData customisationData;

        private const string puncInput = "`-=[];\\,./";

        public void SetContext(PlayerCustomisationData customisationData) {
            this.customisationData = customisationData;
            leftText.text = customisationData.LeftButton;
            rightText.text = customisationData.RightButton;
        }

        public void SetLRContext(Text text) {
            keyText = text;
        }

        private void Update() {
            if (!keyText) {
                return;
            }

            string newKey = GetKey();
            if (newKey != null) {
                keyText.text = newKey;
                if (keyText == leftText) {
                    customisationData.LeftButton = newKey;
                } else /* rightText */ {
                    customisationData.RightButton = newKey;
                }
                keyText = null;

                customisationController.UpdateDragon();
            }
        }

        private string GetKey() {
            foreach (char c in Input.inputString) {
                char lc = char.ToLower(c);
                if ('a' <= lc && c <= 'z') {
                    return lc.ToString();
                }

                if (puncInput.Contains(c.ToString())) {
                    return c.ToString();
                }
            }
            return null;
        }
    }
}
