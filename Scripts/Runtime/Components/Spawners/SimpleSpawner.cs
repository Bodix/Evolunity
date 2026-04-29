// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Components
{
	public class SimpleSpawner<T> : BaseSpawner<T> where T : Object
	{
		public bool UseSpawnerPosition = true;
		public bool UseSpawnerRotation = true;

		public override T GetClone()
		{
			return Instantiate(
				Prefab,
				GetSpawnPosition(),
				UseSpawnerRotation ? transform.rotation : Quaternion.identity,
				Parent);
		}

		public override Vector3 GetSpawnPosition()
		{
			return UseSpawnerPosition ? transform.position : Vector3.zero;
		}
	}

	[AddComponentMenu("Evolunity/Spawners/Simple Spawner")]
	public class SimpleSpawner : SimpleSpawner<GameObject>
	{
	}
}