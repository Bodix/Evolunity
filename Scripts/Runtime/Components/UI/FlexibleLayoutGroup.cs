// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Flexible Layout Group")]
	public class FlexibleLayoutGroup : GridLayoutGroup
	{
		public ColumnSizeMode ColumnSizeMode = ColumnSizeMode.Fixed;
		public int ColumnCount = 5;
		public RowSizeMode RowSizeMode = RowSizeMode.Fixed;
		public int RowCount = 5;

		private float _cellSizeX;
		private float _cellSizeY;

		// Field to access the private m_CellSize via reflection.
		private static readonly FieldInfo CellSizeField = typeof(GridLayoutGroup)
			.GetField("m_CellSize", BindingFlags.NonPublic | BindingFlags.Instance);

		private float ExpandedCellWidth
		{
			get
			{
				int cols = Mathf.Max(1, ColumnCount);
				float width = rectTransform.rect.size.x - padding.horizontal - spacing.x * (cols - 1);
				return Mathf.Floor(Mathf.Max(0, width / cols));
			}
		}

		private float ExpandedCellHeight
		{
			get
			{
				int rows = Mathf.Max(1, RowCount);
				float height = rectTransform.rect.size.y - padding.vertical - spacing.y * (rows - 1);
				return Mathf.Floor(Mathf.Max(0, height / rows));
			}
		}

		public override void CalculateLayoutInputHorizontal()
		{
			SyncConstraints();

			bool isCircularDependency = ColumnSizeMode == ColumnSizeMode.RowSize && RowSizeMode == RowSizeMode.ColumnSize;

			switch (ColumnSizeMode)
			{
				case ColumnSizeMode.Fixed:
					_cellSizeX = cellSize.x;
					break;
				case ColumnSizeMode.RowSize:
					// Prevent circular dependency by falling back to fixed width.
					_cellSizeX = isCircularDependency ? cellSize.x : cellSize.y;
					break;
				case ColumnSizeMode.Expand:
					_cellSizeX = ExpandedCellWidth;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			// Apply new size before base method to ensure correct layout calculations.
			SetCellSizeQuietly(new Vector2(_cellSizeX, cellSize.y));

			base.CalculateLayoutInputHorizontal();
		}

		public override void CalculateLayoutInputVertical()
		{
			bool isCircularDependency = ColumnSizeMode == ColumnSizeMode.RowSize && RowSizeMode == RowSizeMode.ColumnSize;

			switch (RowSizeMode)
			{
				case RowSizeMode.Fixed:
					_cellSizeY = cellSize.y;
					break;
				case RowSizeMode.ColumnSize:
					// Prevent circular dependency by falling back to fixed height.
					_cellSizeY = isCircularDependency ? cellSize.y : cellSize.x;
					break;
				case RowSizeMode.Expand:
					_cellSizeY = ExpandedCellHeight;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			// Apply new size before base method to ensure correct layout calculations.
			SetCellSizeQuietly(new Vector2(cellSize.x, _cellSizeY));

			base.CalculateLayoutInputVertical();
		}

		// Sets the private field to bypass the property setter and prevent prefab overrides.
		private void SetCellSizeQuietly(Vector2 newSize)
		{
			CellSizeField?.SetValue(this, newSize);
		}

		// Synchronizes internal counts with the base GridLayoutGroup constraints.
		private void SyncConstraints()
		{
			if (ColumnSizeMode != ColumnSizeMode.Fixed)
			{
				constraint = Constraint.FixedColumnCount;
				constraintCount = Mathf.Max(1, ColumnCount);
			}
			else if (RowSizeMode != RowSizeMode.Fixed)
			{
				constraint = Constraint.FixedRowCount;
				constraintCount = Mathf.Max(1, RowCount);
			}
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