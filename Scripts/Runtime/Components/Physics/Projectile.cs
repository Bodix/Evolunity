using System;
using NaughtyAttributes;
using UnityEngine;

namespace Evolutex.Evolunity.Components.Physics
{
	[AddComponentMenu("Evolunity/Physics/Projectile")]
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : MonoBehaviour
	{
		public bool PushOnStart = true;
		public float DefaultPushSpeed = 100f;
		public float ColliderRadius = 0.5f;
		// This is an offset that moves the hit particles slightly away from the point of hit to reduce clipping of the hit effect.
		public float HitOffsetAlongNormal = 0.15f;
		public Vector3 StartParticlesLocalOffset = Vector3.zero;
		public LayerMask LayerMask = 1; // 1 = Default

		[Header("Particles")]
		[Tooltip("Effect instantly spawned when this projectile is spawned (connected to projectile as a child).")]
		public GameObject trailParticlesPrefab;
		[Tooltip("Effect instantly spawned when this projectile is spawned (disconnected from projectile).")]
		public GameObject startParticlesPrefab;
		[Tooltip("Effect spawned when this projectile hits a collider.")]
		public GameObject hitParticlesPrefab;

		[Header("Lifetime")]
		public float ProjectileLifetime = 5f;
		// All the following lifetimes are taken by default from `ETFXProjectileScript` from the `Epic Toon FX` asset
		// to increase the chances that `Projectile` script will work well with missiles from this asset.
		// We keep the author's idea just in case.
		public float TrailParticlesLifetime = 2f;
		public float StartParticlesLifetime = 1.5f;
		public float HitParticlesLifetime = 3.5f;

		private GameObject _trailParticles;
		private GameObject _startParticles;

		public event Action<RaycastHit> Hit;

		public Rigidbody Rigidbody { get; private set; }

		private void Awake()
		{
			Rigidbody = GetComponent<Rigidbody>();

			Destroy(gameObject, ProjectileLifetime);
		}

		private void Start()
		{
			CreateParticles();

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
			Push(Vector3.forward, speed);
		}

		private void OnHit(RaycastHit hit)
		{
			Vector3 position = hit.point + hit.normal * HitOffsetAlongNormal;
			GameObject hitParticles = Instantiate(hitParticlesPrefab, position,
				Quaternion.FromToRotation(Vector3.up, hit.normal));

			DetachAndDelayedDestroyTrails();
			Destroy(hitParticles, HitParticlesLifetime);
			Destroy(gameObject);

			Hit?.Invoke(hit);
		}

		private void CreateParticles()
		{
			_trailParticles = Instantiate(trailParticlesPrefab, transform.position, transform.rotation, transform);

			if (startParticlesPrefab)
			{
				_startParticles = Instantiate(startParticlesPrefab,
					transform.position + transform.TransformVector(StartParticlesLocalOffset), transform.rotation);

				Destroy(_startParticles, StartParticlesLifetime);
			}
		}

		private void AlignRotationWithVelocity()
		{
			if (Rigidbody.velocity.magnitude != 0)
				transform.rotation = Quaternion.LookRotation(Rigidbody.velocity);
		}

		private void CheckHit()
		{
			Vector3 direction = Rigidbody.velocity;
			if (Rigidbody.useGravity)
				direction += UnityEngine.Physics.gravity * Time.deltaTime;
			direction = direction.normalized;

			float velocityMagnitudeDelta = Rigidbody.velocity.magnitude * Time.deltaTime;

			if (UnityEngine.Physics.SphereCast(transform.position, ColliderRadius, direction,
				    out RaycastHit hit, velocityMagnitudeDelta, LayerMask))
				OnHit(hit);
		}

		// TODO: Fix string comparison. [#optimization]
		private void DetachAndDelayedDestroyTrails()
		{
			ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
			// Skipping component at index 0 because it is on the parent.
			for (int i = 1; i < particles.Length; i++)
			{
				ParticleSystem possibleTrail = particles[i];

				if (possibleTrail.gameObject.name.ToLower().Contains("trail"))
				{
					possibleTrail.transform.SetParent(null);

					Destroy(possibleTrail.gameObject, TrailParticlesLifetime);
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawSphere(transform.position, ColliderRadius);
		}
	}
}