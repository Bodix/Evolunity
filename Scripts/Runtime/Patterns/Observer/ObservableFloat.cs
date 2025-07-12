// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Evolutex.Evolunity.Patterns
{
    [Serializable]
    public class ObservableFloat : ObservableProperty<float>
    {
        public ObservableFloat(float value) : base(value)
        {
        }
    }
}