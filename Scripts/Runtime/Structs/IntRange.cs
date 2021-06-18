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
    public struct IntRange
    {
        public int Min;
        public int Max;
        
        public IntRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int Length => Max - Min;
        
        public int RandomWithin => Random.Range(Min, Max + 1);
        
        public int Clamp(int value)
        {
            return Mathf.Clamp(value, Min, Max);
        }

        public bool Contains(int value)
        {
            return Min <= value && value <= Max;
        }
        
        public bool ContainsExcludingBounds(int value)
        {
            return Min < value && value < Max;
        }

        public Vector2Int ToVector2Int()
        {
            return new Vector2Int(Min, Max);
        }
        
        public bool Equals(IntRange other)
        {
            return Min == other.Min && Max == other.Max;
        }

        public override bool Equals(object obj)
        {
            return obj is IntRange other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Min * 397) ^ Max;
            }
        }
        
        public static implicit operator IntRange(Vector2Int vector)
        {
            return new IntRange(vector.x, vector.y);
        }

        public static implicit operator Vector2Int(IntRange range)
        {
            return range.ToVector2Int();
        }

        public static bool operator ==(IntRange left, IntRange right)
        {
            return left.Equals(right);
        }
        
        public static bool operator !=(IntRange left, IntRange right)
        {
            return !left.Equals(right);
        }
    }
}