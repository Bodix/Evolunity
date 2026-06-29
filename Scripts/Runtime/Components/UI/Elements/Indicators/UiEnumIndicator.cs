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
		private List<IndicatorState<T>> _states;

		public void SetState(T state)
		{
			if (_image == null)
			{
				Debug.LogWarning("Indicator Image is not assigned.");

				return;
			}

			foreach (IndicatorState<T> mapping in _states)
			{
				if (EqualityComparer<T>.Default.Equals(mapping.State, state))
				{
					_image.sprite = mapping.Sprite;
					_image.color = mapping.Color;

					return;
				}
			}

			Debug.LogWarning($"Sprite for state {state} is not found.");
		}
	}

	[Serializable]
	public struct IndicatorState<T> where T : Enum
	{
		public T State;
		public Sprite Sprite;
		public Color Color;
	}
}