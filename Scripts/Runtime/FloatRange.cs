// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using Random = UnityEngine.Random;

namespace Evolunity
{
    [Serializable]
    public struct FloatRange
    {
        public FloatRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Min;
        public float Max;
        public float RandomWithin => Random.Range(Min, Max);
    }
}