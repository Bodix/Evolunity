/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    // http://wiki.unity3d.com/index.php/QuaternionExtensions
    public static class QuaternionExtensions
    {
        /// <summary>
        /// Quaternion raised to a power.
        /// This is useful for smoothly multiplying a Quaternion by a given floating-point value.
        /// </summary>
        public static Quaternion Pow(this Quaternion quaternion, float power)
        {
            float magnitude = quaternion.Magnitude();
            Vector3 normalizedVector = new Vector3(quaternion.x, quaternion.y, quaternion.z).normalized;

            return new Quaternion(normalizedVector.x, normalizedVector.y, normalizedVector.z, 0)
                .ScalarMultiply(power * Mathf.Acos(quaternion.w / magnitude))
                .Exp()
                .ScalarMultiply(Mathf.Pow(magnitude, power));
        }
        
        /// <summary>
        /// Euler's number raised to quaternion.
        /// </summary>
        public static Quaternion Exp(this Quaternion quaternion)
        {
            Vector3 inputVector = new Vector3(quaternion.x, quaternion.y, quaternion.z);
            float inputScalar = quaternion.w;

            Vector3 outputVector = Mathf.Exp(inputScalar) * (inputVector.normalized * Mathf.Sin(inputVector.magnitude));
            float outputScalar = Mathf.Exp(inputScalar) * Mathf.Cos(inputVector.magnitude);

            return new Quaternion(outputVector.x, outputVector.y, outputVector.z, outputScalar);
        }
        
        public static float Magnitude(this Quaternion quaternion)
        {
            return Mathf.Sqrt(quaternion.x * quaternion.x +
                              quaternion.y * quaternion.y +
                              quaternion.z * quaternion.z +
                              quaternion.w * quaternion.w);
        }

        public static Quaternion ScalarMultiply(this Quaternion quaternion, float scalar)
        {
            return new Quaternion(
                quaternion.x * scalar,
                quaternion.y * scalar,
                quaternion.z * scalar,
                quaternion.w * scalar);
        }
    }
}