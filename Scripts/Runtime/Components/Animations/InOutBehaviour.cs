// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections;
using UnityEngine;

namespace Evolutex.Evolunity.Components.Animations
{
    public abstract class InOutBehaviour : MonoBehaviour, IInOutPlayable
    {
        protected abstract IEnumerator InCoroutine(Action onComplete = null);
        
        protected abstract IEnumerator OutCoroutine(Action onComplete = null);
        
        [ContextMenu("Play In")]
        public void PlayIn()
        {
            PlayInCoroutine();
        }
        
        [ContextMenu("Play Out")]
        public void PlayOut()
        {
            PlayOutCoroutine();
        }

        public Coroutine PlayInCoroutine(Action onComplete = null)
        {
            return StartCoroutine(InCoroutine(onComplete));
        }

        public Coroutine PlayOutCoroutine(Action onComplete = null)
        {
            return StartCoroutine(OutCoroutine(onComplete));
        }

        public Coroutine PlayInOutCoroutine(Action onInComplete = null, Action onOutComplete = null)
        {
            return StartCoroutine(InOutCoroutine(onInComplete, onOutComplete));
        }

        private IEnumerator InOutCoroutine(Action onInComplete = null, Action onOutComplete = null)
        {
            yield return InCoroutine(onInComplete);
            yield return OutCoroutine(onOutComplete);
        }
    }
}