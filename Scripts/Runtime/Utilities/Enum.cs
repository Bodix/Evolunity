// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Linq;

namespace Evolutex.Evolunity.Utilities
{
    public static class Enum<T> where T : struct, Enum
    {
        public static int Count => Enum.GetValues(typeof(T)).Length;

        public static T Parse(string value, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static T[] GetValues()
        {
            Array values = Enum.GetValues(typeof(T));

            return values.Cast<T>().ToArray();
        }

        public static T GetRandomValue()
        {
            Array values = Enum.GetValues(typeof(T));

            return (T)values.GetValue(new Random().Next(values.Length));
        }
    }
}