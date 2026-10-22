// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
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
		private float _endTime;

		/// <summary>
		/// Called when the time runs out, right before the object dies.
		/// </summary>
		public event Action Expired;

		/// <summary>
		/// Seconds before the object dies. While the timer is not running, it is the whole duration.
		/// </summary>
		public float TimeLeft => _timerCoroutine != null ? Mathf.Max(0f, _endTime - Time.time) : Duration;

		private void Start()
		{
			BeginLifetime();
		}

		/// <summary>
		/// Starts the timer for the whole duration. A running timer starts over.
		/// </summary>
		public void BeginLifetime()
		{
			StopLifetime();

			_endTime = Time.time + Duration;
			_timerCoroutine = Delay.ForSeconds(Duration, Expire, this);
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

		private void Expire()
		{
			Expired?.Invoke();
			Die();
		}
	}
}
