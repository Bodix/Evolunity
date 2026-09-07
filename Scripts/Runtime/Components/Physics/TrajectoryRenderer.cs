// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using Bodix.Evolunity.Extensions;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	// TODO: Refactoring. [#refactoring]

	[RequireComponent(typeof(LineRenderer))]
	public class TrajectoryRenderer : MonoBehaviour
	{
		[SerializeField]
		private int _segmentCount = 30;
		[SerializeField]
		private float _timeStep = 0.1f;
		[SerializeField]
		private LayerMask _collisionMask;
		[SerializeField]
		private MeshRenderer _hitCrosshair;

		private LineRenderer _lineRenderer;
		private Vector3[] _points;
		private bool _hasColorOverride;
		private Color _overrideColor;

		private void Awake()
		{
			EnsureInitialized();
		}

		public void DrawTrajectory(Vector3 startPoint, Vector3 initialVelocity)
		{
			ApplyTrajectory(startPoint, initialVelocity);
			ResetColor();
		}

		public void DrawTrajectory(Vector3 startPoint, Vector3 initialVelocity, Color color)
		{
			ApplyTrajectory(startPoint, initialVelocity);
			SetColor(color);
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

		public void HideTrajectory()
		{
			gameObject.SetActive(false);
			ResetColor();
			SetHitCrosshairActive(false);
		}

		private void ApplyTrajectory(Vector3 startPoint, Vector3 initialVelocity)
		{
			EnsureInitialized();
			gameObject.SetActive(true);

			Vector3 gravity = Physics.gravity;
			int currentPositionCount = 1;
			bool hitDetected = false;

			_points[0] = startPoint;
			for (int i = 1; i < _points.Length; i++)
			{
				float time = i * _timeStep;
				Vector3 nextPoint = startPoint + initialVelocity * time + gravity * (0.5f * time * time);
				Vector3 previousPoint = _points[i - 1];

				if (Physics.Linecast(previousPoint, nextPoint, out RaycastHit hit, _collisionMask))
				{
					_points[i] = hit.point;
					currentPositionCount = i + 1;
					hitDetected = true;
					ShowHitCrosshair(hit);
					break;
				}

				_points[i] = nextPoint;
				currentPositionCount++;
			}

			if (!hitDetected)
				SetHitCrosshairActive(false);

			_lineRenderer.positionCount = currentPositionCount;
			_lineRenderer.SetPositions(_points);
		}

		private void ShowHitCrosshair(RaycastHit hit)
		{
			if (!_hitCrosshair)
				return;

			Transform root = GetHitCrosshairRoot();
			root.SetPositionAndRotation(hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
			root.gameObject.SetActive(true);
		}

		private void SetHitCrosshairActive(bool isActive)
		{
			if (_hitCrosshair)
				GetHitCrosshairRoot().gameObject.SetActive(isActive);
		}

		private Transform GetHitCrosshairRoot()
		{
			Transform parent = _hitCrosshair.transform.parent;
			return parent && parent != transform ? parent : _hitCrosshair.transform;
		}

		private void EnsureInitialized()
		{
			if (!_lineRenderer)
				_lineRenderer = GetComponent<LineRenderer>();

			if (_points == null || _points.Length != _segmentCount)
				_points = new Vector3[_segmentCount];
		}
	}
}
