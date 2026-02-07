// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Structs
{
	[Serializable]
	public struct PoseData
	{
		public Vector3 position;
		public Quaternion rotation;

		public override string ToString()
		{
			return position + " : " + rotation;
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