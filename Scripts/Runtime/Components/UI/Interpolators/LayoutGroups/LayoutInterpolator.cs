using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	public class LayoutInterpolator : ComponentRatioInterpolator<HorizontalOrVerticalLayoutGroup, LayoutData>
	{
		protected override LayoutData InterpolateData(LayoutData widest, LayoutData narrowest, float t)
		{
			int left = Mathf.RoundToInt(Mathf.Lerp(widest.PaddingLeft, narrowest.PaddingLeft, t));
			int right = Mathf.RoundToInt(Mathf.Lerp(widest.PaddingRight, narrowest.PaddingRight, t));
			int top = Mathf.RoundToInt(Mathf.Lerp(widest.PaddingTop, narrowest.PaddingTop, t));
			int bottom = Mathf.RoundToInt(Mathf.Lerp(widest.PaddingBottom, narrowest.PaddingBottom, t));
			float spacing = Mathf.Lerp(widest.Spacing, narrowest.Spacing, t);

			return new LayoutData(left, right, top, bottom, spacing);
		}

		protected override LayoutData ExtractDataFromTarget()
		{
			return new LayoutData(
				Target.padding.left,
				Target.padding.right,
				Target.padding.top,
				Target.padding.bottom,
				Target.spacing
			);
		}

		protected override void ApplyDataToTarget(LayoutData data)
		{
			Target.padding = new RectOffset(data.PaddingLeft, data.PaddingRight, data.PaddingTop, data.PaddingBottom);
			Target.spacing = data.Spacing;
		}
	}
}