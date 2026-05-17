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
		protected Slider Slider;

		public event Action<float> ValueChanged;

		public float Value
		{
			get => Slider.value;
			set => Slider.value = value;
		}

		public void SetBoundaries(float minValue, float maxValue)
		{
			Slider.minValue = minValue;
			Slider.maxValue = maxValue;
		}

		public void SetValueWithoutNotify(float value)
		{
			Slider.SetValueWithoutNotify(value);
		}

		public void SetInteractable(bool isInteractable)
		{
			Slider.interactable = isInteractable;
		}

		private void OnEnable()
		{
			Slider.onValueChanged.AddListener(HandleValueChanged);
		}

		private void OnDisable()
		{
			Slider.onValueChanged.RemoveListener(HandleValueChanged);
		}

		private void HandleValueChanged(float value)
		{
			ValueChanged?.Invoke(value);
		}
	}
}