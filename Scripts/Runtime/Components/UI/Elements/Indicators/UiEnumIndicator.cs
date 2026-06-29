using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	/// <summary>
	/// Generic base class for a UI indicator that changes sprites based on an enum state.
	/// </summary>
	public abstract class UiEnumIndicator<T> : MonoBehaviour where T : Enum
	{
		[SerializeField]
		private Image _image;
		[SerializeField]
		private List<StateSpriteMapping<T>> _stateMappings;

		public void SetState(T state)
		{
			if (_image == null)
			{
				Debug.LogWarning("Indicator Image is not assigned.");

				return;
			}

			foreach (StateSpriteMapping<T> mapping in _stateMappings)
			{
				if (EqualityComparer<T>.Default.Equals(mapping.State, state))
				{
					_image.sprite = mapping.Sprite;

					return;
				}
			}

			Debug.LogWarning($"Sprite for state {state} is not found.");
		}
	}

	[Serializable]
	public struct StateSpriteMapping<T> where T : Enum
	{
		public T State;
		public Sprite Sprite;
	}
}