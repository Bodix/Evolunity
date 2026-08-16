// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using Bodix.Evolunity.Utilities;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	[AddComponentMenu("Evolunity/Lifetime")]
	public class Lifetime : MonoBehaviour
	{
		[Tooltip("Time in seconds before the object is turned off.")]
		public float Duration = 5f;

		private void Start()
		{
			BeginLifetime();
		}

		public void BeginLifetime()
		{
			Delay.ForSeconds(Duration, Die, this);
		}

		/// <summary>
		/// Handles the end of the object's lifetime.
		/// Override this method in a derived class to implement object pooling.
		/// </summary>
		protected virtual void Die()
		{
			Destroy(gameObject);
		}
	}
}