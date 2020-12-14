/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using System;
using System.Globalization;
using System.Linq;

namespace Evolutex.Evolunity.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string @string) =>
            @string == string.Empty;

        public static bool IsNullOrEmpty(this string @string) =>
            string.IsNullOrEmpty(@string);

        public static bool IsNumber(this string @string) =>
            double.TryParse(@string, NumberStyles.Any, CultureInfo.InvariantCulture, out _);

        public static bool Contains(this string @string, char @char) =>
            @string.IndexOf(@char) != 1;

        public static bool ContainsAnyOf(this string @string, params char[] chars) => 
            chars.Any(@string.Contains);

        public static string WithoutPrefix(this string @string, string prefix) =>
            @string.StartsWith(prefix) ? @string.Substring(prefix.Length) : @string;

        public static string WithoutSuffix(this string @string, string suffix) =>
            @string.EndsWith(suffix) ? @string.Remove(@string.Length - suffix.Length) : @string;

        public static string Without(this string @string, string part) =>
            @string.Replace(part, string.Empty);

        public static string AllAfter(this string @string, string part)
        {
            int index = @string.IndexOf(part, StringComparison.Ordinal);

            return index == -1 ? @string : @string.Substring(index + part.Length);
        }

        public static string FromLeft(this string @string, int length) => 
            @string.Length > length ? @string.Substring(0, length) : @string;

        public static string FromRight(this string @string, int length) => 
            @string.Length > length ? @string.Substring(@string.Length - length) : @string;

        public static float ToFloat(this string @string, NumberStyles style = NumberStyles.Any) =>
            float.Parse(@string, style, CultureInfo.InvariantCulture);

        public static float ToFloat(this string @string, float @default, NumberStyles style = NumberStyles.Any) =>
            float.TryParse(@string, style, CultureInfo.InvariantCulture, out float result) ? result : @default;

        public static int ToInt(this string @string, NumberStyles style = NumberStyles.Any) =>
            int.Parse(@string, style, CultureInfo.InvariantCulture);

        public static int ToInt(this string @string, int @default, NumberStyles style = NumberStyles.Any) =>
            int.TryParse(@string, style, CultureInfo.InvariantCulture, out int result) ? result : @default;

        public static T ToEnum<T>(this string @string) where T : struct, Enum =>
            (T) Enum.Parse(typeof(T), @string, true);
    }
}