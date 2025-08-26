// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using System.Linq;
using Evolutex.Evolunity.Utilities;
using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return vector;
        }

        public static Vector3 ToVector3(this Vector2 vector)
        {
            return vector;
        }

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
        /// Randomize vector from [-1, -1] to [1, 1].
        /// </summary>
        public static Vector2 Randomize(this Vector2 vector)
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        /// <summary>
        /// Randomize vector from [-1, -1, -1] to [1, 1, 1].
        /// </summary>
        public static Vector3 Randomize(this Vector3 vector)
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        /// <summary>
        /// Randomize vector with given coordinate limits.
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
        /// Randomize vector with given coordinate limits.
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
            angles.x = MathUtility.NormalizeAngle(angles.x);
            angles.y = MathUtility.NormalizeAngle(angles.y);
            angles.z = MathUtility.NormalizeAngle(angles.z);

            return angles;
        }

        public static Vector2 ClampMagnitude(this Vector2 vector, float maxLength)
        {
            return Vector2.ClampMagnitude(vector, maxLength);
        }

        public static Vector3 ClampMagnitude(this Vector3 vector, float maxLength)
        {
            return Vector3.ClampMagnitude(vector, maxLength);
        }

        // Vector4: https://www.dropbox.com/s/el9mci24o10j3z3/VectorExtensions.cs?dl=0
        // https://en.wikipedia.org/wiki/Swizzling_%28computer_graphics%29

        public static Vector2 XY(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static Vector2 XZ(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }

        public static Vector2 YZ(this Vector3 vector)
        {
            return new Vector2(vector.y, vector.z);
        }

        public static Vector2 YX(this Vector3 vector)
        {
            return new Vector2(vector.y, vector.x);
        }

        public static Vector2 ZX(this Vector3 vector)
        {
            return new Vector2(vector.z, vector.x);
        }

        public static Vector2 ZY(this Vector3 vector)
        {
            return new Vector2(vector.z, vector.y);
        }

        /// <summary>
        /// https://discussions.unity.com/t/vector3-comparison-efficiency-and-float-precision/62649/2
        /// </summary>
        /// <param name="precision"><see cref="Mathf.Epsilon"/> by default</param>
        public static bool ApproximatelyEqual(this Vector3 vector, Vector3 other, float precision = default)
        {
            if (precision == default)
                precision = Mathf.Epsilon;

            if (Mathf.Abs(vector.x - other.x) > precision) return false;
            if (Mathf.Abs(vector.y - other.y) > precision) return false;
            if (Mathf.Abs(vector.z - other.z) > precision) return false;

            return true;
        }

        public static bool IsInteger(this Vector3 vector)
        {
            return Mathf.Approximately(vector.x, Mathf.Round(vector.x))
                && Mathf.Approximately(vector.y, Mathf.Round(vector.y))
                && Mathf.Approximately(vector.z, Mathf.Round(vector.z));
        }

        public static float GetPathLength(this IEnumerable<Vector3> vectors)
        {
            if (vectors == null)
                return 0f;

            return vectors
                .Zip(vectors.Skip(1), Vector3.Distance)
                .Sum();
        }
    }
}