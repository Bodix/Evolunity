// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Slider")]
	public class UiSlider : UiElement
	{
		[SerializeField]
		protected Slider slider;

		public event Action<float> ValueChanged;

		public Slider Slider => slider;
		public float Value
		{
			get => slider.value;
			set => slider.value = value;
		}

		public void SetBoundaries(float minValue, float maxValue)
		{
			slider.minValue = minValue;
			slider.maxValue = maxValue;
		}

		private void OnEnable()
		{
			slider.onValueChanged.AddListener(HandleValueChanged);
		}

		private void OnDisable()
		{
			slider.onValueChanged.RemoveListener(HandleValueChanged);
		}

		private void HandleValueChanged(float value)
		{
			ValueChanged?.Invoke(value);
		}
	}
}