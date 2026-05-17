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

		private string _actionPrefix = "Confirm";

		public event Action<int> Confirmed;

		public UiQuantitySlider QuantitySlider => quantitySlider;
		public UiTextButton ConfirmButton => confirmButton;

		private void OnEnable()
		{
			quantitySlider.ValueChanged += UpdateConfirmText;
			confirmButton.Button.onClick.AddListener(Confirm);
		}

		private void OnDisable()
		{
			quantitySlider.ValueChanged -= UpdateConfirmText;
			confirmButton.Button.onClick.RemoveListener(Confirm);
		}

		public void Setup(int minAmount, int maxAmount, string actionPrefix)
		{
			_actionPrefix = actionPrefix;
			quantitySlider.Setup(minAmount, maxAmount, 1);
			confirmButton.Button.interactable = maxAmount > 0;
			UpdateConfirmText(quantitySlider.Value);
		}

		public void Clear()
		{
			quantitySlider.Clear();
			confirmButton.Button.interactable = false;
			if (confirmButton.Text != null)
				confirmButton.Text.text = "...";
		}

		private void Confirm()
		{
			Confirmed?.Invoke(quantitySlider.Value);
		}

		private void UpdateConfirmText(int amount)
		{
			if (confirmButton.Text != null)
				confirmButton.Text.text = $"{_actionPrefix} ({amount} pcs)";
		}
	}
}