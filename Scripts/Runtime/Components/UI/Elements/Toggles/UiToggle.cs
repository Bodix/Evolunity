// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	// TODO: Implement IInteractable interface like in UiButton. [#design]
	
	[AddComponentMenu("Evolunity/UI/Toggle")]
	public class UiToggle : UiElement
	{
		[SerializeField]
		protected Toggle toggle;
		[SerializeField]
		protected Image background;

		public new Toggle Toggle => toggle;
		public Image Background => background;
	}
}