// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Extensions
{
    public static class GraphicExtensions
    {
        public static void SetAlpha(this Graphic graphic, float value)
        {
            graphic.color = graphic.color.WithAlpha(value);
        }

        public static void SetAlpha(this SpriteRenderer spriteRenderer, float value)
        {
            spriteRenderer.color = spriteRenderer.color.WithAlpha(value);
        }
    }
}