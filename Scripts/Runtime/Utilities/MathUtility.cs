// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Utilities
{
    public static class MathUtility
    {
        /// <summary>
        /// https://www.youtube.com/watch?v=YCFt0L5KNWE
        /// </summary>
        public static Vector2 GetPhyllotaxisPosition(int count, float scale, float degree = 137.5f)
        {
            float radius = scale * Mathf.Sqrt(count);
            float angle = count * (degree * Mathf.Deg2Rad);

            return GetCirclePosition(radius, angle);
        }

        /// <summary>
        /// Parametric equation of the circle.
        /// </summary>
        public static Vector2 GetCirclePosition(float radius, float angle)
        {
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            return new Vector2(x, y);
        }
        
        /// <summary>
        /// Parametric equation of the sphere.
        /// </summary>
        public static Vector3 GetSpherePosition(float radius, float horizontalAngle, float verticalAngle)
        {
            float x = radius * Mathf.Sin(verticalAngle) * Mathf.Cos(horizontalAngle);
            float y = radius * Mathf.Cos(verticalAngle);
            float z = radius * Mathf.Sin(verticalAngle) * Mathf.Sin(horizontalAngle);

            return new Vector3(x, y, z);
        }
        
        /// <summary>
        /// Normalize angle within [-360, 360].
        /// </summary>
        public static float NormalizeAngle(float angle)
        {
            if (angle == -360 || angle == 360)
                return angle;
            
            return angle % 360;
        }
    }
}