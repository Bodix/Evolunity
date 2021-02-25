// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Evolutex.Evolunity.Extensions
{
    public static class FunctionalExtensions
    {
        public static T With<T>(this T value, Action<T> action)
        {
            action.Invoke(value);

            return value;
        }
    }
}