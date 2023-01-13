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
}