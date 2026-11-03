// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using Bodix.Evolunity.Utilities;
using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	/// <summary>
	/// Markers on the screen over points of the world, one for each target, e.g. quest targets on a HUD.
	/// A target on the screen gets its marker right above it, a target outside the screen gets it at the edge
	/// of the screen in its direction. The markers are copies of the prefab inside this object, so it should cover the screen.
	/// </summary>
	/// <typeparam name="TTarget">What a marker points to.</typeparam>
	/// <typeparam name="TMarker">The component of the marker prefab that shows a target, or just its RectTransform.</typeparam>
	public abstract class ScreenMarkers<TTarget, TMarker> : MonoBehaviour where TMarker : Component
	{
		[Tooltip("Marker to copy for each target.")]
		[SerializeField]
		private TMarker _markerPrefab;
		[Tooltip("Optional. The main camera when empty.")]
		[SerializeField]
		private Camera _camera;
		[Tooltip("Gap between a marker and the edge of the screen, in canvas units.")]
		[SerializeField]
		private float _edgeMargin = 60f;
		[Tooltip("Gap at the bottom edge of the screen, in canvas units, e.g. so the texts under a marker stay on the screen. " +
			"Negative: the same as the edge margin.")]
		[SerializeField]
		private float _bottomMargin = -1f;
		[Tooltip("Height of the marked point above the target, in world units, so the marker does not cover the target.")]
		[SerializeField]
		private float _heightAboveTarget;

		private readonly List<TTarget> _targets = new();
		private readonly List<TMarker> _markers = new();

		/// <summary>
		/// Adds the targets to mark now to the empty list.
		/// </summary>
		protected abstract void CollectTargets(List<TTarget> targets);

		protected abstract Vector3 PositionOf(TTarget target);

		/// <summary>
		/// Shows the target on its marker, e.g. the distance to the target. Called every frame.
		/// </summary>
		protected virtual void Show(TMarker marker, TTarget target)
		{
		}

		private void LateUpdate()
		{
			_targets.Clear();
			CollectTargets(_targets);

			while (_markers.Count < _targets.Count)
				_markers.Add(Instantiate(_markerPrefab, transform));

			Camera view = _camera != null ? _camera : Camera.main;
			var container = (RectTransform)transform;

			for (int i = 0; i < _markers.Count; i++)
			{
				TMarker marker = _markers[i];
				bool isUsed = i < _targets.Count;

				if (marker.gameObject.activeSelf != isUsed)
					marker.gameObject.SetActive(isUsed);

				if (!isUsed)
					continue;

				TTarget target = _targets[i];
				Vector3 position = PositionOf(target) + Vector3.up * _heightAboveTarget;
				ScreenEdge.Place(view, container, (RectTransform)marker.transform, position, _edgeMargin, _bottomMargin);
				Show(marker, target);
			}
		}
	}
}
