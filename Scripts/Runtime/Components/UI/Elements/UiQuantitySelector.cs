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

		private Func<int, (string Text, bool IsInteractable)> _confirmButtonStateFormatter;

		public event Action<int> Confirmed;

		public UiQuantitySlider QuantitySlider => quantitySlider;
		public UiTextButton ConfirmButton => confirmButton;

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
			Setup(minAmount, maxAmount, amount => ($"{actionName} ({amount})", maxAmount > 0));
		}

		public void Setup(int minAmount, int maxAmount, Func<int, (string Text, bool IsInteractable)> customFormatter)
		{
			_confirmButtonStateFormatter = customFormatter;
			quantitySlider.Setup(minAmount, maxAmount, 1);
			UpdateConfirmButton(quantitySlider.Value);
		}

		public void Clear(string confirmButtonText = "")
		{
			_confirmButtonStateFormatter = null;
			quantitySlider.Clear();
			confirmButton.Text.text = confirmButtonText;
			confirmButton.Button.interactable = false;
		}

		private void UpdateConfirmButton(int amount)
		{
			if (_confirmButtonStateFormatter == null)
				return;

			var state = _confirmButtonStateFormatter(amount);
			confirmButton.Text.text = state.Text;
			confirmButton.Button.interactable = state.IsInteractable;
		}

		private void Confirm()
		{
			Confirmed?.Invoke(quantitySlider.Value);
		}
	}
}