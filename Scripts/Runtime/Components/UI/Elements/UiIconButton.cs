// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Icon Button")]
	public class UiIconButton : UiButton
	{
		[SerializeField]
		protected Image icon;

		public Image Icon => icon;
	}
}