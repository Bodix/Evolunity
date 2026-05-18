using Bodix.Evolunity.Structs;
using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	public class RectTransformInterpolator : ComponentRatioInterpolator<RectTransform, RectTransformData>
	{
		protected override RectTransformData InterpolateData(RectTransformData widest, RectTransformData narrowest, float t)
		{
			return new RectTransformData
			{
				AnchoredPosition = Vector3.Lerp(widest.AnchoredPosition, narrowest.AnchoredPosition, t),
				SizeDelta = Vector2.Lerp(widest.SizeDelta, narrowest.SizeDelta, t),
				AnchorMin = Vector2.Lerp(widest.AnchorMin, narrowest.AnchorMin, t),
				AnchorMax = Vector2.Lerp(widest.AnchorMax, narrowest.AnchorMax, t),
				Pivot = Vector2.Lerp(widest.Pivot, narrowest.Pivot, t)
			};
		}

		protected override RectTransformData ExtractDataFromTarget()
		{
			return Target.GetData();
		}

		protected override void ApplyDataToTarget(RectTransformData data)
		{
			Target.SetData(data);
		}
	}
}