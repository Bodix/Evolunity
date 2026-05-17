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
			set => SetValueInternal(value, true);
		}

		protected virtual void OnEnable()
		{
			if (slider != null)
				slider.ValueChanged += OnSliderValueChanged;

			if (decreaseButton != null && decreaseButton.Button != null)
				decreaseButton.Button.onClick.AddListener(Decrease);

			if (increaseButton != null && increaseButton.Button != null)
				increaseButton.Button.onClick.AddListener(Increase);
		}

		protected virtual void OnDisable()
		{
			if (slider != null)
				slider.ValueChanged -= OnSliderValueChanged;

			if (decreaseButton != null && decreaseButton.Button != null)
				decreaseButton.Button.onClick.RemoveListener(Decrease);

			if (increaseButton != null && increaseButton.Button != null)
				increaseButton.Button.onClick.RemoveListener(Increase);
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
			// Value came from the slider itself, so we skip updating the slider's visual position.
			SetValueInternal(Mathf.RoundToInt(value), false);
		}

		private void Decrease()
		{
			Value--;
		}

		private void Increase()
		{
			Value++;
		}

		private void SetValueInternal(int newValue, bool updateSliderVisuals)
		{
			int clampedValue = Mathf.Clamp(newValue, _minValue, _maxValue);
			if (_currentValue == clampedValue)
				return;

			_currentValue = clampedValue;

			if (amountText != null)
				amountText.Text.text = _currentValue.ToString();

			if (updateSliderVisuals && slider != null)
				slider.Slider.SetValueWithoutNotify(_currentValue);

			if (decreaseButton != null && decreaseButton.Button != null)
				decreaseButton.Button.interactable = _currentValue > _minValue;

			if (increaseButton != null && increaseButton.Button != null)
				increaseButton.Button.interactable = _currentValue < _maxValue;

			ValueChanged?.Invoke(_currentValue);
		}
	}
}