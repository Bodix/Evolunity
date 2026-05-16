// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components
{
	[AddComponentMenu("Evolunity/Look At Camera")]
	public class LookAtCamera : AbstractLookAt
	{
		private Transform _cameraTransform;

		public override Transform Target => _cameraTransform;

		private void Awake()
		{
			_cameraTransform = Camera.main.transform;
		}
	}
}