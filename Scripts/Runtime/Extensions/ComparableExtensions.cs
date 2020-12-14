// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Evolutex.Evolunity.Extensions
{
    public static class ComparableExtensions
    {
        public static bool Between<T>(this T value, T min, T max) where T : IComparable<T> =>
            value.GreaterOrEqualTo(min) && value.LessOrEqualTo(max);

        public static T ButNotLessThan<T>(this T value, T min) where T : IComparable<T> =>
            value.LessThan(min) ? min : value;

        public static T ButNotGreaterThan<T>(this T value, T max) where T : IComparable<T> =>
            value.GreaterThan(max) ? max : value;

        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T> =>
            value.LessThan(min) ? min : value.GreaterThan(max) ? max : value;

        private static bool LessThan<T>(this T value, T than) where T : IComparable<T> =>
            value.CompareTo(than) < 0;

        private static bool LessOrEqualTo<T>(this T value, T to) where T : IComparable<T> =>
            value.CompareTo(to) <= 0;

        private static bool GreaterThan<T>(this T value, T than) where T : IComparable<T> =>
            value.CompareTo(than) > 0;

        private static bool GreaterOrEqualTo<T>(this T value, T to) where T : IComparable<T> =>
            value.CompareTo(to) >= 0;
    }
}