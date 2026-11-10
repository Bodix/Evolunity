// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Utilities
{
	/// <summary>
	/// Places a marker on the screen right over a point of the world, e.g. a name over a unit.
	/// Unlike <see cref="ScreenEdge" />, a point outside the screen leaves the marker outside the screen too.
	/// </summary>
	public static class ScreenPoint
	{
		/// <param name="container">Parent of the marker that covers the screen.</param>
		/// <returns>False when the point is behind the camera: the marker stays where it was then.</returns>
		public static bool Place(Camera camera, RectTransform container, RectTransform marker, Vector3 position)
		{
			if (camera == null)
				return false;

			Vector3 viewport = camera.WorldToViewportPoint(position);

			if (viewport.z <= 0f)
				return false;

			Place(container, marker, viewport);
			return true;
		}

		/// <summary>
		/// Puts the marker at a point of the viewport of a camera: (0, 0) is the bottom left corner of the screen,
		/// (1, 1) is the top right one. For example, the marker can stand over the highest of several points.
		/// </summary>
		/// <param name="container">Parent of the marker that covers the screen.</param>
		public static void Place(RectTransform container, RectTransform marker, Vector2 viewport)
		{
			// The anchors stay in the corner, so every frame only the position of the marker changes.
			if (marker.anchorMin != Vector2.zero || marker.anchorMax != Vector2.zero)
				marker.anchorMin = marker.anchorMax = Vector2.zero;

			Rect rect = container.rect;
			marker.anchoredPosition = new Vector2(viewport.x * rect.width, viewport.y * rect.height);
		}
	}
}