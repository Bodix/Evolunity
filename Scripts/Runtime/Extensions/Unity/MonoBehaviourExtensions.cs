// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections;
using UnityEngine;
using Evolutex.Evolunity.Utilities;

namespace Evolutex.Evolunity.Extensions
{
    public static class MonoBehaviourExtensions
    {
        // TODO:
        // WaitInWrappedCoroutine
        // WaitForEndOfFrame
        // WaitForFixedUpdate
        // WaitForSeconds
        // WaitForSecondsRealtime

        public static WrappedCoroutine StartWrappedCoroutine(this MonoBehaviour monoBehaviour, IEnumerator routine)
        {
            return new WrappedCoroutine(monoBehaviour, routine).Start();
        }

        public static Coroutine WaitInCoroutineUntil(this MonoBehaviour monoBehaviour, Func<bool> conditionPredicate,
            Action onComplete)
        {
            return monoBehaviour.StartCoroutine(WaitUntil(conditionPredicate, onComplete));
        }

        private static IEnumerator WaitUntil(Func<bool> conditionPredicate, Action onComplete)
        {
            yield return new WaitUntil(conditionPredicate);

            onComplete.Invoke();
        }

        public static Coroutine WaitInCoroutineWhile(this MonoBehaviour monoBehaviour, Func<bool> conditionPredicate,
            Action onComplete)
        {
            return monoBehaviour.StartCoroutine(WaitWhile(conditionPredicate, onComplete));
        }

        private static IEnumerator WaitWhile(Func<bool> conditionPredicate, Action onComplete)
        {
            yield return new WaitWhile(conditionPredicate);

            onComplete.Invoke();
        }
    }
}