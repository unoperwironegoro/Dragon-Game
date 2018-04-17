using UnityEngine;

public static class ColourSets {
    public static Color[][] colourSets { get { return new[] { fireSet, iceSet, ghostSet, forestSet, devilSet }; } }

    public static Color[] fireSet { get { return new Color[] { new Color32(45, 3, 0, 0), new Color32(68, 0, 0, 0), new Color32(171, 85, 39, 0), new Color32(255, 73, 19, 0) }; } }
    public static Color[] iceSet { get { return new Color[] { new Color32(10, 42, 73, 0), new Color32(39, 0, 139, 0), new Color32(57, 141, 158, 0), new Color32(255, 255, 255, 0) }; } }
    public static Color[] ghostSet { get { return new Color[] { new Color32(255, 255, 255, 0), new Color32(234, 167, 224, 0), new Color32(114, 11, 169, 0), new Color32(0, 0, 0, 0) }; } }
    public static Color[] forestSet { get { return new Color[] { new Color32(204, 255, 124, 0), new Color32(229, 218, 69, 0), new Color32(32, 88, 27, 0), new Color32(69, 143, 60, 0) }; } }
    public static Color[] devilSet { get { return new Color[] { new Color32(2, 14, 36, 0), new Color32(4, 4, 8, 0), new Color32(18, 60, 77, 0), new Color32(255, 24, 24, 0) }; } }

    public static Color[] RandomColourSet() {
        return colourSets[Random.Range(0, colourSets.Length)];
    }
}