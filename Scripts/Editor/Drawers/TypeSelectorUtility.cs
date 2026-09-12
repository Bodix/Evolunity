// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using Bodix.Evolunity.Attributes;
using UnityEditor;

namespace Bodix.Evolunity.Editor.Drawers
{
	public static class TypeSelectorUtility
	{
		private static readonly Dictionary<Type, string> DisplayNames = new Dictionary<Type, string>();

		public static string GetDisplayName(Type type)
		{
			if (DisplayNames.TryGetValue(type, out string displayName))
				return displayName;

			TypeSelectorNameAttribute attribute = (TypeSelectorNameAttribute)Attribute.GetCustomAttribute(
				type, typeof(TypeSelectorNameAttribute), false);
			displayName = attribute != null && !string.IsNullOrEmpty(attribute.Name)
				? attribute.Name
				: ObjectNames.NicifyVariableName(type.Name);

			DisplayNames.Add(type, displayName);

			return displayName;
		}
	}
}
