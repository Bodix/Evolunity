// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Progress Bar")]
	public class UiProgressBar : UiElement
	{
		[SerializeField]
		protected Image fillImage;

		[Range(0, 1), OnValueChanged(nameof(UpdateFillImage))]
		[SerializeField]
		private float _normalizedValue;

		public Image FillImage => fillImage;
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
			fillImage.rectTransform.anchorMax = new Vector2(_normalizedValue, fillImage.rectTransform.anchorMax.y);
		}
	}
}