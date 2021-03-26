// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Evolutex.Evolunity.Extensions
{
    public static class EnumerableExtensions
    {
        private static Random rand = new Random();

        public static IEnumerable<T> ForEachLazy<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            foreach (T item in source)
            {
                action(item);

                yield return item;
            }
        }

        public static IEnumerable<T> ForEachLazy<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            int index = 0;
            foreach (T item in source)
            {
                action(item, index++);

                yield return item;
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            foreach (var item in source)
            {
                action(item);
            }
            return source;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            int index = 0;
            foreach (T item in source)
            {
                action(item, index++);
            }
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return source.TakeWhile(item => !predicate(item));
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T item)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            return source.Except(Enumerable.Repeat(item, 1));
        }

        public static IEnumerable<T> Concat<T>(IEnumerable<T> source, T item)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            return source.Concat(Enumerable.Repeat(item, 1));
        }

        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return source ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<T> Reverse<T>(this IEnumerable<T> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            T[] array = source.ToArray();

            for (int i = array.Length - 1; i >= 0; i--)
                yield return array[i];
        }

        public static T MinBy<T, TMin>(this IEnumerable<T> source, Func<T, TMin> selector)
            where TMin : IComparable<TMin>
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            T min = default;
            bool first = true;

            foreach (T item in source)
            {
                if (first)
                {
                    min = item;
                    first = false;
                }
                else if (selector(item).CompareTo(selector(min)) < 0)
                {
                    min = item;
                }
            }

            return min;
        }

        public static T MaxBy<T, TMax>(this IEnumerable<T> source, Func<T, TMax> selector)
            where TMax : IComparable<TMax>
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            T max = default;
            bool first = true;

            foreach (T item in source)
            {
                if (first)
                {
                    max = item;
                    first = false;
                }
                else if (selector(item).CompareTo(selector(max)) > 0)
                {
                    max = item;
                }
            }

            return max;
        }

        public static T Random<T>(this IEnumerable<T> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            T[] array = source.ToArray();

            return array[rand.Next(0, array.Length)];
        }

        public static T Random<T>(this IEnumerable<T> source, Func<T, float> chanceSelector)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (chanceSelector is null)
                throw new ArgumentNullException(nameof(chanceSelector));

            T[] orderedArray = source.OrderByDescending(chanceSelector).ToArray();

            float[] chances = orderedArray.Select(chanceSelector).ToArray();
            float totalChance = chances.Sum();
            float chance = rand.Next(0, totalChance);

            int index = 0;
            for (int i = 0; i < chances.Length; i++)
            {
                if (chance <= chances[i])
                {
                    index = i;

                    break;
                }
                else chance -= chances[i];
            }

            return orderedArray[index];
        }

        public static IEnumerable<T> Random<T>(this IEnumerable<T> source, int amount)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return Shuffle(source).Take(amount);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return source.OrderBy(item => rand.Next());
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return !source.Any();
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return new ObservableCollection<T>(source);
        }

        public static string AsString<T>(this IEnumerable<T> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return AsString(source, x => x?.ToString());
        }

        public static string AsString<T>(this IEnumerable<T> source, string separator)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (separator is null)
                throw new ArgumentNullException(nameof(separator));

            return AsString(source, x => x?.ToString(), separator);
        }

        public static string AsString<T>(this IEnumerable<T> source, Func<T, string> selector, string separator = ", ")
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (separator is null)
                throw new ArgumentNullException(nameof(selector));

            return source.IsEmpty() 
                ? string.Empty 
                : string.Join(separator, source.Select(x => selector(x) ?? "null"));
        }
    }
}