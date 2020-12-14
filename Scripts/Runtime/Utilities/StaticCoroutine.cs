/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

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