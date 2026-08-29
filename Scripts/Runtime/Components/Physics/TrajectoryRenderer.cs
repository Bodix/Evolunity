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
		private GameObject _hitIndicator;

		private LineRenderer _lineRenderer;
		private Vector3[] _points;

		private void Awake()
		{
			if (!_lineRenderer)
				_lineRenderer = GetComponent<LineRenderer>();

			_points = new Vector3[_segmentCount];
		}

		public void DrawTrajectory(Vector3 startPoint, Vector3 initialVelocity)
		{
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

					if (_hitIndicator)
					{
						_hitIndicator.transform.position = hit.point;
						_hitIndicator.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
						_hitIndicator.SetActive(true);
					}

					break;
				}

				_points[i] = nextPoint;
				currentPositionCount++;
			}

			if (_hitIndicator && !hitDetected)
				_hitIndicator.SetActive(false);

			_lineRenderer.positionCount = currentPositionCount;
			_lineRenderer.SetPositions(_points);
		}

		public void HideTrajectory()
		{
			gameObject.SetActive(false);

			if (_hitIndicator)
				_hitIndicator.SetActive(false);
		}
	}
}