// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Structs
{
    [Serializable]
    public struct PoseDelta : IEquatable<PoseDelta>
    {
        public static readonly PoseDelta Identity = new PoseDelta(Vector3.zero, Quaternion.identity);
        public static bool LogZeroQuaternionWarning = true;

        public Vector3 PositionDelta { get; set; }
        private Quaternion _rotationDelta;
        public Quaternion RotationDelta
        {
            get
            {
                WarnAboutZeroQuaternion(_rotationDelta);

                return _rotationDelta;
            }
            set
            {
                WarnAboutZeroQuaternion(value);

                _rotationDelta = value;
            }
        }

        public PoseDelta(Pose prevPose, Pose nextPose) : this()
        {
            PositionDelta = nextPose.position - prevPose.position;
            RotationDelta = Quaternion.Inverse(prevPose.rotation) * nextPose.rotation;
        }

        public PoseDelta(Vector3 positionDelta, Quaternion rotationDelta) : this()
        {
            PositionDelta = positionDelta;
            RotationDelta = rotationDelta;
        }

        public static bool operator ==(PoseDelta a, PoseDelta b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(PoseDelta a, PoseDelta b)
        {
            return !a.Equals(b);
        }

        public static PoseDelta operator +(PoseDelta a, PoseDelta b)
        {
            a.PositionDelta += b.PositionDelta;
            a.RotationDelta *= b.RotationDelta;

            return a;
        }

        public static PoseDelta operator -(PoseDelta delta)
        {
            delta.PositionDelta = -delta.PositionDelta;
            delta.RotationDelta = Quaternion.Inverse(delta.RotationDelta);

            return delta;
        }

        public Pose ApplyToPose(Pose pose)
        {
            pose.position += PositionDelta;
            pose.rotation *= RotationDelta;

            return pose;
        }

        public Transform ApplyToTransform(Transform transform)
        {
            transform.position += PositionDelta;
            transform.rotation *= RotationDelta;

            return transform;
        }

        public bool Equals(PoseDelta other)
        {
            return PositionDelta.Equals(other.PositionDelta) && RotationDelta.Equals(other.RotationDelta);
        }

        public override bool Equals(object obj)
        {
            return obj is PoseDelta other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (PositionDelta.GetHashCode() * 397) ^ RotationDelta.GetHashCode();
            }
        }

        public override string ToString()
        {
            return PositionDelta + " : " + RotationDelta.eulerAngles;
        }

        public string ToString(string format)
        {
            return PositionDelta.ToString(format) + " : " + RotationDelta.eulerAngles.ToString(format);
        }

        public string ToStringWithQuaternionInternals()
        {
            return PositionDelta + " : " + RotationDelta;
        }

        public string ToStringWithQuaternionInternals(string format)
        {
            return PositionDelta.ToString(format) + " : " + RotationDelta.ToString(format);
        }

        private static void WarnAboutZeroQuaternion(Quaternion rotation)
        {
            if (LogZeroQuaternionWarning && rotation.Equals(default))
            {
                Debug.LogWarning(nameof(RotationDelta) + " has a value of \"default\" (which is \"[0, 0, 0, 0]\") " +
                    "and may cause bugs, because zero quaternion is invalid rotation. You probably want to use " +
                    "\"Quaternion.identity\" (which is \"[0, 0, 0, 1]\") instead.\n" +
                    "You can hide this warning in one of the following ways:\n" +
                    "1. Replace default value of PoseDelta instance with \"PoseDelta.Identity\".\n" +
                    "2. Replace default value of RotationDelta property with \"Quaternion.identity\" " +
                    "by using constructor (\"new PoseDelta(somePosition, Quaternion.identity)\") " +
                    "or by using public property itself (\"poseDelta.RotationDelta = Quaternion.identity\").\n" +
                    "3. Disable this warning project-wide somewhere in entry point of the application by using " +
                    "\"PoseDelta.LogZeroQuaternionWarning = false\". Do it only if you know what you are doing.");
            }
        }
    }

    public static class PoseExtensions
    {
        public static Pose AddDeltaPose(this Pose pose, PoseDelta poseDelta)
        {
            return poseDelta.ApplyToPose(pose);
        }
    }

    public static partial class TransformExtensions
    {
        public static Transform AddDeltaPose(this Transform transform, PoseDelta poseDelta)
        {
            return poseDelta.ApplyToTransform(transform);
        }
    }
}