// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;

namespace Evolutex.Evolunity.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddToCollection<TKey, TCollection, TValue>(this IDictionary<TKey, TCollection> dictionary,
            TKey key, TValue value) where TCollection : ICollection<TValue>, new()
        {
            dictionary.GetValue(key).OrAdd(new TCollection()).Add(value);
        }

        public static bool RemoveFromCollection<TKey, TCollection, TValue>(this IDictionary<TKey, TCollection> dictionary,
            TKey key, TValue value) where TCollection : ICollection<TValue>
        {
            return dictionary.ContainsKey(key) && dictionary[key] != null && dictionary[key].Remove(value);
        }

        public static Value<TKey, TValue> GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return new Value<TKey, TValue>(dictionary, key);
        }

        public readonly struct Value<TKey, TValue>
        {
            private readonly IDictionary<TKey, TValue> dictionary;
            private readonly TKey key;

            internal Value(IDictionary<TKey, TValue> dictionary, TKey key)
            {
                this.dictionary = dictionary;
                this.key = key;
            }

            public TValue OrDefault() => Or(default(TValue));

            public TValue Or(TValue value) =>
                dictionary.TryGetValue(key, out TValue x) ? x : value;

            public TValue Or(Func<TValue> value) =>
                dictionary.TryGetValue(key, out TValue x) ? x : value();

            public TValue OrAdd(TValue value) =>
                dictionary.TryGetValue(key, out TValue x) ? x : dictionary[key] = value;

            public TValue OrAdd(Func<TValue> value) =>
                dictionary.TryGetValue(key, out TValue x) ? x : dictionary[key] = value();

            public TValue OrException() => OrException($"Couldn't find value with [{key}] key.");

            public TValue OrException(string message) =>
                dictionary.TryGetValue(key, out TValue value) ? value : throw new KeyNotFoundException(message);
        }
    }
}