using System.IO;
using UnityEditor;
using UnityEngine;

namespace Inscept.iOS.Settings
{
    [CustomEditor(typeof(SettingsBundle))]
    public class SettingsBundleEditor : Editor
    {
        private const string AssetPath = "Assets/Editor/SettingsBundle.asset";
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var path = AssetDatabase.GetAssetPath(target);
            
            if (AssetPath != path)
            {
                Debug.Log(path);
                EditorGUILayout.HelpBox(
                    $"Settings bundle asset path should be '{AssetPath}' to be compiled.",
                    MessageType.Warning);
            }

            if (GUILayout.Button("Export"))
            {
                var settingsBundle = (SettingsBundle)target;
                settingsBundle.Export("D:/");
            }
        }
        
        public static bool TryGetSettingsBundle(out SettingsBundle settingsBundle)
        {
            settingsBundle = AssetDatabase.LoadAssetAtPath<SettingsBundle>(AssetPath);
            return settingsBundle != null;
        }
        
        public static SettingsBundle GetSettingsBundle()
        {
            return AssetDatabase.LoadAssetAtPath<SettingsBundle>(AssetPath);
        }
    }
}