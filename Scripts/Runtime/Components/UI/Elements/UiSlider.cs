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
		protected Slider _slider;

		public event Action<float> ValueChanged;

		public float Value
		{
			get => _slider.value;
			set => _slider.value = value;
		}

		public void SetBoundaries(float minValue, float maxValue)
		{
			_slider.minValue = minValue;
			_slider.maxValue = maxValue;
		}

		public void SetValueWithoutNotify(float value)
		{
			_slider.SetValueWithoutNotify(value);
		}

		public void SetInteractable(bool isInteractable)
		{
			_slider.interactable = isInteractable;
		}

		private void OnEnable()
		{
			_slider.onValueChanged.AddListener(HandleValueChanged);
		}

		private void OnDisable()
		{
			_slider.onValueChanged.RemoveListener(HandleValueChanged);
		}

		private void HandleValueChanged(float value)
		{
			ValueChanged?.Invoke(value);
		}
	}
}