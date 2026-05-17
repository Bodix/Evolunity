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
		protected UiSlider slider;
		[SerializeField]
		protected UiButton decreaseButton;
		[SerializeField]
		protected UiButton increaseButton;

		[Header("Visual Elements")]
		[SerializeField]
		protected UiText amountText;

		private int _minValue = 1;
		private int _maxValue = 1;
		private int _currentValue = 1;

		public event Action<int> ValueChanged;

		public UiSlider Slider => slider;
		public UiButton DecreaseButton => decreaseButton;
		public UiButton IncreaseButton => increaseButton;
		public UiText AmountText => amountText;
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
			if (slider != null)
				slider.ValueChanged += OnSliderValueChanged;

			if (decreaseButton != null && decreaseButton.Button != null)
				decreaseButton.Button.onClick.AddListener(OnDecreaseClicked);

			if (increaseButton != null && increaseButton.Button != null)
				increaseButton.Button.onClick.AddListener(OnIncreaseClicked);
		}

		protected virtual void OnDisable()
		{
			if (slider != null)
				slider.ValueChanged -= OnSliderValueChanged;

			if (decreaseButton != null && decreaseButton.Button != null)
				decreaseButton.Button.onClick.RemoveListener(OnDecreaseClicked);

			if (increaseButton != null && increaseButton.Button != null)
				increaseButton.Button.onClick.RemoveListener(OnIncreaseClicked);
		}

		public void Setup(int minValue, int maxValue, int startingValue)
		{
			_minValue = minValue;
			_maxValue = maxValue;

			if (slider != null)
				slider.SetBoundaries(minValue, maxValue);

			Value = startingValue;
		}

		public void Clear()
		{
			_minValue = 0;
			_maxValue = 0;

			if (slider != null)
				slider.SetBoundaries(0, 0);

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
			if (amountText != null)
				amountText.Text.text = _currentValue.ToString();

			if (slider != null && !Mathf.Approximately(slider.Value, _currentValue))
				slider.Slider.SetValueWithoutNotify(_currentValue);

			if (decreaseButton != null && decreaseButton.Button != null)
				decreaseButton.Button.interactable = _currentValue > _minValue;

			if (increaseButton != null && increaseButton.Button != null)
				increaseButton.Button.interactable = _currentValue < _maxValue;
		}
	}
}