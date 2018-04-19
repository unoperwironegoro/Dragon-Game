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
    private Image colourBoxBorder;
    [SerializeField]
    private Image colourBoxFill;

    [SerializeField]
    private Text title;
    [SerializeField]
    private Text next;
    [SerializeField]
    private Text prev;

    [SerializeField]
    private int playerID;

    private DragonConfig dc;

    private const string thisSceneName = "Customisation";

    private static string[] numberWords = { "One", "Two", "Three", "Four" };
    private const string puncInput = "`-=[];\\,./";
    private const int colourFillIndex = 0;
    private const int colourBorderIndex = 3;

    void Start () {
        ChangePlayer(0);
	}

    private void ChangePlayer(int playerID) {
        this.playerID = playerID;

        dc = GameData.GetPlayerData(playerID);
        leftText.text = dc.leftButton;
        rightText.text = dc.rightButton;

        Color c = dc.colourset[colourFillIndex];
        c.a = 1;
        colourBoxFill.color = c;
        c = dc.colourset[colourBorderIndex];
        c.a = 1;
        colourBoxBorder.color = c;
        
        dc.SetDataToDragon(pinput.gameObject);

        int playerNum = playerID + 1;
        bool penultimate = GameData.PlayerCount == playerNum;
        bool first = playerNum == 1;

        next.enabled = !penultimate;
        prev.enabled = !first;

        title.text = "Player " + numberWords[playerNum - 1];
    }
	
	public void Back() {
        if(!prev.enabled) {
            return;
        }
        ChangePlayer(playerID - 1);
	}

    public void Done(string finishScene) {
        SceneSwitcher.InstantSwitch(finishScene, gameObject.scene.name, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void Next() {
        if (!next.enabled) {
            return;
        }
        ChangePlayer(playerID + 1);
    }

    public void SetLRContext(Text text) {
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
                dc.leftButton = newKey;
            } else /* rightText */ {
                pinput.rightKey = newKey;
                dc.rightButton = newKey;
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
        colourIndex = (colourIndex + 1) % ColourSets.colourSets.Length;
        colourSet = ColourSets.colourSets[colourIndex];

        palette.ColourSet = colourSet;
        dc.colourset = colourSet;

        Color c = colourSet[colourBorderIndex];
        c.a = 1;
        colourBoxBorder.color = c;
        c = colourSet[colourFillIndex];
        c.a = 1;
        colourBoxFill.color = c;
    }
}
