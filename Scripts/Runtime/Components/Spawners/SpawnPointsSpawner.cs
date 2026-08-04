// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using System.Linq;
using Bodix.Evolunity.Extensions;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	public class SpawnPointsSpawner<T> : BaseSpawner<T> where T : Object
	{
		[Space]
		public Transform[] SpawnPoints;

		private HashSet<Transform> _spawnPointsSet;

		private void Awake()
		{
			_spawnPointsSet = new HashSet<Transform>();
		}

		protected override T CreateClone(Vector3 validPosition)
		{
			return Instantiate(Prefab, validPosition, Quaternion.identity, Parent);
		}

		protected override Vector3 GetPotentialSpawnPosition()
		{
			if (_spawnPointsSet.Count == 0)
				_spawnPointsSet = new HashSet<Transform>(SpawnPoints.Shuffle());

			Transform spawnPoint = _spawnPointsSet.First();
			_spawnPointsSet.Remove(spawnPoint);

			return spawnPoint.position;
		}
	}

	[AddComponentMenu("Evolunity/Spawners/Spawn Points Spawner")]
	public class SpawnPointsSpawner : SpawnPointsSpawner<GameObject>
	{
	}
}