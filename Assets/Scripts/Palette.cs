using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class Palette : MonoBehaviour {
    public Color[] palette = new Color[4];
    public int colourPreset = -1;
    private int lastColourPreset = -1;

    private Material mat;
    private SpriteRenderer sr;

    void OnEnable() {
        if (mat == null) {
            Shader shader = Shader.Find("Custom/PaletteSwap");
            mat = new Material(shader);
        }

        sr = GetComponent<SpriteRenderer>();
        sr.sharedMaterial = mat;

        if (colourPreset >= 0) {
            var colourSets = ColourSets.colourSets;
            palette = colourSets[colourPreset % colourSets.Length];
        }
        mat.SetMatrix("_ColorMatrix", colorMatrix);
    }

    void Update() {
        if (colourPreset != lastColourPreset && colourPreset >= 0) {
            var colourSets = ColourSets.colourSets;
            palette = colourSets[colourPreset % colourSets.Length];
        }
        mat.SetMatrix("_ColorMatrix", colorMatrix);
    }

    Matrix4x4 colorMatrix {
        get {
            Matrix4x4 mat = new Matrix4x4();
            for(int i = 0; i < 4; i++) {
                mat.SetRow(i, ColorToVec(palette[i]));
            }
            return mat;
        }
    }

    Vector4 ColorToVec(Color color) {
        return new Vector4(color.r, color.g, color.b, color.a);
    }
}