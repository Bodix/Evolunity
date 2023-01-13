﻿// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Structs
{
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

        public static implicit operator T(ObservableValue<T> observable)
        {
            return observable.Value;
        }
    }
}