// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using Bodix.Evolunity.Extensions;
using NaughtyAttributes;
using UnityEngine;

namespace Bodix.Evolunity.Components
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
		public LayerCheckMode RaycastMode = LayerCheckMode.Allowed;
		[ShowIf(nameof(IsRaycastCheck))]
		public LayerMask RaycastLayers = 1;

		[Header("Sphere Check")]
		public bool IsSphereCheck = false;
		[ShowIf(nameof(IsSphereCheck))]
		public float SphereCheckRadius = 2;
		[ShowIf(nameof(IsSphereCheck))]
		public LayerCheckMode SphereCheckMode = LayerCheckMode.Disallowed;
		[ShowIf(nameof(IsSphereCheck))]
		public LayerMask SphereCheckLayers = 0;

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
			if (!IsSpawnPointValid())
				return;

			_buffer.Clear();
			for (int i = 0; i < Amount; i++)
				_buffer.Add(GetClone());

			Spawned?.Invoke(_buffer);
		}

		private bool IsSpawnPointValid()
		{
			// If no checks are enabled, the point is valid by default.
			if (!IsRaycastCheck && !IsSphereCheck)
				return true;

			Vector3 targetPosition = GetSpawnPosition();

			if (IsRaycastCheck)
			{
				bool hasHit = Physics.Raycast(targetPosition.WithY(RaycastHeight), RaycastDirection, out RaycastHit hit, RaycastDistance);

				if (RaycastMode == LayerCheckMode.Allowed)
				{
					if (!hasHit || ((1 << hit.collider.gameObject.layer) & RaycastLayers) == 0)
						return false;

					// Update target position for the sphere check based on the raycast hit.
					targetPosition = hit.point;
				}
				else
				{
					if (hasHit)
					{
						if (((1 << hit.collider.gameObject.layer) & RaycastLayers) != 0)
							return false;

						// Update target position for the sphere check based on the raycast hit.
						targetPosition = hit.point;
					}
				}
			}

			if (IsSphereCheck)
			{
				bool hasOverlap = Physics.CheckSphere(targetPosition, SphereCheckRadius, SphereCheckLayers);

				if (SphereCheckMode == LayerCheckMode.Disallowed && hasOverlap)
					return false;

				if (SphereCheckMode == LayerCheckMode.Allowed && !hasOverlap)
					return false;
			}

			return true;
		}
	}

	public enum LayerCheckMode
	{
		Allowed,
		Disallowed
	}

	public enum SpawnMethod
	{
		Manual,
		Start,
		Update,
		Periodic
	}
}