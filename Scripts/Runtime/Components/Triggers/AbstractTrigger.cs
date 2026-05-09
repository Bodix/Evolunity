// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections;
using Evolutex.Evolunity.Utilities;
using NaughtyAttributes;
using UnityEngine;

namespace Evolutex.Evolunity.Components.Triggers
{
	[RequireComponent(typeof(Collider))]
	public abstract class AbstractTrigger : MonoBehaviour
	{
		public bool DisableAfterEnter = false;
		[ShowIf(nameof(DisableAfterEnter))]
		public bool ReenableOnDistance = false;
		[ShowIf(nameof(ReenableOnDistance))]
		public float ReenableThresholdDistance = 10f;
		[ShowIf(nameof(ReenableOnDistance))]
		[Tooltip("Times per second")]
		public float DistanceCheckRate = 10;
		[Tag, ValidateInput(nameof(ValidateTag),
			 "If \"(None)\" is selected, then collider with any tag will activate this trigger")]
		public string AllowedTag = "Player";

		private Collider _collider;
#if UNITY_2023_2_OR_NEWER
        // https://docs.unity3d.com/2023.2/Documentation/ScriptReference/TagHandle.html
        private TagHandle _tagHandle;
#endif

		public Collider Collider => _collider;

		public event Action Triggered;

		protected virtual void OnValidate()
		{
			_collider = GetComponent<Collider>();
			_collider.isTrigger = true;
		}

		protected virtual void Awake()
		{
			_collider = GetComponent<Collider>();
#if UNITY_2023_2_OR_NEWER
            _tagHandle = TagHandle.GetExistingTag(AllowedTag);
#endif
		}

		private void OnTriggerEnter(Collider other)
		{
#if UNITY_2023_2_OR_NEWER
            if (string.IsNullOrEmpty(AllowedTag) || other.CompareTag(_tagHandle))
#else
			if (string.IsNullOrEmpty(AllowedTag) || other.CompareTag(AllowedTag))
#endif
			{
				EnterTrigger(other);

				if (DisableAfterEnter)
				{
					gameObject.SetActive(false);

					if (ReenableOnDistance)
						StaticCoroutine.Start(DistanceCheckForReenable(other));
				}
			}
		}

		private IEnumerator DistanceCheckForReenable(Collider collider)
		{
			while (collider)
			{
				if (Vector3.Distance(collider.transform.position, transform.position) > ReenableThresholdDistance)
				{
					gameObject.SetActive(true);

					yield break;
				}

				yield return new WaitForSeconds(1f / DistanceCheckRate);
			}
		}

		private void OnTriggerStay(Collider other)
		{
#if UNITY_2023_2_OR_NEWER
            if (string.IsNullOrEmpty(AllowedTag) || other.CompareTag(_tagHandle))
#else
			if (string.IsNullOrEmpty(AllowedTag) || other.CompareTag(AllowedTag))
#endif
				StayInTrigger(other);
		}

		private void OnTriggerExit(Collider other)
		{
#if UNITY_2023_2_OR_NEWER
            if (string.IsNullOrEmpty(AllowedTag) || other.CompareTag(_tagHandle))
#else
			if (string.IsNullOrEmpty(AllowedTag) || other.CompareTag(AllowedTag))
#endif
				ExitTrigger(other);
		}

		protected abstract void EnterTrigger(Collider other);

		protected virtual void StayInTrigger(Collider other) { }

		protected virtual void ExitTrigger(Collider other) { }

		protected void InvokeTriggeredEvent()
		{
			Triggered?.Invoke();
		}

		// Should be protected to be visible by in derived class by NaughtyAttributes.
		protected bool ValidateTag(string value)
		{
			return !string.IsNullOrEmpty(value);
		}
	}
}