// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections;
using UnityEngine;

namespace Evolunity.Utilities
{
    public static class StaticCoroutine
    {
        private static StaticCoroutineComponent component;

        public static Coroutine Start(IEnumerator routine)
        {
            if (!component)
            {
                component = new GameObject("Static Coroutine Object")
                    {
                        hideFlags = HideFlags.HideAndDontSave
                    }
                    .AddComponent<StaticCoroutineComponent>();
            }

            return component.StartCoroutine(routine);
        }

        private class StaticCoroutineComponent : MonoBehaviour { }
    }
}