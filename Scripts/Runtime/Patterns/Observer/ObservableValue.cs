// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Bodix.Evolunity.Patterns
{
	/// <summary>
	/// A zero-allocation reactive value container implemented as a mutable struct.
	/// Suitable for use only as fields or local variables. Not suitable for use as a property, as it will be copied
	/// as a value type when accessed. If you need to use it as a property, use <see cref="ObservableProperty{T}"/> instead.
	/// WARNING: Passing this struct by value will create an independent copy of both its data and its event subscribers.
	/// It MUST be manipulated via 'ref' modifiers or stored directly in contiguous memory layouts (like arrays) to prevent silent desynchronization bugs.
	/// </summary>
	/// <remarks>
	/// <b>DO NOT USE:</b>
	/// - In Dependency Injection containers (causes boxing or copying).
	/// - As return types for standard properties (returns a copy).
	/// - As standard method arguments (passed by value).
	/// 
	/// <b>USE ONLY:</b>
	/// - In performance-critical tight loops or large arrays to ensure CPU cache locality.
	/// - When you can strictly guarantee access via 'ref' locals or 'ref' returns.
	/// </remarks>
	[Serializable]
	public struct ObservableValue<T>
	{
		[SerializeField]
		private T _value;

		public ObservableValue(T value)
		{
			_value = value;

			Updated = null;
		}

		public event Action<T> Updated;

		public T Value
		{
			get => _value;
			set
			{
				_value = value;

				Updated?.Invoke(_value);
			}
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public static implicit operator T(ObservableValue<T> observable)
		{
			return observable.Value;
		}
	}
}