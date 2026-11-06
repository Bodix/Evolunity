// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using Bodix.Evolunity.Extensions;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	[RequireComponent(typeof(LineRenderer))]
	public class TrajectoryRenderer : MonoBehaviour
	{
		[SerializeField]
		private int _segmentCount = 30;
		[SerializeField]
		private float _timeStep = 0.1f;
		[SerializeField, Min(1), Tooltip("How many straight pieces of the path are checked against colliders. " +
			"Fewer pieces are cheaper but cut the arc more.")]
		private int _castCount = 29;
		[SerializeField]
		private LayerMask _collisionMask;
		[SerializeField]
		private MeshRenderer _hitCrosshair;

		private LineRenderer _lineRenderer;
		private Vector3[] _points;
		private Vector3 _hitCrosshairLocalPosition;
		private Quaternion _hitCrosshairLocalRotation;
		private bool _hitCrosshairPoseCached;
		private bool _hasColorOverride;
		private Color _overrideColor;

		/// <summary>
		/// A flat floor the path stops at, like a sea surface. The path finds it by a formula, not by casts, so the floor
		/// needs no collider in the collision mask. Negative infinity means no floor.
		/// </summary>
		public float FloorHeight { get; set; } = float.NegativeInfinity;

		/// <summary>
		/// Where the drawn path ends: at the first collider it meets, at the floor or at its last point.
		/// </summary>
		public Vector3 EndPoint => _points[_points.Length - 1];

		private void Awake()
		{
			EnsureInitialized();
		}

		/// <summary>
		/// Draws the path. Its color stays as it is: <see cref="SetColor"/> and <see cref="ResetColor"/> change it.
		/// </summary>
		public void DrawTrajectory(Vector3 startPoint, Vector3 initialVelocity)
		{
			EnsureInitialized();
			gameObject.SetActive(true);

			Vector3 gravity = Physics.gravity;
			bool reachesFloor = TryGetFloorTime(startPoint, initialVelocity, gravity, out float endTime);

			if (!reachesFloor)
				endTime = (_points.Length - 1) * _timeStep;

			bool hitDetected = TryCast(startPoint, initialVelocity, gravity, ref endTime, out RaycastHit hit);
			int lastIndex = _points.Length - 1;

			for (int i = 0; i <= lastIndex; i++)
				_points[i] = PointAt(startPoint, initialVelocity, gravity, endTime * i / lastIndex);

			if (hitDetected)
			{
				_points[lastIndex] = hit.point;
				ShowHitCrosshair(hit.point, hit.normal);
			}
			else if (reachesFloor)
			{
				ShowHitCrosshair(_points[lastIndex], Vector3.up);
			}
			else
			{
				HideHitCrosshair();
			}

			_lineRenderer.positionCount = _points.Length;
			_lineRenderer.SetPositions(_points);
		}

		public void HideTrajectory()
		{
			gameObject.SetActive(false);
			ResetColor();
			HideHitCrosshair();
		}

		public void SetColor(Color color)
		{
			EnsureInitialized();

			if (_hasColorOverride && _overrideColor == color)
				return;

			_lineRenderer.SetMainColor(color);

			if (_hitCrosshair)
				_hitCrosshair.SetMainColor(color.WithAlpha(_hitCrosshair.sharedMaterial.GetMainColor().a));

			_hasColorOverride = true;
			_overrideColor = color;
		}

		public void ResetColor()
		{
			if (!_hasColorOverride)
				return;

			EnsureInitialized();

			_lineRenderer.SetMainColor(_lineRenderer.sharedMaterial.GetMainColor());

			if (_hitCrosshair)
				_hitCrosshair.SetMainColor(_hitCrosshair.sharedMaterial.GetMainColor());

			_hasColorOverride = false;
		}

		private bool TryGetFloorTime(Vector3 startPoint, Vector3 initialVelocity, Vector3 gravity, out float time)
		{
			float height = startPoint.y - FloorHeight;
			time = 0f;

			if (float.IsInfinity(height) || height < 0f || gravity.y >= 0f)
				return false;

			// startPoint.y + initialVelocity.y * time + gravity.y * time² / 2 = FloorHeight
			float vertical = initialVelocity.y;
			time = (-vertical - Mathf.Sqrt(vertical * vertical - 2f * gravity.y * height)) / gravity.y;
			return true;
		}

		/// <summary>
		/// Checks the path against colliders piece by piece. A hit cuts the path short.
		/// </summary>
		private bool TryCast(Vector3 startPoint, Vector3 initialVelocity, Vector3 gravity, ref float endTime,
			out RaycastHit hit)
		{
			float pieceTime = endTime / _castCount;
			Vector3 previousPoint = startPoint;

			for (int i = 1; i <= _castCount; i++)
			{
				Vector3 nextPoint = PointAt(startPoint, initialVelocity, gravity, i * pieceTime);

				if (Physics.Linecast(previousPoint, nextPoint, out hit, _collisionMask))
				{
					float pieceLength = Vector3.Distance(previousPoint, nextPoint);
					endTime = (i - 1 + hit.distance / pieceLength) * pieceTime;
					return true;
				}

				previousPoint = nextPoint;
			}

			hit = default;
			return false;
		}

		private static Vector3 PointAt(Vector3 startPoint, Vector3 initialVelocity, Vector3 gravity, float time)
		{
			return startPoint + initialVelocity * time + gravity * (0.5f * time * time);
		}

		private void ShowHitCrosshair(Vector3 point, Vector3 normal)
		{
			if (!_hitCrosshair)
				return;

			Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, normal);
			_hitCrosshair.transform.SetPositionAndRotation(
				point + surfaceRotation * _hitCrosshairLocalPosition,
				surfaceRotation * _hitCrosshairLocalRotation);
			_hitCrosshair.enabled = true;
		}

		private void HideHitCrosshair()
		{
			if (_hitCrosshair)
				_hitCrosshair.enabled = false;
		}

		private void EnsureInitialized()
		{
			if (!_lineRenderer)
				_lineRenderer = GetComponent<LineRenderer>();

			if (_points == null || _points.Length != _segmentCount)
				_points = new Vector3[_segmentCount];

			if (_hitCrosshair && !_hitCrosshairPoseCached)
			{
				_hitCrosshairLocalPosition = _hitCrosshair.transform.localPosition;
				_hitCrosshairLocalRotation = _hitCrosshair.transform.localRotation;
				_hitCrosshairPoseCached = true;
			}
		}
	}
}