// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Reflection;
using Bodix.Evolunity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	// TODO:
	// 1. Fix "RowSize + ColumnSize" case: uncontrollable cellSize.x in Inspector.
	// 2. Add sync with constraint count.
	// 3. Add "AspectRatio" mode.

	[AddComponentMenu("Evolunity/UI/Flexible Layout Group")]
	public class FlexibleLayoutGroup : GridLayoutGroup
	{
		public ColumnSizeMode ColumnSizeMode = ColumnSizeMode.Fixed;
		public int ColumnCount = 5;
		public RowSizeMode RowSizeMode = RowSizeMode.Fixed;
		public int RowCount = 5;

		private float cellSizeX;
		private float cellSizeY;

		// Field to access the private m_CellSize via reflection.
		private static readonly FieldInfo CellSizeField = typeof(GridLayoutGroup)
			.GetField("m_CellSize", BindingFlags.NonPublic | BindingFlags.Instance);

		private float ExpandedCellWidth
		{
			get
			{
				int cols = Mathf.Max(1, ColumnCount);
				float width = rectTransform.rect.size.x - padding.horizontal - spacing.x * (cols - 1);
				return Mathf.Max(0, width / cols);
			}
		}

		private float ExpandedCellHeight
		{
			get
			{
				int rows = Mathf.Max(1, RowCount);
				float height = rectTransform.rect.size.y - padding.vertical - spacing.y * (rows - 1);
				return Mathf.Max(0, height / rows);
			}
		}

		public override void CalculateLayoutInputHorizontal()
		{
			switch (ColumnSizeMode)
			{
				case ColumnSizeMode.Fixed:
					cellSizeX = cellSize.x;
					break;
				case ColumnSizeMode.RowSize:
					cellSizeX = cellSize.y;
					break;
				case ColumnSizeMode.Expand:
					cellSizeX = ExpandedCellWidth;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			// Apply new size before base method to ensure correct layout calculations.
			SetCellSizeQuietly(new Vector2(cellSizeX, cellSize.y));

			base.CalculateLayoutInputHorizontal();
		}

		public override void CalculateLayoutInputVertical()
		{
			switch (RowSizeMode)
			{
				case RowSizeMode.Fixed:
					cellSizeY = cellSize.y;
					break;
				case RowSizeMode.ColumnSize:
					cellSizeY = cellSize.x;
					break;
				case RowSizeMode.Expand:
					cellSizeY = ExpandedCellHeight;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			// Apply new size before base method to ensure correct layout calculations.
			SetCellSizeQuietly(new Vector2(cellSize.x, cellSizeY));

			base.CalculateLayoutInputVertical();
		}

		// Sets the private field to bypass the property setter and prevent prefab overrides.
		private void SetCellSizeQuietly(Vector2 newSize)
		{
			CellSizeField?.SetValue(this, newSize);
		}
	}

	public enum ColumnSizeMode
	{
		Fixed,
		RowSize,
		Expand
	}

	public enum RowSizeMode
	{
		Fixed,
		ColumnSize,
		Expand
	}
}