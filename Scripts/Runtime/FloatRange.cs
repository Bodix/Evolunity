/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

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