// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using TMPro;
using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Text")]
	public class UiText : UiElement
	{
		[SerializeField]
		protected TMP_Text text;

		public TMP_Text Text => text;
	}
}