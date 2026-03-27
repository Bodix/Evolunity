// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Components.DebugSelector
{
	[Serializable]
	public struct GameObjectSelectable : ISelectable
	{
		public GameObject GameObject;

		public void Select()
		{
			GameObject.SetActive(true);
		}

		public void Unselect()
		{
			GameObject.SetActive(false);
		}
	}
}