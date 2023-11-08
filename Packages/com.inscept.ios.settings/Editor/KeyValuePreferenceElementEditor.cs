using UnityEditor;

namespace Inscept.iOS.Settings
{
    [CustomEditor(typeof(KeyValuePreferenceElement<>), editorForChildClasses: true)]
    public class KeyValuePreferenceElementEditor : Editor
    {
        private SerializedProperty _identifier;

        private void OnEnable()
        {
            _identifier = serializedObject.FindProperty("_identifier");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (string.IsNullOrWhiteSpace(_identifier.stringValue))
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("Identifier is required.\n" +
                                        "This is the string you use this to retrieve " +
                                        "the preference value from the PlayerPrefs.", MessageType.Warning);
            }
        }
    }
}