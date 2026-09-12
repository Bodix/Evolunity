// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Bodix.Evolunity.Attributes
{
	/// <summary>
	/// Target type requirements:
	/// <br/> - Public
	/// <br/> - Has [Serializable] attribute
	/// <br/> - Not abstract
	/// <br/> - Not generic
	/// <br/> - Not derived from UnityEngine.Object
	/// </summary>
	// TODO:
	// - Nested dropdown paths via '/' in TypeSelectorName (e.g. "Combat/Kill Enemies"); button shows the last segment.
	// - Localization of type display names.
	// - Custom order of types in the dropdown.
	// - Hiding types from the dropdown.
	// [#design]
	[AttributeUsage(AttributeTargets.Field)]
	public class TypeSelectorAttribute : PropertyAttribute
	{
	}
}