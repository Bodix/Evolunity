// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Quantity Selector")]
	public class UiQuantitySelector : UiElement
	{
		[Header("Interactive Elements")]
		[SerializeField] private UiSlider _slider;
		[SerializeField] private UiButton _decreaseButton;
		[SerializeField] private UiButton _increaseButton;

		[Header("Visual Elements")]
		[SerializeField] private TMP_Text _amountText;

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
			if (_slider != null)
				_slider.ValueChanged += OnSliderValueChanged;

			if (_decreaseButton != null && _decreaseButton.Button != null)
				_decreaseButton.Button.onClick.AddListener(OnDecreaseClicked);

			if (_increaseButton != null && _increaseButton.Button != null)
				_increaseButton.Button.onClick.AddListener(OnIncreaseClicked);
		}

		protected virtual void OnDisable()
		{
			if (_slider != null)
				_slider.ValueChanged -= OnSliderValueChanged;

			if (_decreaseButton != null && _decreaseButton.Button != null)
				_decreaseButton.Button.onClick.RemoveListener(OnDecreaseClicked);

			if (_increaseButton != null && _increaseButton.Button != null)
				_increaseButton.Button.onClick.RemoveListener(OnIncreaseClicked);
		}

		public void Setup(int minValue, int maxValue, int startingValue)
		{
			_minValue = minValue;
			_maxValue = maxValue;

			if (_slider != null)
				_slider.SetBoundaries(minValue, maxValue);

			Value = startingValue;
		}

		public void Clear()
		{
			_minValue = 0;
			_maxValue = 0;

			if (_slider != null)
				_slider.SetBoundaries(0, 0);

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
				_amountText.text = _currentValue.ToString();

			if (_slider != null && !Mathf.Approximately(_slider.Value, _currentValue))
				_slider.SetValueWithoutNotify(_currentValue);

			if (_decreaseButton != null && _decreaseButton.Button != null)
				_decreaseButton.Button.interactable = _currentValue > _minValue;

			if (_increaseButton != null && _increaseButton.Button != null)
				_increaseButton.Button.interactable = _currentValue < _maxValue;
		}
	}
}