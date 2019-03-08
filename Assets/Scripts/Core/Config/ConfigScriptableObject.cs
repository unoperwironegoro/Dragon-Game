using System;
using UnityEditor;
using UnityEngine;
using Unoper.Unity.DragonGame;

/// <summary>
/// A singleton designed to be populated from a Scriptable Object asset.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ConfigScriptableObject<T> : ScriptableObject
    where T : ScriptableObject {

    private static T Instance;

    /// <summary>
    /// Return the Instance if it exists, otherwise load from an asset at the given path.
    /// </summary>
    protected static T GetOrCreateFromAsset(string assetPath) {
        if (Instance == null) {
            Instance = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            Debug.Log(string.Format(
                "Loaded {0} singleton from {1}.",
                typeof(T).Name,
                assetPath));
        }
        return Instance;
    }

    /// <summary>
    /// Return the Instance if it exists, otherwise load through the ConfigDirectory.
    /// </summary>
    protected static T GetOrLookup(Func<ConfigDirectory, string> lookup) {
        if (Instance == null) {
            Instance = ConfigDirectory.LoadAsset<T>(lookup);
        }
        return Instance;
    }
}
