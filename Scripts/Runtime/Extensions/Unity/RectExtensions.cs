// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Linq;
using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    // TODO:
    // 1. Add cutting.
    // 2. Add stretching.
    // 3. Replace Split methods with https://github.com/slavniyteo/rect-ex.
    // 4. Improve Clip method with clamping.
    // 5. Refactor SplitAndCombine methods with custom struct to improve readability.
    // 6. Remove redundant creating new Rects (?).

    public static class RectExtensions
    {
        #region Position

        public static Rect WithX(this Rect rect, float x)
        {
            return new Rect(x, rect.y, rect.width, rect.height);
        }

        public static Rect WithY(this Rect rect, float y)
        {
            return new Rect(rect.x, y, rect.width, rect.height);
        }

        public static Rect WithPosition(this Rect rect, float x, float y)
        {
            return new Rect(x, y, rect.width, rect.height);
        }

        public static Rect WithPosition(this Rect rect, Vector2 position)
        {
            return new Rect(position.x, position.y, rect.width, rect.height);
        }

        public static Rect TranslateX(this Rect rect, float x)
        {
            return new Rect(rect.x + x, rect.y, rect.width, rect.height);
        }

        public static Rect TranslateY(this Rect rect, float y)
        {
            return new Rect(rect.x, rect.y + y, rect.width, rect.height);
        }

        public static Rect Translate(this Rect rect, float x, float y)
        {
            return new Rect(rect.x + x, rect.y + y, rect.width, rect.height);
        }

        public static Rect Translate(this Rect rect, Vector2 translation)
        {
            return new Rect(rect.x + translation.x, rect.y + translation.y, rect.width, rect.height);
        }

        public static Rect Below(this Rect rect, Rect target)
        {
            return new Rect(rect.x, target.y + target.height, rect.width, rect.height);
        }

        public static Rect Above(this Rect rect, Rect target)
        {
            return new Rect(rect.x, target.y - rect.height, rect.width, rect.height);
        }

        public static Rect RightOf(this Rect rect, Rect target)
        {
            return new Rect(target.x + target.width, rect.y, rect.width, rect.height);
        }

        public static Rect LeftOf(this Rect rect, Rect target)
        {
            return new Rect(target.x - rect.width, rect.y, rect.width, rect.height);
        }

        public static Rect AlignToTopOf(this Rect rect, Rect target)
        {
            return new Rect(rect.x, target.y, rect.width, rect.height);
        }

        public static Rect AlignToBottomOf(this Rect rect, Rect target)
        {
            return new Rect(rect.x, target.yMax - rect.height, rect.width, rect.height);
        }

        public static Rect AlignToLeftOf(this Rect rect, Rect target)
        {
            return new Rect(target.x, rect.y, rect.width, rect.height);
        }

        public static Rect AlignToRightOf(this Rect rect, Rect target)
        {
            return new Rect(target.xMax - rect.width, rect.y, rect.width, rect.height);
        }

        public static Rect AlignToTopLeftOf(this Rect rect, Rect target)
        {
            return new Rect(target.x, target.y, rect.width, rect.height);
        }

        public static Rect AlignToTopRightOf(this Rect rect, Rect target)
        {
            return new Rect(target.xMax - rect.width, target.y, rect.width, rect.height);
        }

        public static Rect AlignToBottomLeftOf(this Rect rect, Rect target)
        {
            return new Rect(target.x, target.yMax - rect.height, rect.width, rect.height);
        }

        public static Rect AlignToBottomRightOf(this Rect rect, Rect target)
        {
            return new Rect(target.xMax - rect.width, target.yMax - rect.height, rect.width, rect.height);
        }

        public static Rect AlignVerticallyCenteredTo(this Rect rect, Rect target)
        {
            float y = target.y + (target.height - rect.height) / 2;

            return new Rect(rect.x, y, rect.width, rect.height);
        }

        public static Rect AlignHorizontallyCenteredTo(this Rect rect, Rect target)
        {
            float x = target.x + (target.width - rect.width) / 2;

            return new Rect(x, rect.y, rect.width, rect.height);
        }

        public static Rect CenterTo(this Rect rect, Rect target)
        {
            return rect
                .AlignVerticallyCenteredTo(target)
                .AlignHorizontallyCenteredTo(target);
        }

        #endregion

        #region Size

        public static Rect WithWidth(this Rect rect, float width)
        {
            return new Rect(rect.x, rect.y, width, rect.height);
        }

        public static Rect WithHeight(this Rect rect, float height)
        {
            return new Rect(rect.x, rect.y, rect.width, height);
        }

        public static Rect WithSize(this Rect rect, float width, float height)
        {
            return new Rect(rect.x, rect.y, width, height);
        }

        public static Rect WithSize(this Rect rect, Vector2 size)
        {
            return new Rect(rect.x, rect.y, size.x, size.y);
        }

        public static Rect AddWidth(this Rect rect, float width)
        {
            return new Rect(rect.x, rect.y, rect.width + width, rect.height);
        }

        public static Rect AddHeight(this Rect rect, float height)
        {
            return new Rect(rect.x, rect.y, rect.width, rect.height + height);
        }

        public static Rect AddSize(this Rect rect, float width, float height)
        {
            return new Rect(rect.x, rect.y, rect.width + width, rect.height + height);
        }

        public static Rect AddSize(this Rect rect, Vector2 size)
        {
            return new Rect(rect.x, rect.y, rect.width + size.x, rect.height + size.y);
        }

        public static Rect TopHalf(this Rect rect)
        {
            return new Rect(rect.x, rect.y, rect.width, rect.height / 2);
        }

        public static Rect BottomHalf(this Rect rect)
        {
            return new Rect(rect.x, rect.y + rect.height / 2, rect.width, rect.height / 2);
        }

        public static Rect LeftHalf(this Rect rect)
        {
            return new Rect(rect.x, rect.y, rect.width / 2, rect.height);
        }

        public static Rect RightHalf(this Rect rect)
        {
            return new Rect(rect.x + rect.width / 2, rect.y, rect.width / 2, rect.height);
        }

        public static Rect Pad(this Rect rect, float padding)
        {
            return new Rect(rect.x + padding, rect.y + padding, rect.width - padding * 2, rect.height - padding * 2);
        }

        public static Rect Pad(this Rect rect, float left, float top, float right, float bottom)
        {
            return new Rect(rect.x + left, rect.y + top, rect.width - right - left, rect.height - bottom - top);
        }

        public static Rect Clip(this Rect rect, Rect target)
        {
            float x = rect.x;
            if (x < target.x)
                x = target.x;
            if (x > target.xMax)
                x = target.xMax;

            float y = rect.y;
            if (y < target.y)
                y = target.y;
            if (y > target.yMax)
                y = target.yMax;

            float width = rect.width;
            if (x + width > target.xMax)
                width = target.xMax - rect.x;

            float height = rect.height;
            if (y + height > target.yMax)
                height = target.yMax - rect.y;

            return new Rect(x, y, width, height);
        }

        public static Rect Cover(this Rect rect, params Rect[] targets)
        {
            float x = targets.Min(t => t.x);
            float y = targets.Min(t => t.y);
            float width = targets.Max(t => t.xMax - x);
            float height = targets.Max(t => t.yMax - y);

            return new Rect(x, y, width, height);
        }

        public static Rect[] SplitVertical(this Rect rect, int count)
        {
            float height = rect.height / count;

            Rect[] rects = new Rect[count];
            for (int i = 0; i < rects.Length; i++)
                rects[i] = new Rect(rect.x, rect.y + height * i, rect.width, height);

            return rects;
        }

        public static Rect[] SplitHorizontal(this Rect rect, int count)
        {
            float width = rect.width / count;

            Rect[] rects = new Rect[count];
            for (int i = 0; i < rects.Length; i++)
                rects[i] = new Rect(rect.x + width * i, rect.y, width, rect.height);

            return rects;
        }

        public static Rect SplitVerticalAndCombine(this Rect rect, int count, int start, int length = 1)
        {
            float height = rect.height / count;

            return new Rect(rect.x, rect.y + height * start, rect.width, height * length);
        }

        public static Rect SplitHorizontalAndCombine(this Rect rect, int count, int start, int length = 1)
        {
            float width = rect.width / count;

            return new Rect(rect.x + width * start, rect.y, width * length, rect.height);
        }

        public static bool OverlapsCompletelyBy(this Rect rect, Rect otherRect)
        {
            return OverlapsCompletely(otherRect, rect);
        }

        public static bool OverlapsCompletely(this Rect rect, Rect otherRect)
        {
            return rect.Contains(otherRect.min) && rect.Contains(otherRect.max);
        }

        #endregion
    }
}