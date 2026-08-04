// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components
{
	public class CircleZoneSpawner<T> : BaseSpawner<T> where T : Object
	{
		[Space]
		public float Radius = 10;

		protected override T CreateClone(Vector3 validPosition)
		{
			return Instantiate(Prefab, validPosition, Quaternion.identity, Parent);
		}

		protected override Vector3 GetPotentialSpawnPosition()
		{
			Vector2 position = Random.insideUnitCircle * Radius;

			return new Vector3(transform.position.x + position.x, transform.position.y, transform.position.z + position.y);
		}
	}

	[AddComponentMenu("Evolunity/Spawners/Circle Zone Spawner")]
	public class CircleZoneSpawner : CircleZoneSpawner<GameObject>
	{
	}
}