using System;
using System.Collections.Generic;
using System.Linq;
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

		private List<Collider> _ignoredColliders;
		private RaycastHit[] _hitsBuffer;
		private GameObject _childEffect;

		public event Action<RaycastHit> Hit;

		public Rigidbody Rigidbody { get; private set; }

		private void Awake()
		{
			Rigidbody = GetComponent<Rigidbody>();
			_hitsBuffer = new RaycastHit[HitsBufferSize];
			_ignoredColliders = new List<Collider>();

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

		private void OnHit(IEnumerable<RaycastHit> hits)
		{
			IEnumerable<RaycastHit> filteredHits = hits.Where(x => !_ignoredColliders.Contains(x.collider)).ToArray();

			if (filteredHits.IsNullOrEmpty())
				return;

			foreach (RaycastHit hit in filteredHits)
			{
				Vector3 position = hit.point + hit.normal * HitOffsetAlongNormal;
				GameObject hitEffect = Instantiate(hitEffectPrefab, position,
					Quaternion.FromToRotation(Vector3.up, hit.normal));
				Destroy(hitEffect, HitEffectLifetime);

				Hit?.Invoke(hit);
			}

			DetachAndDelayedDestroyTrails();
			Destroy(gameObject);
		}

		private void SpawnEffects()
		{
			_childEffect = Instantiate(childEffectPrefab, transform.position, transform.rotation, transform);

			if (startEffectPrefab)
			{
				GameObject startEffect = Instantiate(startEffectPrefab,
					transform.position + transform.TransformVector(StartEffectLocalOffset), transform.rotation);

				Destroy(startEffect, StartEffectLifetime);
			}
		}

		private void AlignRotationWithVelocity()
		{
			if (Rigidbody.velocity.magnitude != 0)
				transform.rotation = Quaternion.LookRotation(Rigidbody.velocity);
		}

		private void CheckHit()
		{
			Vector3 direction = Rigidbody.velocity.normalized;
			float velocityMagnitudeDelta = Rigidbody.velocity.magnitude * Time.fixedDeltaTime;

			int hitsCount;
			if ((hitsCount = Physics.SphereCastNonAlloc(transform.position, ColliderRadius, direction,
				    _hitsBuffer, velocityMagnitudeDelta, LayerMask)) > 0)
				OnHit(_hitsBuffer.Take(hitsCount));
		}

		// TODO: Improve reliability of this method. [#bug]
		// TODO: Consider change GetComponentsInChildren by something else. [#optimization]
		private void DetachAndDelayedDestroyTrails()
		{
			ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
			if (particles.Length > 1)
				// Skipping component at index 0 because it is on the parent.
				for (int i = 1; i < particles.Length; i++)
				{
					ParticleSystem possibleTrail = particles[i];

					// TODO: Fix string comparison. [#optimization]
					if (possibleTrail.gameObject.name.ToLower().Contains("trail"))
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