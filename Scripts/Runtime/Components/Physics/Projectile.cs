using System;
using System.Collections.Generic;
using Bodix.Evolunity.Extensions;
using NaughtyAttributes;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	[AddComponentMenu("Evolunity/Physics/Projectile")]
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : MonoBehaviour
	{
		public bool PushOnStart = true;
		public float DefaultPushSpeed = 100f;
		public float ColliderRadius = 0.5f;
		// This is an offset that moves the hit effect slightly away from the point of hit to reduce clipping of the hit effect.
		public float HitOffsetAlongNormal = 0.15f;
		public Vector3 StartEffectLocalOffset = Vector3.zero;
		public LayerMask LayerMask = 1; // 1 = Default
		[Min(1)]
		public int HitsBufferSize = 10;

		[Header("Effects")]
		[Tooltip("Effect instantly spawned when this projectile is spawned (connected to projectile as a child).")]
		public GameObject childEffectPrefab;
		[Tooltip("Effect instantly spawned when this projectile is spawned (disconnected from projectile).")]
		public GameObject startEffectPrefab;
		[Tooltip("Effect spawned when this projectile hits a collider.")]
		public GameObject hitEffectPrefab;

		[Header("Lifetime")]
		public float ProjectileLifetime = 5f;
		// All the following lifetimes are taken by default from `ETFXProjectileScript` from the `Epic Toon FX` asset
		// to increase the chances that `Projectile` script will work well with missiles from this asset.
		// We keep the author's idea just in case.
		public float ChildEffectLifetime = 2f;
		public float StartEffectLifetime = 1.5f;
		public float HitEffectLifetime = 3.5f;

		private HashSet<Collider> _ignoredColliders;
		private RaycastHit[] _hitsBuffer;
		private GameObject _childEffect;

		public event Action<RaycastHit> Hit;

		public Rigidbody Rigidbody { get; private set; }

		private void Awake()
		{
			Rigidbody = GetComponent<Rigidbody>();
			_hitsBuffer = new RaycastHit[HitsBufferSize];
			_ignoredColliders = new HashSet<Collider>();

			Destroy(gameObject, ProjectileLifetime);
		}

		private void Start()
		{
			SpawnEffects();

			if (PushOnStart)
				PushForward();
		}

		private void FixedUpdate()
		{
			AlignRotationWithVelocity();
			CheckHit();
		}

		[Button]
		public void PushForward()
		{
			PushForward(DefaultPushSpeed);
		}

		public void Push(Vector3 direction, float speed)
		{
			Rigidbody.rotation = Quaternion.LookRotation(direction);
			Rigidbody.AddForce(direction * speed);
		}

		public void PushForward(float speed)
		{
			Push(transform.forward, speed);
		}

		public void ToggleChildEffect(bool isOn)
		{
			_childEffect.SetActive(isOn);
		}

		public void AddIgnoredCollider(Collider collider)
		{
			_ignoredColliders.Add(collider);
		}

		public void RemoveIgnoredCollider(Collider collider)
		{
			_ignoredColliders.Remove(collider);
		}

		private void OnHit(int hitsCount)
		{
			bool hasValidHit = false;

			for (int i = 0; i < hitsCount; i++)
			{
				RaycastHit hit = _hitsBuffer[i];

				if (_ignoredColliders.Contains(hit.collider))
					continue;

				hasValidHit = true;

				// https://discussions.unity.com/t/spherecastall-returns-0-0-0-for-all-raycasthit-points/638063
				// https://stackoverflow.com/questions/55014423/raycasthit-point-always-returns-0-0-0
				//
				// From docs:
				// Notes: For colliders that overlap the sphere at the start of the sweep,
				// RaycastHit.normal is set opposite to the direction of the sweep,
				// RaycastHit.distance is set to zero, and the zero vector gets returned in RaycastHit.point.
				// You might want to check whether this is the case in your particular query
				// and perform additional queries to refine the result. Passing a zero radius results
				// in undefined output and doesn't always behave the same as Physics.Raycast.
				// https://docs.unity3d.com/ScriptReference/Physics.SphereCastAll.html
				if (hit.point == Vector3.zero && hit.distance == 0f)
					hit.point = hit.collider.ClosestPoint(transform.position);

				Vector3 position = hit.point + hit.normal * HitOffsetAlongNormal;
				GameObject hitEffect = Instantiate(hitEffectPrefab, position,
					Quaternion.FromToRotation(Vector3.up, hit.normal));
				Destroy(hitEffect, HitEffectLifetime);

				Hit?.Invoke(hit);
			}

			if (!hasValidHit)
				return;

			DetachAndDelayedDestroyTrails();
			Destroy(gameObject);
		}

		private void SpawnEffects()
		{
			_childEffect = Instantiate(childEffectPrefab, transform.position, transform.rotation, transform);

			if (startEffectPrefab)
			{
				GameObject startEffect = Instantiate(startEffectPrefab,
					transform.TransformPoint(StartEffectLocalOffset), transform.rotation);

				Destroy(startEffect, StartEffectLifetime);
			}
		}

		private void AlignRotationWithVelocity()
		{
			if (Rigidbody.velocity.sqrMagnitude > 0f)
				transform.rotation = Quaternion.LookRotation(Rigidbody.velocity);
		}

		private void CheckHit()
		{
			Vector3 velocity = Rigidbody.velocity;
			float speed = velocity.magnitude;

			if (speed == 0f)
				return;

			Vector3 direction = velocity / speed;
			float distance = speed * Time.fixedDeltaTime;

			int hitsCount = Physics.SphereCastNonAlloc(transform.position, ColliderRadius, direction,
				_hitsBuffer, distance, LayerMask);

			if (hitsCount > 0)
				OnHit(hitsCount);
		}

		// TODO: Improve reliability of this method. [#bug]
		// TODO: Consider change GetComponentsInChildren by something else. [#optimization]
		private void DetachAndDelayedDestroyTrails()
		{
			ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();

			foreach (ParticleSystem possibleTrail in particles)
			{
				// Skip the component if it is on the parent.
				if (possibleTrail.gameObject == gameObject)
					continue;

				// Optimized string comparison without memory allocations.
				if (possibleTrail.gameObject.name.IndexOf("trail", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					possibleTrail.transform.SetParent(null);

					Destroy(possibleTrail.gameObject, ChildEffectLifetime);
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawSphere(transform.position, ColliderRadius);
		}
	}
}