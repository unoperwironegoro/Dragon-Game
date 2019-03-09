using System;
using UnityEditor;
using UnityEngine;

namespace Unoper.Unity.DragonGame {

    [CreateAssetMenu(fileName = "ConfigDirectory", menuName = "ScriptableObjects/ConfigDirectory", order = 1)]
    public class ConfigDirectory : ConfigScriptableObject<ConfigDirectory> {
        public static string ROOT = "Assets/Config/";
        [SerializeField] public string FOLDER_DEFAULT_CUSTOMISATION;
        [SerializeField] public string ASSET_GAME_CONSTANTS;
        [SerializeField] public string ASSET_PLAYERPREF_KEYS;
        [SerializeField] public string ASSET_DEFAULT_STATS;
        [SerializeField] public string ASSET_COLOURSET;

        public static ConfigDirectory Instance { get { return GetOrCreateFromAsset(ROOT + "ConfigDirectory.asset"); } }


        /// <summary>
        /// Load an asset through the ConfigDirectory.
        /// </summary>
        public static T LoadAsset<T>(Func<ConfigDirectory, string> lookup) where T : ScriptableObject {
            string assetPath = ROOT + lookup(Instance) + ".asset";
            Debug.Log(string.Format(
                "Loading {0} Asset from {1}.",
                typeof(T).Name,
                assetPath));
            return AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }
    }
}
