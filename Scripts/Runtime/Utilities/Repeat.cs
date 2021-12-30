// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections;
using UnityEngine;

namespace Evolutex.Evolunity.Utilities
{
    public static class Repeat
    {
        public static Coroutine EveryFrame(Action onRepeat, MonoBehaviour coroutineOwner = null)
        {
            return EveryFrames(1, onRepeat, coroutineOwner);
        }
        
        public static Coroutine EveryFrames(int frames, Action onRepeat, MonoBehaviour coroutineOwner = null)
        {
            ThrowIfNotGreaterThanZero(frames);
            
            if (coroutineOwner)
                return coroutineOwner.StartCoroutine(FramesRepeat(frames, onRepeat));
            else
                return StaticCoroutine.Start(FramesRepeat(frames, onRepeat));
        }

        public static Coroutine EverySeconds(float seconds, Action onRepeat, MonoBehaviour coroutineOwner = null)
        {
            ThrowIfNotGreaterThanZero(seconds);

            if (coroutineOwner)
                return coroutineOwner.StartCoroutine(SecondsRepeat(seconds, onRepeat));
            else
                return StaticCoroutine.Start(SecondsRepeat(seconds, onRepeat));
        }

        private static IEnumerator FramesRepeat(int frames, Action onRepeat)
        {
            while (true)
            {
                for (int i = 0; i < frames; i++)
                    yield return null;

                onRepeat();
            }
        }

        private static IEnumerator SecondsRepeat(float seconds, Action onRepeat)
        {
            while (true)
            {
                yield return new WaitForSeconds(seconds);

                onRepeat();
            }
        }

        private static void ThrowIfNotGreaterThanZero(float delay)
        {
            if (delay <= 0)
                throw new ArgumentException("Delay can't be less than or equal to zero");
        }
    }
}