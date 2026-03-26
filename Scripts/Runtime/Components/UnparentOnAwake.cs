// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Components
{
	[AddComponentMenu("Evolunity/Unparent On Awake")]
	public class UnparentOnAwake : MonoBehaviour
	{
		private void Awake()
		{
			transform.SetParent(null);
		}
	}
}