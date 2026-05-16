// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Progress Bar")]
	public class UiProgressBar : UiElement
	{
		public Image FillImage;

		[Range(0, 1), OnValueChanged(nameof(UpdateFillImage))]
		[SerializeField]
		private float _normalizedValue;

		public float NormalizedValue
		{
			get => _normalizedValue;
			set
			{
				_normalizedValue = Mathf.Clamp01(value);

				UpdateFillImage();
			}
		}

		public void UpdateFillImage()
		{
			// IMPORTANT: Using anchors instead of "fillAmount" to add possibility to work with Sliced image type.
			FillImage.rectTransform.anchorMax = new Vector2(_normalizedValue, FillImage.rectTransform.anchorMax.y);
		}
	}
}