// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	[Serializable]
	public struct GameObjectArraySelectable : ISelectable
	{
		public GameObject[] GameObjects;

		public void Select()
		{
			foreach (GameObject gameObject in GameObjects)
				gameObject.SetActive(true);
		}

		public void Unselect()
		{
			foreach (GameObject gameObject in GameObjects)
				gameObject.SetActive(false);
		}
	}
}