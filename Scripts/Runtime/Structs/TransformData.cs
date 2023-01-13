// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Structs
{
    [Serializable]
    public readonly struct TransformData
    {
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;
        public readonly Vector3 LocalScale;

        public TransformData(Vector3 position, Quaternion rotation, Vector3 localScale)
        {
            Position = position;
            Rotation = rotation;
            LocalScale = localScale;
        }
    }

    public static class TransformExtensions
    {
        public static TransformData GetData(this Transform transform)
        {
            return new TransformData(transform.position, transform.rotation, transform.localScale);
        }

        public static void SetData(this Transform transform, TransformData data)
        {
            transform.position = data.Position;
            transform.rotation = data.Rotation;
            transform.localScale = data.LocalScale;
        }
    }
}