// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Structs
{
    public readonly struct Direction
    {
        public static Direction Up => new Direction(Vector2.up);
        public static Direction Down => new Direction(Vector2.down);
        public static Direction Right => new Direction(Vector2.right);
        public static Direction Left => new Direction(Vector2.left);
        
        public readonly Vector2 NormalizedVector;

        public readonly Vector2 Vector;
    
        /// <summary>
        /// Signed angle from Vector2.right.
        /// </summary>
        public readonly float Angle;
        
        private readonly Func<Vector2, float> axisSelector;

        private Direction(Vector2 vector)
        {
            NormalizedVector = vector.normalized;
            Vector = vector;
            Angle = Vector2.SignedAngle(Vector2.right, NormalizedVector);
            
            axisSelector = v => vector.x * v.x + vector.y * v.y;
        }

        public float GetAxis(Vector2 vector)
        {
            return axisSelector?.Invoke(vector) ?? 0f;
        }
    
        public bool Equals(Direction other)
        {
            return NormalizedVector.Equals(other.NormalizedVector) 
                   && Angle.Equals(other.Angle) 
                   && axisSelector.Equals(other.axisSelector);
        }

        public override bool Equals(object obj)
        {
            return obj is Direction other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = NormalizedVector.GetHashCode();
                hashCode = (hashCode * 397) ^ Angle.GetHashCode();
                hashCode = (hashCode * 397) ^ (axisSelector != null ? axisSelector.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Direction direction1, Direction direction2)
        {
            return direction1.Equals(direction2);
        }

        public static bool operator !=(Direction direction1, Direction direction2)
        {
            return !direction1.Equals(direction2);
        }
    }
}