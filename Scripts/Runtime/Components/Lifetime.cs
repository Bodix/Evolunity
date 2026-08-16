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

		private Coroutine _timerCoroutine;

		private void Start()
		{
			BeginLifetime();
		}

		public void BeginLifetime()
		{
			StopLifetime();

			_timerCoroutine = Delay.ForSeconds(Duration, Die, this);
		}

		/// <summary>
		/// Call this if the object is destroyed or pooled before the time runs out.
		/// </summary>
		public void StopLifetime()
		{
			if (_timerCoroutine != null)
			{
				StopCoroutine(_timerCoroutine);

				_timerCoroutine = null;
			}
		}

		/// <summary>
		/// Override this method in a derived class to implement object pooling.
		/// </summary>
		protected virtual void Die()
		{
			_timerCoroutine = null;

			Destroy(gameObject);
		}
	}
}