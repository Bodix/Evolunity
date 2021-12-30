// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections;
using UnityEngine;

namespace Evolutex.Evolunity.Utilities
{
    public static class StaticCoroutine
    {
        private static StaticCoroutineComponent component;

        public static Coroutine Start(IEnumerator routine)
        {
            if (!component)
            {
                component = new GameObject("Static Coroutine")
                    {
                        hideFlags = HideFlags.HideAndDontSave
                    }
                    .AddComponent<StaticCoroutineComponent>();
            }

            return component.StartCoroutine(routine);
        }

        /// <summary>
        /// Used to stop coroutines that were created with <see cref="StaticCoroutine"/> class.
        ///
        /// <para/>If you try to call StopCoroutine method from another component,
        /// the following error will appear in the console:
        /// <br/>Coroutine continue failure
        ///
        /// <para/>https://issuetracker.unity3d.com/issues/coroutine-continue-failure-error-when-using-stopcoroutine
        /// <br/>http://answers.unity.com/answers/1233994/view.html
        /// </summary>
        public static void Stop(Coroutine routine)
        {
            component.StopCoroutine(routine);
        }

        private class StaticCoroutineComponent : MonoBehaviour
        {
        }
    }
}