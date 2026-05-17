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
		protected UiQuantitySlider _quantitySlider;
		[SerializeField]
		protected UiTextButton _confirmButton;

		public event Action<int> Confirmed;

		private string _actionPrefix = "Confirm";

		private void OnEnable()
		{
			_quantitySlider.ValueChanged += UpdateConfirmText;
			_confirmButton.Button.onClick.AddListener(Confirm);
		}

		private void OnDisable()
		{
			_quantitySlider.ValueChanged -= UpdateConfirmText;
			_confirmButton.Button.onClick.RemoveListener(Confirm);
		}

		public void Setup(int minAmount, int maxAmount, string actionPrefix)
		{
			_actionPrefix = actionPrefix;
			_quantitySlider.Setup(minAmount, maxAmount, 1);
			_confirmButton.Button.interactable = maxAmount > 0;
			UpdateConfirmText(_quantitySlider.Value);
		}

		public void Clear()
		{
			_quantitySlider.Clear();
			_confirmButton.Button.interactable = false;
			if (_confirmButton.Text != null)
				_confirmButton.Text.text = "...";
		}

		private void Confirm()
		{
			Confirmed?.Invoke(_quantitySlider.Value);
		}

		private void UpdateConfirmText(int amount)
		{
			if (_confirmButton.Text != null)
				_confirmButton.Text.text = $"{_actionPrefix} ({amount} pcs)";
		}
	}
}