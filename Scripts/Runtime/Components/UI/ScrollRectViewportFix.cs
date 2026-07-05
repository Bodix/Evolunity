// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	[RequireComponent(typeof(ScrollRect))]
	public class ScrollRectViewportFix : MonoBehaviour
	{
		private ScrollRect _scrollRect;

		private void Awake()
		{
			_scrollRect = GetComponent<ScrollRect>();
		}

		private void Start()
		{
			RestoreViewportLayout();
		}

		private void RestoreViewportLayout()
		{
			if (_scrollRect.viewport == null)
				return;

			RectTransform viewport = _scrollRect.viewport;

			viewport.anchorMin = Vector2.zero;
			viewport.anchorMax = Vector2.one;

			viewport.offsetMin = Vector2.zero;
			viewport.offsetMax = Vector2.zero;
		}
	}
}