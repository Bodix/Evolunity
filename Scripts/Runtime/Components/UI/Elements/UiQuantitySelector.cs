// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Quantity Selector")]
	public class UiQuantitySelector : UiElement
	{
		[SerializeField]
		protected UiQuantitySlider quantitySlider;
		[SerializeField]
		protected UiTextButton confirmButton;

		public event Action<int> Confirmed;

		public UiQuantitySlider QuantitySlider => quantitySlider;
		public UiTextButton ConfirmButton => confirmButton;

		private Func<int, string> _textFormatter;

		private void OnEnable()
		{
			quantitySlider.ValueChanged += UpdateConfirmButton;
			confirmButton.Button.onClick.AddListener(Confirm);
		}

		private void OnDisable()
		{
			quantitySlider.ValueChanged -= UpdateConfirmButton;
			confirmButton.Button.onClick.RemoveListener(Confirm);
		}

		public void Setup(int minAmount, int maxAmount, string actionName)
		{
			Setup(minAmount, maxAmount, amount => $"{actionName} ({amount})");
		}

		public void Setup(int minAmount, int maxAmount, Func<int, string> customFormatter)
		{
			_textFormatter = customFormatter;
			quantitySlider.Setup(minAmount, maxAmount, 1);

			confirmButton.Button.interactable = maxAmount > 0;
			UpdateConfirmButton(quantitySlider.Value);
		}

		public void Clear(string confirmButtonText = "")
		{
			_textFormatter = null;
			quantitySlider.Clear();
			confirmButton.Text.text = confirmButtonText;
		}

		private void UpdateConfirmButton(int amount)
		{
			if (_textFormatter != null)
				confirmButton.Text.text = _textFormatter.Invoke(amount);
		}

		private void Confirm()
		{
			Confirmed?.Invoke(quantitySlider.Value);
		}
	}
}