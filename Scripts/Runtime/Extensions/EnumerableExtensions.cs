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
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);

                yield return item;
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            int index = 0;
            foreach (T item in enumerable)
            {
                action(item, index++);

                yield return item;
            }
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> enumerable, bool condition, Func<T, bool> predicate)
        {
            return condition ? enumerable.Where(predicate) : enumerable;
        }

        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.TakeWhile(item => !predicate(item));
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, T item)
        {
            return enumerable.Except(Enumerable.Repeat(item, 1));
        }

        public static IEnumerable<T> Concat<T>(IEnumerable<T> enumerable, T item)
        {
            return enumerable.Concat(Enumerable.Repeat(item, 1));
        }

        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<T> Reverse<T>(this IEnumerable<T> enumerable)
        {
            T[] array = enumerable.ToArray();

            for (int i = array.Length - 1; i >= 0; i--)
                yield return array[i];
        }

        public static T MinBy<T, TMin>(this IEnumerable<T> enumerable, Func<T, TMin> selector)
            where TMin : IComparable<TMin>
        {
            T min = default;
            bool first = true;

            foreach (T item in enumerable)
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

        public static T MaxBy<T, TMax>(this IEnumerable<T> enumerable, Func<T, TMax> selector)
            where TMax : IComparable<TMax>
        {
            T max = default;
            bool first = true;

            foreach (T item in enumerable)
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

        public static T FirstOrNull<T>(this IEnumerable<T> enumerable) where T : class
        {
            return enumerable.DefaultIfEmpty(null).FirstOrDefault();
        }

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            T[] array = enumerable.ToArray();

            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static T Random<T>(this IEnumerable<T> enumerable, Func<T, float> chanceSelector)
        {
            T[] orderedArray = enumerable.OrderByDescending(chanceSelector).ToArray();

            float[] chances = orderedArray.Select(chanceSelector).ToArray();
            float totalChance = chances.Sum();
            float chance = UnityEngine.Random.Range(0, totalChance);

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

        public static IEnumerable<T> Random<T>(this IEnumerable<T> enumerable, int amount)
        {
            return Shuffle(enumerable).Take(amount);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(item => UnityEngine.Random.value);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.IsEmpty();
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }

        public static string AsString<T>(this IEnumerable<T> enumerable)
        {
            return AsString(enumerable, x => x?.ToString(), ", ");
        }
        
        public static string AsString<T>(this IEnumerable<T> enumerable, Func<T, string> selector)
        {
            return AsString(enumerable, selector, ", ");
        }
        
        public static string AsString<T>(this IEnumerable<T> enumerable, string separator)
        {
            return AsString(enumerable, x => x?.ToString(), separator);
        }

        public static string AsString<T>(this IEnumerable<T> enumerable, Func<T, string> selector, string separator)
        {
            return enumerable.IsEmpty() 
                ? string.Empty 
                : string.Join(separator, enumerable.Select(x => selector(x) ?? "null"));
        }
    }
}