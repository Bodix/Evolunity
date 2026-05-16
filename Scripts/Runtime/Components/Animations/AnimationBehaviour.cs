// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	public abstract class AnimationBehaviour : MonoBehaviour, IAnimation
	{
		private Coroutine _coroutine;

		public virtual void Play(Action onStart = null, Action onComplete = null)
		{
			_coroutine = StartCoroutine(AnimationCoroutineWithCallbacks(onStart, onComplete));
		}

		public void Stop()
		{
			if (_coroutine != null)
				StopCoroutine(_coroutine);
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