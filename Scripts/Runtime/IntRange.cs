// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using Random = UnityEngine.Random;

namespace Evolunity
{
    [Serializable]
    public struct IntRange
    {
        public IntRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public float Min;
        public float Max;
        public float RandomWithin => Random.Range(Min, Max + 1);
    }
}