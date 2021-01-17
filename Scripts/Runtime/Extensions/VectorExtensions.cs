// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using Evolutex.Evolunity.Utilities;

namespace Evolutex.Evolunity.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 WithX(this Vector2 vector, float value)
        {
            vector.x = value;

            return vector;
        }

        public static Vector2 WithY(this Vector2 vector, float value)
        {
            vector.y = value;

            return vector;
        }

        public static Vector3 WithX(this Vector3 vector, float value)
        {
            vector.x = value;

            return vector;
        }

        public static Vector3 WithY(this Vector3 vector, float value)
        {
            vector.y = value;

            return vector;
        }

        public static Vector3 WithZ(this Vector3 vector, float value)
        {
            vector.z = value;

            return vector;
        }

        public static Vector2 AddX(this Vector2 vector, float value)
        {
            vector.x += value;

            return vector;
        }

        public static Vector2 AddY(this Vector2 vector, float value)
        {
            vector.y += value;

            return vector;
        }

        public static Vector3 AddX(this Vector3 vector, float value)
        {
            vector.x += value;

            return vector;
        }

        public static Vector3 AddY(this Vector3 vector, float value)
        {
            vector.y += value;

            return vector;
        }

        public static Vector3 AddZ(this Vector3 vector, float value)
        {
            vector.z += value;

            return vector;
        }

        /// <summary>
        /// Randomize current vector from [-1, -1] to [1, 1].
        /// </summary>
        public static Vector2 Randomize(this Vector2 vector)
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        /// <summary>
        /// Randomize current vector from [-1, -1, -1] to [1, 1, 1].
        /// </summary>
        public static Vector3 Randomize(this Vector3 vector)
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        /// <summary>
        /// Randomize current vector with given coordinate limits.
        /// If one of the limits is <c>null</c>, coordinate is set to the value from the original vector.
        /// </summary>
        public static Vector2 Randomize(this Vector2 vector,
            (float, float)? xLimits, (float, float)? yLimits)
        {
            return new Vector3(
                xLimits != null ? Random.Range(xLimits.Value.Item1, xLimits.Value.Item2) : vector.x,
                yLimits != null ? Random.Range(yLimits.Value.Item1, yLimits.Value.Item2) : vector.y);
        }

        /// <summary>
        /// Randomize current vector with given coordinate limits.
        /// If one of the limits is <c>null</c>, coordinate is set to the value from the original vector.
        /// </summary>
        public static Vector3 Randomize(this Vector3 vector,
            (float, float)? xLimits, (float, float)? yLimits, (float, float)? zLimits)
        {
            return new Vector3(
                xLimits != null ? Random.Range(xLimits.Value.Item1, xLimits.Value.Item2) : vector.x,
                yLimits != null ? Random.Range(yLimits.Value.Item1, yLimits.Value.Item2) : vector.y,
                zLimits != null ? Random.Range(zLimits.Value.Item1, zLimits.Value.Item2) : vector.z);
        }

        public static Vector3 NormalizeAngles(this Vector3 angles)
        {
            angles.x = Angle.Normalize(angles.x);
            angles.y = Angle.Normalize(angles.y);
            angles.z = Angle.Normalize(angles.z);
            
            return angles;
        }
    }
}