// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

#if UNITY_2019_3_OR_NEWER

using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Evolutex.Evolunity.Attributes;
using Evolutex.Evolunity.Editor.Extensions;

namespace Evolutex.Evolunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(TypeSelectorAttribute))]
    public class TypeSelectorDrawer : AttributePropertyDrawer<TypeSelectorAttribute>
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }

        protected override SerializedPropertyType[] SupportedTypes => new[]
        {
            SerializedPropertyType.ManagedReference
        };

        protected override void OnValidatedGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect dropdownButtonRect = new Rect(position);
            dropdownButtonRect.width -= EditorGUIUtility.labelWidth;
            dropdownButtonRect.x += EditorGUIUtility.labelWidth;
            dropdownButtonRect.height = EditorGUIUtility.singleLineHeight;

            if (EditorGUI.DropdownButton(dropdownButtonRect,
                new GUIContent(GetManagedReferenceValueTypename(property)), FocusType.Keyboard))
            {
                Type baseType = property.GetManagedReferenceFieldType();
                TypeSelectorDropdown dropdown = new TypeSelectorDropdown(
                    TypeCache.GetTypesDerivedFrom(baseType)
                        .Append(baseType)
                        .Where(x =>
                            (x.IsPublic || x.IsNestedPublic) &&
                            !x.IsAbstract &&
                            !x.IsGenericType &&
                            !typeof(UnityEngine.Object).IsAssignableFrom(x) &&
                            System.Attribute.IsDefined(x, typeof(SerializableAttribute))
                        ));
                dropdown.TypeSelected += type =>
                {
                    object obj = property.CreateManagedReferenceValue(type);
                    property.isExpanded = obj != null;
                    property.serializedObject.ApplyModifiedProperties();
                    property.serializedObject.Update();
                };

                dropdown.Show(dropdownButtonRect);
            }

            EditorGUI.PropertyField(position, property, label, true);
        }

        private string GetManagedReferenceValueTypename(SerializedProperty property)
        {
            Type type = property.GetManagedReferenceValueType();

            return type == null ? "NULL" : ObjectNames.NicifyVariableName(type.Name);
        }
    }
}

#endif