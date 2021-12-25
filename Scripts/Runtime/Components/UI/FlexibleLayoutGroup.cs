// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using Evolutex.Evolunity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Components.UI
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

        private float ExpandedCellWidth => (rectTransform.rect.size.x - padding.horizontal - spacing.x * (ColumnCount - 1)) / ColumnCount;
        private float ExpandedCellHeight => (rectTransform.rect.size.y - padding.vertical - spacing.y * (RowCount - 1)) / RowCount;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

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
        }

        public override void SetLayoutHorizontal()
        {
            base.SetLayoutHorizontal();
            
            cellSize = cellSize.WithX(cellSizeX);
        }

        public override void CalculateLayoutInputVertical()
        {
            base.CalculateLayoutInputVertical();

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
        }

        public override void SetLayoutVertical()
        {
            base.SetLayoutVertical();

            cellSize = cellSize.WithY(cellSizeY);
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