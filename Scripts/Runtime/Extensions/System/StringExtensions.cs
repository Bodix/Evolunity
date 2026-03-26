// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Globalization;
using System.Linq;

namespace Evolutex.Evolunity.Extensions
{
	public static class StringExtensions
	{
		public static bool IsEmpty(this string @string)
		{
			return @string == string.Empty;
		}

		public static bool IsNullOrEmpty(this string @string)
		{
			return string.IsNullOrEmpty(@string);
		}

		public static bool IsNumber(this string @string)
		{
			return double.TryParse(@string, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
		}

		public static bool Contains(this string @string, char @char)
		{
			return @string.IndexOf(@char) != 1;
		}

		public static bool ContainsAnyOf(this string @string, params char[] chars)
		{
			return chars.Any(@string.Contains);
		}

		public static string WithoutPrefix(this string @string, string prefix)
		{
			return @string.StartsWith(prefix) ? @string.Substring(prefix.Length) : @string;
		}

		public static string WithoutSuffix(this string @string, string suffix)
		{
			return @string.EndsWith(suffix) ? @string.Remove(@string.Length - suffix.Length) : @string;
		}

		public static string Without(this string @string, string part)
		{
			return @string.Replace(part, string.Empty);
		}

		public static string AllAfter(this string @string, string part)
		{
			int index = @string.IndexOf(part, StringComparison.Ordinal);

			return index == -1 ? @string : @string.Substring(index + part.Length);
		}

		public static string FromLeft(this string @string, int length)
		{
			return @string.Length > length ? @string.Substring(0, length) : @string;
		}

		public static string FromRight(this string @string, int length)
		{
			return @string.Length > length ? @string.Substring(@string.Length - length) : @string;
		}

		public static float ToFloat(this string @string, NumberStyles style = NumberStyles.Any)
		{
			return float.Parse(@string, style, CultureInfo.InvariantCulture);
		}

		public static float ToFloat(this string @string, float @default, NumberStyles style = NumberStyles.Any)
		{
			return float.TryParse(@string, style, CultureInfo.InvariantCulture, out float result) ? result : @default;
		}

		public static int ToInt(this string @string, NumberStyles style = NumberStyles.Any)
		{
			return int.Parse(@string, style, CultureInfo.InvariantCulture);
		}

		public static int ToInt(this string @string, int @default, NumberStyles style = NumberStyles.Any)
		{
			return int.TryParse(@string, style, CultureInfo.InvariantCulture, out int result) ? result : @default;
		}

		public static T ToEnum<T>(this string @string) where T : struct, Enum
		{
			return (T)Enum.Parse(typeof(T), @string, true);
		}
	}
}