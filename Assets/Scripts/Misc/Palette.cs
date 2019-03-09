using UnityEngine;
using System.Collections;

namespace Unoper.Unity.DragonGame {
    [RequireComponent(typeof(SpriteRenderer))]
    [ExecuteInEditMode]
    public class Palette : MonoBehaviour, IColourable {
        [SerializeField]
        private Color[] palette = new Color[4];
        [SerializeField]
        private bool random;
        [SerializeField]
        private int colourPreset = -1;
        private int lastColourPreset = -1;

        public Color[] ColourSet {
            get { return palette; }
            set { palette = value; lastColourPreset = colourPreset;  UpdateShader(); }
        }

        private Material mat;
        private SpriteRenderer sr;

        private Color colourable = Color.white;

        private void Awake() {
            if (colourPreset <= 0 || random) {
                colourPreset = Random.Range(0, ColourCatalogue.ColourSets.Length);
            }
            UpdateShader();
        }

        private void OnApplicationFocus(bool focus) {
            if(focus) {
                UpdateShader();
            }
        }

        void OnEnable() {
            UpdateShader();
        }

        private void OnValidate() {
            UpdateShader();
        }

        private void Update() {
            if(colourPreset != lastColourPreset && colourPreset >= 0) {
                lastColourPreset = colourPreset;

                palette = ColourCatalogue.GetColourSet(colourPreset);
                UpdateShader();
            }
        }

        private void UpdateShader() {
            if (mat == null) {
                Shader shader = Shader.Find("Custom/PaletteSwap");
                mat = new Material(shader);

                sr = GetComponent<SpriteRenderer>();
                sr.sharedMaterial = mat;
            }

            Matrix4x4 cmx = new Matrix4x4();
            for (int i = 0; i < 4; i++) {
                Color c = Color.Lerp(colourable, palette[i], colourable.a);
                cmx.SetRow(i, ColorToVec(c));
            }
            mat.SetMatrix("_ColorMatrix", cmx);
        }

        private Vector4 ColorToVec(Color color) {
            return new Vector4(color.r, color.g, color.b, color.a);
        }

        public void AddColour(Color c) {
            colourable = c;
            UpdateShader();
        }

        public void ClearColour() {
            colourable = Color.white;
            UpdateShader();
        }
    }

}