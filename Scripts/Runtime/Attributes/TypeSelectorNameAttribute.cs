// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Bodix.Evolunity.Attributes
{
	/// <summary>
	/// Custom display name for this type in a <see cref="TypeSelectorAttribute"/> dropdown.
	/// If omitted, the nicified class name is used.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public class TypeSelectorNameAttribute : Attribute
	{
		public TypeSelectorNameAttribute(string name)
		{
			Name = name;
		}

		public string Name { get; }
	}
}
