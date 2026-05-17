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
		protected UiActionButton confirmButton;

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
			quantitySlider.Setup(minAmount, maxAmount, 1);
			confirmButton.Setup(actionName, quantitySlider.Value);

			confirmButton.Button.interactable = maxAmount > 0;
		}

		public void Clear()
		{
			quantitySlider.Clear();
			confirmButton.Setup("...", 0);
			confirmButton.Button.interactable = false;
		}

		private void UpdateConfirmButton(int amount)
		{
			confirmButton.SetAmount(amount);
		}

		private void Confirm()
		{
			Confirmed?.Invoke(quantitySlider.Value);
		}
	}
}