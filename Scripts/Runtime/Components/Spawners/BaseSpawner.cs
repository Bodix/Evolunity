// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using Evolutex.Evolunity.Extensions;
using NaughtyAttributes;
using UnityEngine;

namespace Evolutex.Evolunity.Components
{
	public abstract class BaseSpawner<T> : PeriodicBehaviour where T : UnityEngine.Object
	{
		public SpawnMethod SpawnMethod = SpawnMethod.Start;
		public T Prefab;
		public uint Amount = 1;
		public Transform Parent;

		[Header("Raycast")]
		public bool IsRaycastCheck = false;
		[ShowIf(nameof(IsRaycastCheck))]
		public Vector3 RaycastDirection = Vector3.down;
		[ShowIf(nameof(IsRaycastCheck))]
		public float RaycastHeight = 50;
		[ShowIf(nameof(IsRaycastCheck))]
		public float RaycastDistance = 100;
		[ShowIf(nameof(IsRaycastCheck))]
		public LayerMask RaycastAllowedLayers = 1;
		[ShowIf(nameof(IsRaycastCheck))]
		public bool IsSphereCheck = false;
		[ShowIf(EConditionOperator.And, nameof(IsRaycastCheck), nameof(IsSphereCheck))]
		public float SphereCheckRadius = 2;
		[ShowIf(EConditionOperator.And, nameof(IsRaycastCheck), nameof(IsSphereCheck))]
		public LayerMask SphereCheckDisallowedLayers = 0;

		private readonly List<T> _buffer = new List<T>();

		public event Action<List<T>> Spawned;

		public override bool DrawPeriodFieldInInspector => SpawnMethod == SpawnMethod.Periodic;
		public override bool DrawPeriodProgressInInspector => SpawnMethod == SpawnMethod.Periodic;

		private void Reset()
		{
			Parent = transform;
		}

		private void Start()
		{
			if (SpawnMethod == SpawnMethod.Start)
				Spawn();
		}

		protected override void Update()
		{
			if (SpawnMethod == SpawnMethod.Update)
				Spawn();
			else
				base.Update();
		}

		protected override void OnPeriod()
		{
			if (SpawnMethod == SpawnMethod.Periodic)
				Spawn();
		}

		public abstract T GetClone();
		public abstract Vector3 GetSpawnPosition();

		public void Spawn()
		{
			_buffer.Clear();

			if (IsRaycastCheck && !IsSpawnPointValid())
				return;

			for (int i = 0; i < Amount; i++)
				_buffer.Add(GetClone());

			Spawned?.Invoke(_buffer);
		}

		private bool IsSpawnPointValid()
		{
			if (UnityEngine.Physics.Raycast(GetSpawnPosition().WithY(RaycastHeight), RaycastDirection, out RaycastHit hit, RaycastDistance))
			{
				if (((1 << hit.collider.gameObject.layer) & RaycastAllowedLayers) != 0)
				{
					if (IsSphereCheck)
					{
						return !UnityEngine.Physics.CheckSphere(hit.point, SphereCheckRadius, SphereCheckDisallowedLayers);
					}
					else
					{
						return true;
					}
				}
			}

			return false;
		}
	}

	public enum SpawnMethod
	{
		Manual,
		Start,
		Update,
		Periodic
	}
}