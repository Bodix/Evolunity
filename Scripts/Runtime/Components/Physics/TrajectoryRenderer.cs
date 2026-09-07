// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

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
		private GameObject _hitCrosshair;

		private LineRenderer _lineRenderer;
		private Vector3[] _points;
		private GradientColorKey[] _originalColorKeys;
		private GradientAlphaKey[] _originalAlphaKeys;
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

			GradientColorKey[] colorKeys = new GradientColorKey[_originalColorKeys.Length];
			for (int i = 0; i < _originalColorKeys.Length; i++)
			{
				GradientColorKey original = _originalColorKeys[i];
				colorKeys[i] = new GradientColorKey(color, original.time);
			}

			Gradient gradient = new Gradient();
			gradient.SetKeys(colorKeys, _originalAlphaKeys);
			_lineRenderer.colorGradient = gradient;
			_hasColorOverride = true;
			_overrideColor = color;
		}

		public void ResetColor()
		{
			if (!_hasColorOverride)
				return;

			EnsureInitialized();

			Gradient gradient = new Gradient();
			gradient.SetKeys(_originalColorKeys, _originalAlphaKeys);
			_lineRenderer.colorGradient = gradient;
			_hasColorOverride = false;
		}

		public void HideTrajectory()
		{
			gameObject.SetActive(false);
			ResetColor();

			if (_hitCrosshair)
				_hitCrosshair.SetActive(false);
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

					if (_hitCrosshair)
					{
						_hitCrosshair.transform.position = hit.point;
						_hitCrosshair.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
						_hitCrosshair.SetActive(true);
					}

					break;
				}

				_points[i] = nextPoint;
				currentPositionCount++;
			}

			if (_hitCrosshair && !hitDetected)
				_hitCrosshair.SetActive(false);

			_lineRenderer.positionCount = currentPositionCount;
			_lineRenderer.SetPositions(_points);
		}

		private void EnsureInitialized()
		{
			if (!_lineRenderer)
				_lineRenderer = GetComponent<LineRenderer>();

			if (_points == null || _points.Length != _segmentCount)
				_points = new Vector3[_segmentCount];

			if (_originalColorKeys == null)
			{
				Gradient gradient = _lineRenderer.colorGradient;
				_originalColorKeys = gradient.colorKeys;
				_originalAlphaKeys = gradient.alphaKeys;
			}
		}
	}
}
