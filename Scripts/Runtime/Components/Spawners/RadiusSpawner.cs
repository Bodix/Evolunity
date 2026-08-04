// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components
{
	public class RadiusSpawner<T> : BaseSpawner<T> where T : Object
	{
		[Space]
		public Transform Origin;
		public float MinRadius = 30;
		public float MaxRadius = 45;

		protected override T CreateClone(Vector3 validPosition)
		{
			return Instantiate(Prefab, validPosition, Quaternion.identity, Parent);
		}

		protected override Vector3 GetPotentialSpawnPosition()
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