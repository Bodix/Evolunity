// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Structs
{
	[Serializable]
	public struct PoseData : IEquatable<PoseData>
	{
		public Vector3 position;
		public Quaternion rotation;

		public override string ToString()
		{
			return position + " : " + rotation;
		}

		public bool Equals(PoseData other)
		{
			return position == other.position && rotation == other.rotation;
		}

		public override bool Equals(object obj)
		{
			return obj is PoseData other && Equals(other);
		}

		public override int GetHashCode()
		{
#if UNITY_2021_2_OR_NEWER
			return HashCode.Combine(position, rotation);
#else
			unchecked
			{
				return (position.GetHashCode() * 397) ^ rotation.GetHashCode();
			}
#endif
		}

		/// <param name="other"> </param>
		/// <param name="positionSqrEpsilon">To convert metres to sqrMagnitude, simply square them.</param>
		/// <param name="rotationDotEpsilon">To convert degrees to Dot Product, use the cosine of half angle formula: Dot = cos(Angle / 2).</param>
		public bool ApproximatelyEquals(PoseData other, float positionSqrEpsilon, float rotationDotEpsilon)
		{
			bool positionMatch = (position - other.position).sqrMagnitude < positionSqrEpsilon;
			bool rotationMatch = Mathf.Abs(Quaternion.Dot(rotation, other.rotation)) > rotationDotEpsilon;

			return positionMatch && rotationMatch;
		}

		public static bool operator ==(PoseData left, PoseData right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(PoseData left, PoseData right)
		{
			return !left.Equals(right);
		}

		public static implicit operator Pose(PoseData data)
		{
			return new Pose(data.position, data.rotation);
		}

		public static implicit operator PoseData(Pose pose)
		{
			return new PoseData
			{
				position = pose.position,
				rotation = pose.rotation
			};
		}
	}
}