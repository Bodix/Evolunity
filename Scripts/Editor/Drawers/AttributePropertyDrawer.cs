// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Linq;
using Evolutex.Evolunity.Extensions;
using UnityEditor;
using UnityEngine;

namespace Evolutex.Evolunity.Editor.Drawers
{
    // Don't forget for this attribute.
    // [CustomPropertyDrawer(typeof(TAttribute))]
    public abstract class AttributePropertyDrawer<TAttribute> : PropertyDrawer where TAttribute : PropertyAttribute
    {
        protected abstract SerializedPropertyType[] SupportedTypes { get; }
        
        // TODO: FIX THIS ISSUE. CHECK FOR LAYER ATTRIBUTE.
        // public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => -2f;
        
        protected TAttribute Attribute => (TAttribute) attribute;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsSupported(property))
            {
                OnValidatedGUI(position, property, label);
            }
            else
            {
                string typeErrorText = "Type " + property.propertyType
                    + " is not supported with this property attribute.\n\n"
                    + "Supported property types:\n"
                    + SupportedTypes.AsString(x => "- " + x, "\n");

                EditorGUI.HelpBox(GUILayoutUtility.GetRect(new GUIContent(typeErrorText), EditorStyles.helpBox),
                    typeErrorText, MessageType.Error);
            }
        }

        protected virtual bool IsSupported(SerializedProperty property)
        {
            return SupportedTypes.Any(x => x == property.propertyType);
        }

        protected virtual void OnValidatedGUI(Rect position, SerializedProperty property, GUIContent label) { }
    }
}