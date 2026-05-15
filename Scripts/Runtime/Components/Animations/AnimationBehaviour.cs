using System;
using System.Collections;
using UnityEngine;

namespace Evolutex.Evolunity.Components.Animations
{
	public abstract class AnimationBehaviour : MonoBehaviour, IAnimation
	{
		public virtual void Play(Action onStart = null, Action onComplete = null)
		{
			StartCoroutine(AnimationCoroutineWithCallbacks(onStart, onComplete));
		}

		protected virtual IEnumerator AnimationCoroutineWithCallbacks(Action onStart, Action onComplete)
		{
			onStart?.Invoke();
			yield return AnimationCoroutine();
			onComplete?.Invoke();
		}

		protected abstract IEnumerator AnimationCoroutine();
	}
}