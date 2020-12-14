/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using System;
using System.Collections;
using UnityEngine;

namespace Evolunity.Utilities
{
    public static class Delay
    {
        public static void ForOneFrame(Action onComplete)
        {
            ForFrames(1, onComplete);
        }

        public static void ForFrames(int frames, Action onComplete)
        {
            if (frames > 0)
                StaticCoroutine.Start(FramesDelay(frames, onComplete));
            else
                onComplete();
        }

        public static void ForSeconds(float seconds, Action onComplete)
        {
            if (seconds > 0)
                StaticCoroutine.Start(SecondsDelay(seconds, onComplete));
            else
                onComplete();
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
    }
}