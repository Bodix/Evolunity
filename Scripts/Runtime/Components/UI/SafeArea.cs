// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Safe Area")]
	[RequireComponent(typeof(RectTransform))]
	public class SafeArea : MonoBehaviour
	{
		private RectTransform _rectTransform;
		private Rect _lastSafeArea = Rect.zero;

		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();

			ApplySafeArea();
		}

		private void Update()
		{
			if (_lastSafeArea != Screen.safeArea)
				ApplySafeArea();
		}

		private void ApplySafeArea()
		{
			Rect safeArea = Screen.safeArea;
			_lastSafeArea = safeArea;

			// Converts safe area pixels to screen ratio.
			Vector2 anchorMin = safeArea.position;
			Vector2 anchorMax = safeArea.position + safeArea.size;

			anchorMin.x /= Screen.width;
			anchorMin.y /= Screen.height;
			anchorMax.x /= Screen.width;
			anchorMax.y /= Screen.height;

			_rectTransform.anchorMin = anchorMin;
			_rectTransform.anchorMax = anchorMax;
		}
	}
}