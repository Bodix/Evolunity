// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Quantity Slider")]
	public class UiQuantitySlider : UiElement
	{
		[Header("Interactive Elements")]
		[SerializeField]
		protected UiSlider Slider;
		[SerializeField]
		protected UiButton DecreaseButton;
		[SerializeField]
		protected UiButton IncreaseButton;

		[Header("Visual Elements")]
		[SerializeField]
		protected UiText _amountText;

		public event Action<int> ValueChanged;

		private int _minValue = 1;
		private int _maxValue = 1;
		private int _currentValue = 1;

		public int Value
		{
			get => _currentValue;
			set
			{
				int clampedValue = Mathf.Clamp(value, _minValue, _maxValue);
				if (_currentValue == clampedValue)
					return;

				_currentValue = clampedValue;
				UpdateVisuals();
				ValueChanged?.Invoke(_currentValue);
			}
		}

		protected virtual void OnEnable()
		{
			if (Slider != null)
				Slider.ValueChanged += OnSliderValueChanged;

			if (DecreaseButton != null && DecreaseButton.Button != null)
				DecreaseButton.Button.onClick.AddListener(OnDecreaseClicked);

			if (IncreaseButton != null && IncreaseButton.Button != null)
				IncreaseButton.Button.onClick.AddListener(OnIncreaseClicked);
		}

		protected virtual void OnDisable()
		{
			if (Slider != null)
				Slider.ValueChanged -= OnSliderValueChanged;

			if (DecreaseButton != null && DecreaseButton.Button != null)
				DecreaseButton.Button.onClick.RemoveListener(OnDecreaseClicked);

			if (IncreaseButton != null && IncreaseButton.Button != null)
				IncreaseButton.Button.onClick.RemoveListener(OnIncreaseClicked);
		}

		public void Setup(int minValue, int maxValue, int startingValue)
		{
			_minValue = minValue;
			_maxValue = maxValue;

			if (Slider != null)
				Slider.SetBoundaries(minValue, maxValue);

			Value = startingValue;
		}

		public void Clear()
		{
			_minValue = 0;
			_maxValue = 0;

			if (Slider != null)
				Slider.SetBoundaries(0, 0);

			Value = 0;
		}

		private void OnSliderValueChanged(float value)
		{
			Value = Mathf.RoundToInt(value);
		}

		private void OnDecreaseClicked()
		{
			Value--;
		}

		private void OnIncreaseClicked()
		{
			Value++;
		}

		private void UpdateVisuals()
		{
			if (_amountText != null)
				_amountText.Text.text = _currentValue.ToString();

			if (Slider != null && !Mathf.Approximately(Slider.Value, _currentValue))
				Slider.SetValueWithoutNotify(_currentValue);

			if (DecreaseButton != null && DecreaseButton.Button != null)
				DecreaseButton.Button.interactable = _currentValue > _minValue;

			if (IncreaseButton != null && IncreaseButton.Button != null)
				IncreaseButton.Button.interactable = _currentValue < _maxValue;
		}
	}
}