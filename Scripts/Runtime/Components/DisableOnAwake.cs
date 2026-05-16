// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components
{
	[AddComponentMenu("Evolunity/Disable On Awake")]
	public class DisableOnAwake : MonoBehaviour
	{
		private void Awake()
		{
			gameObject.SetActive(false);
		}
	}
}