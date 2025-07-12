// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Evolutex.Evolunity.Patterns
{
    [Serializable]
    public class ObservableInt : ObservableProperty<int>
    {
        public ObservableInt(int value) : base(value)
        {
        }
    }
}