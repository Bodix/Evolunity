// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using Evolutex.Evolunity.Attributes;
using UnityEditor;
using UnityEngine;

namespace Evolutex.Evolunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerDrawer : AttributePropertyDrawer<LayerAttribute>
    {
        protected override SerializedPropertyType[] SupportedTypes => new[]
        {
            SerializedPropertyType.Integer
        };

        protected override void OnValidatedGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int index = property.intValue;
            if (index < 0)
            {
                Debug.LogError("Layer index is too low '" + index + "', was set to '0'");

                index = 0;
            }
            else if (index > 31)
            {
                Debug.LogError("Layer index is too high '" + index + "', was set to '31'");

                index = 31;
            }

            property.intValue = EditorGUI.LayerField(position, label, index);
        }
    }
}