// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Icon Text Toggle")]
	public class UiIconTextToggle : UiToggle
	{
		[SerializeField]
		protected Image icon;
		[SerializeField]
		protected TMP_Text text;

		public Image Icon => icon;
		public TMP_Text Text => text;
	}
}