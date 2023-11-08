using UnityEditor;
using UnityEngine;

namespace Inscept.iOS.Settings
{
    [CustomPropertyDrawer(typeof(ValueReference<>))]
    public class ValueReferencePropertyDrawer : PropertyDrawer
    {
        // Options to display in the popup to select constant or variable.
        private readonly string[] _popupOptions = { "Use Constant", "Use Variable" };

        // Cached style to use to draw the popup button.
        private GUIStyle _popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_popupStyle == null)
            {
                _popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                _popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
            
            EditorGUI.BeginChangeCheck();

            // Get properties
            var useConstant = property.FindPropertyRelative("_useConstant");
            var constantValue = property.FindPropertyRelative("_constantValue");
            var variable = property.FindPropertyRelative("_variable");

            // Calculate rect for configuration button
            var buttonRect = new Rect(position);
            buttonRect.yMin += _popupStyle.margin.top;
            buttonRect.width = _popupStyle.fixedWidth + _popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, _popupOptions, _popupStyle);

            useConstant.boolValue = result == 0;

            EditorGUI.PropertyField(position, useConstant.boolValue ? constantValue : variable, GUIContent.none);

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }
            
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}