// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Linq;
using Bodix.Evolunity.Attributes;
using Bodix.Evolunity.Editor.Extensions;
using UnityEditor;
using UnityEngine;

namespace Bodix.Evolunity.Editor.Drawers
{
	// TODO: Make safe types renaming:
	// https://docs.unity3d.com/ScriptReference/SerializationUtility.HasManagedReferencesWithMissingTypes.html
	// https://docs.unity3d.com/ScriptReference/SerializationUtility.GetManagedReferencesWithMissingTypes.html

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
			EditorGUI.PropertyField(position, property, label, true);

			GUIContent buttonContent = new GUIContent(GetManagedReferenceValueTypename(property));
			GUIStyle buttonStyle = EditorStyles.popup;
			float buttonWidth = buttonStyle.CalcSize(buttonContent).x + 5f;
			float maxAvailableWidth = position.width - EditorGUIUtility.labelWidth;
			buttonWidth = Mathf.Min(buttonWidth, Mathf.Max(maxAvailableWidth, 50f));
			Rect dropdownButtonRect = new Rect(
				position.xMax - buttonWidth,
				position.y,
				buttonWidth,
				EditorGUIUtility.singleLineHeight
			);

			if (EditorGUI.DropdownButton(dropdownButtonRect, buttonContent, FocusType.Keyboard, buttonStyle))
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
					property.serializedObject.Update();

					object obj = property.CreateManagedReferenceValue(type);
					property.isExpanded = obj != null;

					property.serializedObject.ApplyModifiedProperties();
				};

				dropdown.Show(dropdownButtonRect);
			}
		}

		private string GetManagedReferenceValueTypename(SerializedProperty property)
		{
			Type type = property.GetManagedReferenceValueType();

			return type == null ? "<NULL>" : ObjectNames.NicifyVariableName(type.Name);
		}
	}
}