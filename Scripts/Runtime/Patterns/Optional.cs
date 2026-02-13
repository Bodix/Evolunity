#if CSHARP_8_OR_LATER
using System;
using UnityEngine;

namespace Evolutex.Evolunity.Patterns
{
	/// <summary>
	/// Can be used for optional dependencies in VContainer, because VContainer doesn't support it by default.
	/// </summary>
	[Serializable]
	public struct Optional<T>
	{
		[SerializeField]
		private T? _value;

		public T Value => !HasValue
			? throw new InvalidOperationException($"Optional<{typeof(T).Name}> has no value")
			: _value!;
		public T? ValueOrDefault => _value;
		public bool HasValue => _value != null;

		public Optional(T? value)
		{
			_value = value;
		}

		public static implicit operator Optional<T>(T value)
		{
			return new Optional<T>(value);
		}

		public static implicit operator bool(Optional<T> optional)
		{
			return optional.HasValue;
		}
	}
}
#endif