// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Evolutex.Evolunity.Patterns
{
    public class Singleton<T> where T : class, new()
    {
        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());
        
        private Singleton() { }

        public static T Instance => instance.Value;
        public static bool IsInstanceCreated => instance.IsValueCreated;
    }
}