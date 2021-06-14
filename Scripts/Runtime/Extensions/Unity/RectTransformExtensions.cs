// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    public static class RectTransformExtensions
    {
        private static readonly Vector3[] worldCornersBuffer = new Vector3[4];

        // https://answers.unity.com/questions/1100493/convert-recttransformrect-to-rect-world.html
        public static Rect GetWorldRect(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(worldCornersBuffer);

            // Get the bottom left corner.
            Vector3 position = worldCornersBuffer[0];
            Vector2 size = Vector2.Scale(rectTransform.rect.size, rectTransform.lossyScale);

            return new Rect(position, size);
        }

        // https://answers.unity.com/questions/1013011/convert-recttransform-rect-to-screen-space.html
        public static Rect GetScreenRect(this RectTransform rectTransform)
        {
            Vector2 size = Vector2.Scale(rectTransform.rect.size, rectTransform.lossyScale);
            Vector2 position = (Vector2) rectTransform.position - size * 0.5f;

            return new Rect(position, size);
        }

        // https://answers.unity.com/questions/1013011/convert-recttransform-rect-to-screen-space.html
        public static Bounds GetBounds(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(worldCornersBuffer);

            Bounds bounds = new Bounds(worldCornersBuffer[0], Vector3.zero);
            for (int i = 1; i < 4; ++i)
                bounds.Encapsulate(worldCornersBuffer[i]);

            return bounds;
        }

        // https://forum.unity.com/threads/how-do-you-get-the-parent-canvas.311240/
        public static Canvas GetRootCanvas(this RectTransform rectTransform)
        {
            Canvas[] canvases = rectTransform.GetComponentsInParent<Canvas>();

            return canvases.Length > 0 ? canvases[canvases.Length - 1] : null;
        }

        public static Canvas GetRootCanvasInternal(this RectTransform rectTransform)
        {
            Canvas canvas = rectTransform.GetComponentInParent<Canvas>();

            if (canvas)
                return canvas.isRootCanvas ? canvas : canvas.rootCanvas;
            else
                return null;
        }
    }
}