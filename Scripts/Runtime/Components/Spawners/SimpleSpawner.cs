// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components
{
	public class SimpleSpawner<T> : BaseSpawner<T> where T : Object
	{
		protected override T CreateClone(Vector3 validPosition)
		{
			return Instantiate(
				Prefab,
				validPosition,
				transform.rotation,
				Parent);
		}

		protected override Vector3 GetPotentialSpawnPosition()
		{
			return transform.position;
		}
	}

	[AddComponentMenu("Evolunity/Spawners/Simple Spawner")]
	public class SimpleSpawner : SimpleSpawner<GameObject>
	{
	}
}