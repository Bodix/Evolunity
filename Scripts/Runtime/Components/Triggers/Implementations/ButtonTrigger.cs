// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components
{
	[AddComponentMenu("Evolunity/Triggers/Button Trigger")]
	public class ButtonTrigger : Trigger
	{
		[SerializeField, HideIf(nameof(HideButtonInInspector))]
		protected Button _uiButton;

		protected virtual bool HideButtonInInspector => false;

		protected virtual void OnDisable()
		{
			HideButton();
		}

		protected override void EnterTrigger(Collider other)
		{
			ShowButton();
		}

		protected override void ExitTrigger(Collider other)
		{
			HideButton();
		}

		protected virtual void ShowButton()
		{
			if (!_uiButton)
				return;

			_uiButton.onClick.AddListener(InvokeTrigger);
			_uiButton.gameObject.SetActive(true);
		}

		protected virtual void HideButton()
		{
			if (!_uiButton)
				return;

			_uiButton.onClick.RemoveListener(InvokeTrigger);
			_uiButton.gameObject.SetActive(false);
		}
	}
}