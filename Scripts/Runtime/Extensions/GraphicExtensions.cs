/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

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