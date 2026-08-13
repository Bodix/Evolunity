using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	/// <summary>
	/// It simply adds an event when the interactivity state (<see cref="Button.interactable"/>) changes.
	/// </summary>
	[AddComponentMenu("")]
	public class ObservableButton : Button, IInteractable
	{
		private bool _wasInteractable = true;

		public event Action<bool> InteractabilityChanged;

		public new bool IsInteractable { get => interactable; set => interactable = value; }

		protected override void Awake()
		{
			base.Awake();

			_wasInteractable = interactable;
		}

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			base.DoStateTransition(state, instant);

			if (interactable != _wasInteractable)
			{
				_wasInteractable = interactable;

				InteractabilityChanged?.Invoke(_wasInteractable);
			}
		}
	}
}