using UnityEditor;
using UnityEngine;

namespace Inscept.SettingsBundle
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
                EditorGUILayout.HelpBox(
                    $"Settings bundle asset path should be '{AssetPath}' to be compiled.",
                    MessageType.Warning);
            }

            if (GUILayout.Button("Export"))
            {
                var outputPath = EditorUtility.OpenFolderPanel("Export Settings Bundle", "", "Settings.bundle");
                if (string.IsNullOrEmpty(outputPath))
                    return;
                
                var settingsBundle = (SettingsBundle)target;
                settingsBundle.Export(outputPath);
                
                // Settings bundle might be saved under Assets folder.
                // So it needs to be refreshed to make it visible.
                AssetDatabase.Refresh();
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