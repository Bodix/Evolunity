// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Components
{
	public abstract class AbstractLookAt : MonoBehaviour
	{
		public bool FlipX;
		public bool FlipY;
		public bool FlipZ;

		public abstract Transform Target { get; }

		private void Update()
		{
			LookAtTarget();
		}

		private void LookAtTarget()
		{
			transform.LookAt(Target);

			if (FlipX)
				transform.Rotate(180, 0, 0);
			if (FlipY)
				transform.Rotate(0, 180, 0);
			if (FlipZ)
				transform.Rotate(0, 0, 180);
		}
	}
}