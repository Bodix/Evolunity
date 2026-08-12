// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Button")]
	public class UiButton : UiElement, IInteractable
	{
		[SerializeField]
		protected Button button;
		[SerializeField]
		protected Image background;

		public event Action<bool> InteractabilityChanged;

		public Button Button => button;
		public Image Background => background;
		public bool IsInteractable
		{
			get => button.interactable;
			set
			{
				if (button.interactable != value)
				{
					button.interactable = value;

					// If it's an ObservableButton, the event will fire automatically.
					// If not, we fire it manually here to ensure backward compatibility.
					if (!(button is ObservableButton))
						InvokeInteractabilityChangedEvent(value);
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();

			if (button is ObservableButton observableButton)
				observableButton.InteractabilityChanged += InvokeInteractabilityChangedEvent;
		}

		protected virtual void OnDestroy()
		{
			if (button is ObservableButton observableButton)
				observableButton.InteractabilityChanged -= InvokeInteractabilityChangedEvent;
		}

		private void InvokeInteractabilityChangedEvent(bool isInteractable)
		{
			InteractabilityChanged?.Invoke(isInteractable);
		}
	}
}