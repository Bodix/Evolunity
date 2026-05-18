// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Action Button")]
	public class UiActionButton : UiTextButton
	{
		[Tooltip("{0} represents the action name, {1} represents the amount.")]
		public string Format = "{0} ({1})";

		private string _currentAction = "Confirm";
		private int _currentAmount;

		public void Setup(string action, int amount)
		{
			_currentAction = action;
			_currentAmount = amount;

			UpdateVisuals();
		}

		public void SetAmount(int amount)
		{
			_currentAmount = amount;

			UpdateVisuals();
		}

		public void SetAction(string action)
		{
			_currentAction = action;

			UpdateVisuals();
		}

		private void UpdateVisuals()
		{
			Text.text = string.Format(Format, _currentAction, _currentAmount);
		}
	}
}