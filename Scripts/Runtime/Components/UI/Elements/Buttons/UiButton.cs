// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Button")]
	public class UiButton : UiElement
	{
		[SerializeField]
		protected Button button;
		[SerializeField]
		protected Image background;

		public Button Button => button;
		public Image Background => background;
	}
}