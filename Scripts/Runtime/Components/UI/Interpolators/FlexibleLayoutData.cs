using System;

namespace Bodix.Evolunity.Components.UI
{
	[Serializable]
	public class FlexibleLayoutData
	{
		public int ColumnCount = 3;
		public int Padding = 40;
		public float Spacing = 20f;

		public FlexibleLayoutData()
		{
		}

		public FlexibleLayoutData(int columnCount, int padding, float spacing)
		{
			ColumnCount = columnCount;
			Padding = padding;
			Spacing = spacing;
		}
	}
}