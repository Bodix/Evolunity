// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

#if UNITY_2019_1_OR_NEWER

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Evolutex.Evolunity.Attributes;

namespace Evolutex.Evolunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(TypeSelectorAttribute))]
    public class TypeSelectorDrawer : AttributePropertyDrawer<TypeSelectorAttribute>
    {
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

            // Cache this string. This property internally call Assembly.GetName, which result in a large allocation.
            string managedReferenceFieldTypename = property.managedReferenceFieldTypename;
            Debug.Log(managedReferenceFieldTypename);

            using (new EditorGUI.PropertyScope(position, label, property))
            {
                // if (EditorGUI.DropdownButton(popupPosition, GetTypeName(property), FocusType.Keyboard))
                // {
                //     TypePopupCache popup = GetTypePopup(property);
                //     m_TargetProperty = property;
                //     popup.TypePopup.Show(popupPosition);
                // }
                Type baseType = GetType(managedReferenceFieldTypename);
                if (EditorGUI.DropdownButton(dropdownButtonRect, new GUIContent("Test"), FocusType.Keyboard))
                    new TypeSelectorDropdown(TypeCache.GetTypesDerivedFrom(baseType).Append(baseType).Where(p =>
                        (p.IsPublic || p.IsNestedPublic) &&
                        !p.IsAbstract &&
                        !p.IsGenericType &&
                        !typeof(UnityEngine.Object).IsAssignableFrom(p) &&
                        System.Attribute.IsDefined(p, typeof(SerializableAttribute))
                    )).Show(dropdownButtonRect);

                EditorGUI.PropertyField(position, property, label, true);
            }
        }
        
        public static Type GetType(string typeName)
        {
            int splitIndex = typeName.IndexOf(' ');
            Assembly assembly = Assembly.Load(typeName.Substring(0, splitIndex));
            return assembly.GetType(typeName.Substring(splitIndex + 1));
        }
        
        public static object SetManagedReference (SerializedProperty property,Type type) {
            object obj = (type != null) ? Activator.CreateInstance(type) : null;
            property.managedReferenceValue = obj;
            return obj;
        }
    }
}

#endif