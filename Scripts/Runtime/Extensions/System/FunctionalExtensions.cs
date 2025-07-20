// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Evolutex.Evolunity.Extensions
{
    public static class FunctionalExtensions
    {
        public static T With<T>(this T value, Action<T> action) where T : class
        {
            action.Invoke(value);

            return value;
        }

        public delegate void ActionWithRef<T>(ref T obj);

        public static T WithValue<T>(this T value, ActionWithRef<T> action) where T : struct
        {
            action.Invoke(ref value);

            return value;
        }
    }
}