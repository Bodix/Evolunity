// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Components
{
	public class RadiusSpawner<T> : BaseSpawner<T> where T : Object
	{
		[Space]
		public Transform Origin;
		public float MinRadius = 30;
		public float MaxRadius = 45;

		public override T GetClone()
		{
			return Instantiate(Prefab, GetSpawnPosition(), Quaternion.identity, Parent);
		}

		public override Vector3 GetSpawnPosition()
		{
			Vector2 direction = Random.insideUnitCircle.normalized;
			float distance = Random.Range(MinRadius, MaxRadius);

			return Origin.position + new Vector3(direction.x, 0, direction.y) * distance;
		}
	}

	[AddComponentMenu("Evolunity/Spawners/Radius Spawner")]
	public class RadiusSpawner : RadiusSpawner<GameObject>
	{
	}
}