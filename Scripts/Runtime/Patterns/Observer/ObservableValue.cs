// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Patterns
{
    /// <summary>
    /// Suitable for use only as fields or local variables. Not suitable for use as a property, as it will be copied
    /// as a value type when accessed. If you need to use it as a property, use <see cref="ObservableProperty{T}"/> instead.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public struct ObservableValue<T>
    {
        [SerializeField]
        private T _value;

        public ObservableValue(T value)
        {
            _value = value;

            Updated = null;
        }

        public event Action<T> Updated;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;

                Updated?.Invoke(_value);
            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator T(ObservableValue<T> observable)
        {
            return observable.Value;
        }
    }
}