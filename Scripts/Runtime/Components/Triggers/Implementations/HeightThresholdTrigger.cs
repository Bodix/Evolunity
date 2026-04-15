// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.Events;

namespace Evolutex.Evolunity.Components.Triggers
{
	[AddComponentMenu("Evolunity/Triggers/Height Threshold Trigger")]
	public class HeightThresholdTrigger : MonoBehaviour
	{
		public float TargetHeightThreshold = 0f;
		public bool DisableAfterTrigger = false;
		public UnityEvent ThresholdCrossed;

		private bool _isInitialized;
		private bool _wasBelowThreshold;

		private void OnEnable()
		{
			_isInitialized = false;
		}

		private void Update()
		{
			bool isCurrentlyBelowThreshold = transform.position.y < TargetHeightThreshold;

			if (!_isInitialized)
			{
				_wasBelowThreshold = isCurrentlyBelowThreshold;
				_isInitialized = true;

				return;
			}

			if (isCurrentlyBelowThreshold != _wasBelowThreshold)
			{
				_wasBelowThreshold = isCurrentlyBelowThreshold;

				ThresholdCrossed?.Invoke();

				if (DisableAfterTrigger)
					enabled = false;
			}
		}
	}
}