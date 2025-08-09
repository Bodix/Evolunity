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
        /// <summary>
        /// Signed angle from Vector2.right.
        /// </summary>
        public readonly float Angle;

        // private readonly bool _isInitialized;
        private readonly Func<Vector2, float> _axisSelector;

        private Direction(Vector2 vector)
        {
            NormalizedVector = vector.normalized;
            Angle = Vector2.SignedAngle(Vector2.right, NormalizedVector);

            // _isInitialized = true;
            _axisSelector = v => vector.x * v.x + vector.y * v.y;
        }

        public float GetAxis(Vector2 vector)
        {
            return _axisSelector?.Invoke(vector) ?? 0f;
        }

        public bool Equals(Direction other)
        {
            return NormalizedVector.Equals(other.NormalizedVector) 
                   && Angle.Equals(other.Angle) 
                   && _axisSelector.Equals(other._axisSelector);
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
                hashCode = (hashCode * 397) ^ (_axisSelector != null ? _axisSelector.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return NormalizedVector.ToString();
        }

        // private void CheckInitialization()
        // {
        //     if (!_isInitialized)
        //         Debug.LogError(@"Struct is not created correctly. This could have happened in one of two cases:
        //             - either the structure was created using a standard constructor;
        //             - or the structure was created using default(Direction).
        //             To create the structure correctly, use one of the following methods:
        //             - either create the structure using a constructor with a parameter. For example: “new Direction(new Vector2(1, 0))”
        //             - or create the structure using a static method. For example: “Direction.Right”");
        // }

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