// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Utilities
{
	/// <summary>
	/// Places a marker on the screen over a point of the world. A point on the screen gets the marker right on it,
	/// a point outside the screen gets it at the edge of the screen in its direction.
	/// </summary>
	public static class ScreenEdge
	{
		/// <param name="edgeMargin">Gap between the marker and the edge of the screen, in canvas units.</param>
		/// <param name="bottomMargin">Gap at the bottom edge, so texts under the marker stay on the screen.
		/// Negative — the same as <paramref name="edgeMargin" />.</param>
		public static void Place(Camera camera, RectTransform container, RectTransform marker, Vector3 position, float edgeMargin,
			float bottomMargin = -1f)
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
	}
}