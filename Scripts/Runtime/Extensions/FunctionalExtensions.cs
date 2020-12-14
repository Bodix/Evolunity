/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

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