using System;
using UnityEngine;

namespace Bodix.Evolunity.Patterns
{
	/// <summary>
	/// A strict, non-nullable reference container for Dependency Injection.
	/// Wraps an observable property to provide reactivity while ensuring the dependency is never null.
	/// </summary>
	[Serializable]
	public class ObservableReference<T> where T : class
	{
		[SerializeField]
		private ObservableProperty<T> _property;

		public ObservableReference(T initialValue)
		{
			if (initialValue == null)
				Debug.LogError($"[{nameof(ObservableReference<T>)}] Initialization failed: provided initial {typeof(T).Name} is null.");

			_property = new ObservableProperty<T>(initialValue);
		}

		public event Action<T> Changed
		{
			add => _property.Updated += value;
			remove => _property.Updated -= value;
		}

		public T Value
		{
			get => _property.Value;
			set
			{
				if (value == null)
				{
					Debug.LogError($"[{nameof(ObservableReference<T>)}] Failed to set value: provided {typeof(T).Name} is null.");

					return;
				}

				_property.Value = value;
			}
		}
	}
}