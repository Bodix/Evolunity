// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Evolutex.Evolunity.Extensions
{
    // TODO: Optimize all ToArray() invocations. [#optimization]

    public static class EnumerableExtensions
    {
        // System.Random is used because UnityEngine.Random only works in the main thread.
        private static readonly Random _random = new Random();

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
                action(item);

            return source;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int index = 0;
            foreach (T item in source)
                action(item, index++);

            return source;
        }

        public static IEnumerable<T> ForEachLazy<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);

                yield return item;
            }
        }

        public static IEnumerable<T> ForEachLazy<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int index = 0;
            foreach (T item in source)
            {
                action(item, index++);

                yield return item;
            }
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source.TakeWhile(item => !predicate(item));
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T item)
        {
            return source.Except(Enumerable.Repeat(item, 1));
        }

        public static IEnumerable<T> Concat<T>(IEnumerable<T> source, T item)
        {
            return source.Concat(Enumerable.Repeat(item, 1));
        }

        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<T> Reverse<T>(this IEnumerable<T> source)
        {
            return Enumerable.Reverse(source);
        }

        public static IEnumerable<T> ResizeWithRepeating<T>(this IEnumerable<T> source,
            int newSize, T defaultValue = default)
        {
            List<T> list = source.ToList();
            int currentSize = list.Count;

            if (newSize < currentSize)
            {
                list.RemoveRange(newSize, currentSize - newSize);
            }
            else if (newSize > currentSize)
            {
                if (newSize > list.Capacity)
                    list.Capacity = newSize;

                list.AddRange(Enumerable.Repeat(defaultValue, newSize - currentSize));
            }

            return list;
        }

        public static IEnumerable<T> RemoveDuplicates<T>(this IEnumerable<T> enumerable,
            IEqualityComparer<T> comparer = null)
        {
            IList<T> list = enumerable.ToList();
            if (list.Count <= 1)
                return list;

            HashSet<T> hashSet = new HashSet<T>(comparer ?? EqualityComparer<T>.Default);
            int index = 0;
            while (index < list.Count)
            {
                if (hashSet.Add(list[index]))
                    index++;
                else
                    list.RemoveAt(index);
            }

            return list;
        }

        public static T MinBy<T, TMin>(this IEnumerable<T> source, Func<T, TMin> selector)
            where TMin : IComparable<TMin>
        {
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

        public static T FirstOrNull<T>(this IEnumerable<T> source) where T : class
        {
            return source.DefaultIfEmpty(null).FirstOrDefault();
        }

        public static T Random<T>(this IEnumerable<T> source)
        {
            return Random(source, _random);
        }

        public static T Random<T>(this IEnumerable<T> source, Random random)
        {
            T[] array = source.ToArray();

            return array[random.Next(0, array.Length)];
        }

        public static T Random<T>(this IEnumerable<T> source, Func<T, float> chanceSelector)
        {
            return Random(source, chanceSelector, _random);
        }

        public static T Random<T>(this IEnumerable<T> source, Func<T, float> chanceSelector, Random random)
        {
            T[] orderedArray = source.OrderByDescending(chanceSelector).ToArray();

            float[] chances = orderedArray.Select(chanceSelector).ToArray();
            float totalChance = chances.Sum();
            double chance = random.NextDouble() * totalChance;

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
            return Random(source, amount, _random);
        }

        public static IEnumerable<T> Random<T>(this IEnumerable<T> source, int amount, Random random)
        {
            return Shuffle(source, random).Take(amount);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return Shuffle(source, _random);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random)
        {
            return source.OrderBy(item => random.Next());
        }

        public static IEnumerable<T> Clone<T>(this IEnumerable<T> source) where T : ICloneable
        {
            return source.Select(item => (T)item.Clone()).ToList();
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.IsEmpty();
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            return new ObservableCollection<T>(source);
        }

        public static string AsString<T>(this IEnumerable<T> source)
        {
            return AsString(source, x => x?.ToString());
        }

        public static string AsString<T>(this IEnumerable<T> source, string separator)
        {
            return AsString(source, x => x?.ToString(), separator);
        }

        public static string AsString<T>(this IEnumerable<T> source, Func<T, string> selector, string separator = ", ")
        {
            return source.IsEmpty()
                ? string.Empty
                : string.Join(separator, source.Select(x => selector(x) ?? "null"));
        }
    }
}