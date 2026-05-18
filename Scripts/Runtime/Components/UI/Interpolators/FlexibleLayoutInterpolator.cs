using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	public class FlexibleLayoutAdapter : ComponentRatioInterpolator<FlexibleLayoutGroup, FlexibleLayoutData>
	{
		protected override FlexibleLayoutData InterpolateData(FlexibleLayoutData widest, FlexibleLayoutData narrowest, float t)
		{
			int padding = Mathf.RoundToInt(Mathf.Lerp(widest.Padding, narrowest.Padding, t));
			int columnCount = Mathf.RoundToInt(Mathf.Lerp(widest.ColumnCount, narrowest.ColumnCount, t));
			float spacing = Mathf.Lerp(widest.Spacing, narrowest.Spacing, t);

			return new FlexibleLayoutData(columnCount, padding, spacing);
		}

		protected override FlexibleLayoutData ExtractDataFromTarget()
		{
			return new FlexibleLayoutData(
				Target.ColumnCount,
				Target.padding.left,
				Target.spacing.x
			);
		}

		protected override void ApplyDataToTarget(FlexibleLayoutData data)
		{
			Target.ColumnCount = data.ColumnCount;
			Target.padding = new RectOffset(data.Padding, data.Padding, data.Padding, data.Padding);
			Target.spacing = new Vector2(data.Spacing, data.Spacing);
		}
	}
}