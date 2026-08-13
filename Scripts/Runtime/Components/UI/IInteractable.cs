using System;

namespace Bodix.Evolunity.Components.UI
{
	public interface IInteractable
	{
		event Action<bool> InteractabilityChanged;

		bool IsInteractable { get; set; }
	}
}