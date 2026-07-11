using System;

namespace Bodix.Evolunity.Components.UI
{
	[Serializable]
	public class LayoutData
	{
		public int PaddingLeft;
		public int PaddingRight;
		public int PaddingTop;
		public int PaddingBottom;
		public float Spacing;

		public LayoutData()
		{
		}

		public LayoutData(int left, int right, int top, int bottom, float spacing)
		{
			PaddingLeft = left;
			PaddingRight = right;
			PaddingTop = top;
			PaddingBottom = bottom;
			Spacing = spacing;
		}
	}
}