// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Quantity Text")]
	public class UiQuantityText : UiText
	{
		public string Prefix = "";
		public string Suffix = "";
		public bool AbbreviateLargeNumbers;

		private int _currentValue;

		public int Value => _currentValue;

		public void SetAmount(int amount)
		{
			_currentValue = amount;

			UpdateVisuals();
		}

		private void UpdateVisuals()
		{
			Text.text = $"{Prefix}{FormatNumber(_currentValue)}{Suffix}";
		}

		private string FormatNumber(int number)
		{
			if (!AbbreviateLargeNumbers)
				return number.ToString();

			if (number >= 1000000)
				return (number / 1000000f).ToString("0.#") + "M";

			if (number >= 1000)
				return (number / 1000f).ToString("0.#") + "k";

			return number.ToString();
		}
	}
}