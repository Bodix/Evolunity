// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Components
{
	public class CircleZoneSpawner<T> : BaseSpawner<T> where T : Object
	{
		public float Radius = 10;

		public override T GetClone()
		{
			return Instantiate(Prefab, GetSpawnPosition(), Quaternion.identity, Parent);
		}

		public override Vector3 GetSpawnPosition()
		{
			Vector2 position = Random.insideUnitCircle * Radius;

			return new Vector3(position.x, 0, position.y);
		}
	}

	[AddComponentMenu("Evolunity/Spawners/Circle Zone Spawner")]
	public class CircleZoneSpawner : CircleZoneSpawner<GameObject>
	{
	}
}