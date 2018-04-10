using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationController : MonoBehaviour {
    [SerializeField]
    private PlayerInput pinput;
    [SerializeField]
    private Palette palette;

    private Text keyText;
    [SerializeField]
    private Text leftText;
    [SerializeField]
    private Text rightText;

    private int colourIndex;
    private Color[] colourSet = ColourSets.colourSets[0];
    [SerializeField]
    private Image colourBox1;
    [SerializeField]
    private Image colourBox2;

    [SerializeField]
    private Text title;
    [SerializeField]
    private Text next;
    private GameData gdata;

    private DragonData ddatum;

    private string lastScene;
    private string nextScene;

    private static string[] numberWords = { "One", "Two", "Three", "Four" };
    private const string puncInput = "`-=[];\\,./";

	void Start () {
        ddatum = new DragonData();
        ddatum.leftButton = leftText.text;
        ddatum.rightButton = rightText.text;
        ddatum.colourset = palette.palette;

        gdata = FindObjectOfType<GameData>();
        int playerNum = gdata.setup + 1;
        bool penultimate = gdata.playerCount == playerNum;
        bool first = playerNum == 1;

        next.text = penultimate? "Done" : "Next";
        lastScene = first? "Menu" : "Customisation";
        nextScene = penultimate? "Arena1" : "Customisation";

        title.text = "Player " + numberWords[playerNum - 1];
	}
	
	public void Back () {
        gdata.setup--;
        SceneTransitionController.InstantScene(lastScene);
	}

    public void Next() {
        gdata.ddata[gdata.setup] = ddatum;
        gdata.setup++;
        SceneTransitionController.InstantScene(nextScene);
    }

    public void GetInput(Text text) {
        keyText = text;
    }

    private void Update() {
        if(!keyText) {
            return;
        }

        string newKey = GetKey();
        if(newKey != null) {
            keyText.text = newKey;
            if(keyText == leftText) {
                pinput.leftKey = newKey;
                ddatum.leftButton = newKey;
            } else /* rightText */ {
                pinput.rightKey = newKey;
                ddatum.rightButton = newKey;
            }
            keyText = null;
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

    public void ColourSwitch() {
        colourIndex++;
        colourIndex %= ColourSets.colourSets.Length;
        colourSet = ColourSets.colourSets[colourIndex];

        palette.colourPreset = colourIndex;
        ddatum.colourset = colourSet;

        Color c = colourSet[0];
        c.a = 1;
        colourBox1.color = c;
        c = colourSet[3];
        c.a = 1;
        colourBox2.color = c;
    }
}
