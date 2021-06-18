// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Evolutex.Evolunity.Structs
{
    // TODO:
    // 1. Intersection.
    // 2. IntersectionLength.
    // 3. NormalizedIntersectionLength.
    // 4. Intersects.
    // 5. Overlaps.
    // 6. '+', '-', '*', '/' operators.
    
    [Serializable]
    public struct FloatRange
    {
        public float Min;
        public float Max;
        
        public FloatRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Length => Max - Min;

        public float RandomWithin => Random.Range(Min, Max);

        public float Lerp(float t)
        {
            return Mathf.Lerp(Min, Max, t);
        }

        public float LerpUnclamped(float t)
        {
            return Mathf.LerpUnclamped(Min, Max, t);
        }

        public float InverseLerp(float t)
        {
            return Mathf.InverseLerp(Min, Max, t);
        }

        public float Clamp(float value)
        {
            return Mathf.Clamp(value, Min, Max);
        }

        public bool Contains(float value)
        {
            return Min <= value && value <= Max;
        }
        
        public bool ContainsExcludingBounds(int value)
        {
            return Min < value && value < Max;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(Min, Max);
        }
        
        public bool Equals(FloatRange other)
        {
            return Min.Equals(other.Min) && Max.Equals(other.Max);
        }

        public override bool Equals(object obj)
        {
            return obj is FloatRange other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Min.GetHashCode() * 397) ^ Max.GetHashCode();
            }
        }

        public static implicit operator FloatRange(Vector2 vector)
        {
            return new FloatRange(vector.x, vector.y);
        }

        public static implicit operator Vector2(FloatRange range)
        {
            return range.ToVector2();
        }

        public static bool operator ==(FloatRange left, FloatRange right)
        {
            return left.Equals(right);
        }
        
        public static bool operator !=(FloatRange left, FloatRange right)
        {
            return !left.Equals(right);
        }
    }
}