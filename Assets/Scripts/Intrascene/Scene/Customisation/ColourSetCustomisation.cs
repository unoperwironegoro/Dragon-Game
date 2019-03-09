using UnityEngine;
using UnityEngine.UI;

namespace Unoper.Unity.DragonGame {

    public class ColourSetCustomisation : MonoBehaviour {

        private const int colourFillIndex = 0;
        private const int colourBorderIndex = 3;

        [SerializeField] private CustomisationController customisationController;
        private PlayerCustomisationData customisationData;

        // Colour Selection
        [SerializeField] private Image colourBoxBorder;
        [SerializeField] private Image colourBoxFill;
        private int colourSetIndex;

        public void SetContext(PlayerCustomisationData customisationData) {
            this.customisationData = customisationData;
            UpdateUIColours(customisationData.ColourSetIndex);
        }

        public void ColourSwitch() {
            colourSetIndex = (colourSetIndex + 1) % ColourCatalogue.ColourSets.Length;

            UpdateUIColours(colourSetIndex);

            customisationData.ColourSetIndex = colourSetIndex;
            customisationController.UpdateDragon();
        }

        private void UpdateUIColours(int colourSetIndex) {
            var colourSet = ColourCatalogue.GetColourSet(colourSetIndex);

            Color cFill = colourSet[colourFillIndex];
            cFill.a = 1;
            colourBoxFill.color = cFill;

            Color cBorder = colourSet[colourBorderIndex];
            cBorder.a = 1;
            colourBoxBorder.color = cBorder;
        }
    }
}
