// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components
{
	public class SimpleSpawner<T> : BaseSpawner<T> where T : Object
	{
		public override T GetClone()
		{
			return Instantiate(
				Prefab,
				GetSpawnPosition(),
				transform.rotation,
				Parent);
		}

		public override Vector3 GetSpawnPosition()
		{
			return transform.position;
		}
	}

	[AddComponentMenu("Evolunity/Spawners/Simple Spawner")]
	public class SimpleSpawner : SimpleSpawner<GameObject>
	{
	}
}