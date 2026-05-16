// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Bodix.Evolunity.Structs
{
	[Serializable]
	public struct RectTransformData
	{
		public Vector3 AnchoredPosition;
		public Vector2 SizeDelta;
		public Vector2 AnchorMin;
		public Vector2 AnchorMax;
		public Vector2 Pivot;
	}

	public static class RectTransformExtensions
	{
		public static RectTransformData GetData(this RectTransform rectTransform)
		{
			return new RectTransformData
			{
				AnchoredPosition = rectTransform.anchoredPosition3D,
				SizeDelta = rectTransform.sizeDelta,
				AnchorMin = rectTransform.anchorMin,
				AnchorMax = rectTransform.anchorMax,
				Pivot = rectTransform.pivot
			};
		}

		public static void SetData(this RectTransform rectTransform, RectTransformData data)
		{
			rectTransform.anchoredPosition3D = data.AnchoredPosition;
			rectTransform.sizeDelta = data.SizeDelta;
			rectTransform.anchorMin = data.AnchorMin;
			rectTransform.anchorMax = data.AnchorMax;
			rectTransform.pivot = data.Pivot;
		}
	}
}