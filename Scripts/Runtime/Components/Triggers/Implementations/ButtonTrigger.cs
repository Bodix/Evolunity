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
		private MultiSourceActivationTracker _tracker;

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

			if (!_tracker)
				if (!_uiButton.TryGetComponent(out _tracker))
					_tracker = _uiButton.gameObject.AddComponent<MultiSourceActivationTracker>();

			_tracker.AddRequest(this);
		}

		protected virtual void HideButton()
		{
			if (!_uiButton)
				return;

			_uiButton.onClick.RemoveListener(InvokeTrigger);

			if (_tracker)
				_tracker.RemoveRequest(this);
		}
	}
}