using Evolutex.Evolunity.Structs;
using UnityEditor;
using UnityEngine;

namespace Evolutex.Evolunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(FloatRange))]
    public class FloatRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float tempLabelWidth = EditorGUIUtility.labelWidth;

            EditorGUI.LabelField(position, property.displayName);

            float valueX = tempLabelWidth;
            float valueWidth = position.width - tempLabelWidth;
            float compWidth = 0.5f * valueWidth;

            EditorGUIUtility.labelWidth = 45.0f;
            EditorGUI.PropertyField(new Rect(valueX, position.y, compWidth, position.height),
                property.FindPropertyRelative("Min"));
            EditorGUI.PropertyField(new Rect(valueX + compWidth, position.y, compWidth, position.height),
                property.FindPropertyRelative("Max"));
            EditorGUIUtility.labelWidth = tempLabelWidth;

            EditorGUI.EndProperty();
        }
    }
}