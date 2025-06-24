// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Patterns
{
    [Serializable]
    public class ObservableProperty<T>
    {
        [SerializeField]
        private T _value;

        public ObservableProperty(T value)
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

        public static implicit operator T(ObservableProperty<T> observable)
        {
            return observable.Value;
        }
    }
}