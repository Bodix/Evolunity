/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    public static class ColorExtensions
    {
        public static Color WithR(this Color color, float value)
        {
            color.r = value;

            return color;
        }

        public static Color WithG(this Color color, float value)
        {
            color.g = value;

            return color;
        }

        public static Color WithB(this Color color, float value)
        {
            color.b = value;

            return color;
        }

        public static Color WithAlpha(this Color color, float value)
        {
            color.a = value;

            return color;
        }
        
        public static float Hue(this Color color)
        {
            float min = Mathf.Min(color.r, color.g, color.b);
            float max = Mathf.Max(color.r, color.g, color.b);
            
            if (max == 0 || max == min)
                return 0;
            else
            {
                float hue;
                float delta = max - min;

                if (color.r == max)
                    hue = 0 + (color.g - color.b) / delta;
                else if (color.g == max)
                    hue = 2 + (color.b - color.r) / delta;
                else
                    hue = 4 + (color.r - color.g) / delta;

                hue *= 60;

                if (hue < 0) 
                    hue += 360;

                return hue;
            }
        }

        public static float Brightness(this Color color)
        {
            return Mathf.Max(color.r, color.g, color.b) * 100 / 255;
        }

        public static float Saturation(this Color color)
        {
            if (color == Color.black)
                return 0;

            float max = Mathf.Max(color.r, color.g, color.b);
            float delta = max - Mathf.Min(color.r, color.g, color.b);

            return 255 * delta / max;
        }
        
        public static Color Closest(this Color color, Color[] colors)
        {
            return colors.MinBy(x => GetDiff(x, color));
        }

        private static float GetDiff(Color color, Color otherColor)
        {
            float a = color.a - otherColor.a,
                r = color.r - otherColor.r,
                g = color.g - otherColor.g,
                b = color.b - otherColor.b;
            
            return a * a + r * r + g * g + b * b;
        }
    }
}