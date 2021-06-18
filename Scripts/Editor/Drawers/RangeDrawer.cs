// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using Evolutex.Evolunity.Extensions;
using Evolutex.Evolunity.Structs;
using UnityEditor;
using UnityEngine;

namespace Evolutex.Evolunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(FloatRange))]
    [CustomPropertyDrawer(typeof(IntRange))]
    public class RangeDrawer : PropertyDrawer
    {
        // TODO: Change rects getting to cutting.
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Rect labelRect = position.WithWidth(EditorGUIUtility.labelWidth);
            EditorGUI.LabelField(labelRect, property.displayName);

            Rect valueRect = position.TranslateX(labelRect.width).AddWidth(-labelRect.width);
            Rect minValueRect = valueRect.WithWidth(valueRect.width / 2);
            Rect maxValueRect = valueRect.TranslateX(valueRect.width / 2).AddWidth(-valueRect.width / 2);
            
            EditorGUIUtility.labelWidth = 30f;
            EditorGUI.PropertyField(minValueRect, property.FindPropertyRelative("Min"));
            EditorGUI.PropertyField(maxValueRect, property.FindPropertyRelative("Max"));
            EditorGUIUtility.labelWidth = 0f;
            
            EditorGUI.EndProperty();
        }
    }
}