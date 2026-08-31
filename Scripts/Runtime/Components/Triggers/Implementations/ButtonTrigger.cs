// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
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

		/// <summary>
		/// Tracks all objects that are currently within this specific trigger.
		/// </summary>
		private readonly HashSet<Collider> _collidersInside = new HashSet<Collider>();
		/// <summary>
		/// Manages the shared activation state of the target UI button.
		/// It ensures the button remains active when multiple triggers overlap,
		/// preventing one trigger from hiding the button while another is still using it.
		/// </summary>
		private MultiSourceActivationTracker _buttonActivationTracker;

		protected virtual bool HideButtonInInspector => false;

		protected virtual void OnDisable()
		{
			if (_collidersInside.Count > 0)
			{
				_collidersInside.Clear();

				HideButton();
			}
		}

		protected override void EnterTrigger(Collider other)
		{
			if (_collidersInside.Count == 0)
				ShowButton();

			_collidersInside.Add(other);
		}

		protected override void ExitTrigger(Collider other)
		{
			if (_collidersInside.Remove(other))
			{
				if (_collidersInside.Count == 0)
					HideButton();
			}
		}

		protected virtual void ShowButton()
		{
			if (!_uiButton)
				return;

			_uiButton.onClick.AddListener(InvokeTrigger);

			if (!_buttonActivationTracker)
				if (!_uiButton.TryGetComponent(out _buttonActivationTracker))
					_buttonActivationTracker = _uiButton.gameObject.AddComponent<MultiSourceActivationTracker>();

			_buttonActivationTracker.AddRequest(this);
		}

		protected virtual void HideButton()
		{
			if (!_uiButton)
				return;

			_uiButton.onClick.RemoveListener(InvokeTrigger);

			if (_buttonActivationTracker)
				_buttonActivationTracker.RemoveRequest(this);
		}
	}
}