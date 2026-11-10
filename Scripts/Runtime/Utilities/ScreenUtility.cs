// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Utilities
{
	public static class ScreenUtility
	{
		/// <summary>
		/// Places a marker on the screen right over a point of the world, e.g. a name over a unit.
		/// Unlike <see cref="PlaceMarkerClampedToEdges" />, a point outside the screen leaves the marker outside the screen too.
		/// </summary>
		/// <param name="container">Parent of the marker that covers the screen.</param>
		/// <returns>False when the point is behind the camera: the marker stays where it was then.</returns>
		public static bool PlaceMarker(Camera camera, RectTransform container, RectTransform marker, Vector3 position)
		{
			if (camera == null)
				return false;

			Vector3 viewport = camera.WorldToViewportPoint(position);

			if (viewport.z <= 0f)
				return false;

			PlaceMarkerAtViewport(container, marker, viewport);
			return true;
		}

		/// <summary>
		/// Puts the marker at a point of the viewport of a camera: (0, 0) is the bottom left corner of the screen,
		/// (1, 1) is the top right one. For example, the marker can stand over the highest of several points.
		/// </summary>
		/// <param name="container">Parent of the marker that covers the screen.</param>
		public static void PlaceMarkerAtViewport(RectTransform container, RectTransform marker, Vector2 viewport)
		{
			// The anchors stay in the corner, so every frame only the position of the marker changes.
			if (marker.anchorMin != Vector2.zero || marker.anchorMax != Vector2.zero)
				marker.anchorMin = marker.anchorMax = Vector2.zero;

			Rect rect = container.rect;
			marker.anchoredPosition = new Vector2(viewport.x * rect.width, viewport.y * rect.height);
		}

		/// <summary>
		/// Places a marker on the screen over a point of the world. A point on the screen gets the marker right on it,
		/// a point outside the screen gets it at the edge of the screen in its direction.
		/// </summary>
		/// <param name="edgeMargin">Gap between the marker and the edge of the screen, in canvas units.</param>
		/// <param name="bottomMargin">Gap at the bottom edge, so texts under the marker stay on the screen.
		/// Negative — the same as <paramref name="edgeMargin" />.</param>
		public static void PlaceMarkerClampedToEdges(Camera camera, RectTransform container, RectTransform marker, Vector3 position,
			float edgeMargin, float bottomMargin = -1f)
		{
			if (camera == null)
				return;

			Vector3 viewport = camera.WorldToViewportPoint(position);
			Vector2 fromCenter = new(viewport.x - 0.5f, viewport.y - 0.5f);

			// Behind the camera the projection flips: mirror it so the marker points the right way.
			if (viewport.z < 0f)
				fromCenter = -fromCenter;

			Rect rect = container.rect;
			float halfX = 0.5f - (rect.width > 0f ? edgeMargin / rect.width : 0f);
			float verticalMargin = fromCenter.y < 0f && bottomMargin >= 0f ? bottomMargin : edgeMargin;
			float halfY = 0.5f - (rect.height > 0f ? verticalMargin / rect.height : 0f);
			bool isOutside = viewport.z < 0f || Mathf.Abs(fromCenter.x) > halfX || Mathf.Abs(fromCenter.y) > halfY;

			// A point outside the screen: move the marker along its direction onto the edge.
			if (isOutside && fromCenter.sqrMagnitude > 1e-6f)
			{
				float scaleX = Mathf.Abs(fromCenter.x) > 1e-6f ? halfX / Mathf.Abs(fromCenter.x) : float.MaxValue;
				float scaleY = Mathf.Abs(fromCenter.y) > 1e-6f ? halfY / Mathf.Abs(fromCenter.y) : float.MaxValue;
				fromCenter *= Mathf.Min(scaleX, scaleY);
			}

			marker.anchorMin = marker.anchorMax = new Vector2(0.5f + fromCenter.x, 0.5f + fromCenter.y);
			marker.anchoredPosition = Vector2.zero;
		}

		// https://medium.com/@kunaltandon.kt/scaling-sprites-based-on-screen-resolutions-f28c47744ab5
		// TODO [#bug] Right only for an orthographic camera that looks along the Z axis: a perspective camera gives zero,
		// a rotated one gives a wrong size.
		public static Vector2 GetWorldSize(Camera camera = null)
		{
			camera = camera ? camera : Camera.main;

			Vector2 topRightCorner = new Vector2(1, 1);

			return (camera.ViewportToWorldPoint(topRightCorner) - camera.transform.position) * 2;
		}

		public static float GetWorldHeight(Camera camera = null)
		{
			return GetWorldSize(camera).y;
		}

		public static float GetWorldWidth(Camera camera = null)
		{
			return GetWorldSize(camera).x;
		}
	}
}
