// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections;
using UnityEngine;

namespace Evolutex.Evolunity.Utilities
{
    public static class Delay
    {
        public static Coroutine ForOneFrame(Action onComplete)
        {
            return ForFrames(1, onComplete);
        }

        public static Coroutine ForFrames(int frames, Action onComplete, MonoBehaviour coroutineOwner = null)
        {
            ThrowIfLessThanZero(frames);

            if (frames > 0)
            {
                if (coroutineOwner)
                    return coroutineOwner.StartCoroutine(FramesDelay(frames, onComplete));
                else
                    return StaticCoroutine.Start(FramesDelay(frames, onComplete));
            }
            else
            {
                onComplete();
                
                return null;
            }
        }

        public static Coroutine ForSeconds(float seconds, Action onComplete, MonoBehaviour coroutineOwner = null)
        {
            ThrowIfLessThanZero(seconds);

            if (seconds > 0)
            {
                if (coroutineOwner)
                    return coroutineOwner.StartCoroutine(SecondsDelay(seconds, onComplete));
                else
                    return StaticCoroutine.Start(SecondsDelay(seconds, onComplete));
            }
            else
            {
                onComplete();
                
                return null;
            }
        }

        private static IEnumerator FramesDelay(int frames, Action onComplete)
        {
            for (int i = 0; i < frames; i++)
                yield return null;

            onComplete();
        }

        private static IEnumerator SecondsDelay(float seconds, Action onComplete)
        {
            yield return new WaitForSeconds(seconds);

            onComplete();
        }

        private static void ThrowIfLessThanZero(float delay)
        {
            if (delay < 0)
                throw new ArgumentException("Delay can't be less than zero");
        }
    }
}