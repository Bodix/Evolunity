using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	[RequireComponent(typeof(IInteractable))]
	public abstract class UiInteractabilityHandler : MonoBehaviour
	{
		private IInteractable _interactable;

		protected virtual void Awake()
		{
			_interactable = GetComponent<IInteractable>();

			_interactable.InteractabilityChanged += HandleInteractabilityChange;

			HandleInteractabilityChange(_interactable.IsInteractable);
		}

		protected virtual void OnDestroy()
		{
			_interactable.InteractabilityChanged -= HandleInteractabilityChange;
		}

		protected abstract void HandleInteractabilityChange(bool interactable);
	}
}