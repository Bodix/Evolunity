using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	public class UiShadowHandler : UiInteractabilityHandler
	{
		[SerializeField]
		private Shadow _shadow;

		protected override void Awake()
		{
			base.Awake();

			if (!_shadow)
				enabled = false;
		}

		protected override void HandleInteractabilityChange(bool interactable)
		{
			_shadow.enabled = interactable;
		}
	}
}