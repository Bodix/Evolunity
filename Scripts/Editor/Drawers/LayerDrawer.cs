// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using Evolunity.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Evolutex.Evolunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerDrawer : PropertyDrawer
    {
        private const string TypeErrorMessage = "Use " + nameof(LayerAttribute) + " with int";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Assert.IsTrue(property.propertyType == SerializedPropertyType.Integer, TypeErrorMessage);

            if (property.propertyType == SerializedPropertyType.Integer)
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
            else
            {
                EditorGUI.LabelField(position, TypeErrorMessage);
            }
        }
    }
}