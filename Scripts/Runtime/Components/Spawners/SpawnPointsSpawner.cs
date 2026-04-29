// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using System.Linq;
using Evolutex.Evolunity.Extensions;
using Sirenix.Utilities;
using UnityEngine;

namespace Evolutex.Evolunity.Components
{
	public class SpawnPointsSpawner<T> : BaseSpawner<T> where T : Object
	{
		[SerializeField]
		private Transform[] _spawnPoints;

		private HashSet<Transform> _spawnPointsSet;

		private void Awake()
		{
			_spawnPointsSet = new HashSet<Transform>();
		}

		public override T GetClone()
		{
			if (_spawnPointsSet.Count == 0)
				_spawnPointsSet = _spawnPoints.Shuffle().ToHashSet();

			Transform spawnPoint = _spawnPointsSet.First();
			Vector3 spawnPosition = spawnPoint.position;
			_spawnPointsSet.Remove(spawnPoint);

			return Instantiate(Prefab, spawnPosition, Quaternion.identity, Parent);
		}
	}

	[AddComponentMenu("Evolunity/Spawners/Spawn Points Spawner")]
	public class SpawnPointsSpawner : SpawnPointsSpawner<GameObject>
	{
	}
}