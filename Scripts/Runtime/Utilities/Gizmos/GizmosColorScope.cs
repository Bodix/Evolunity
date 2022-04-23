// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Utilities.Gizmos
{
    public readonly struct GizmosColorScope : IDisposable
    {
        private readonly Color _prevColor;

        public GizmosColorScope(Color color)
        {
            _prevColor = UnityEngine.Gizmos.color;
            
            UnityEngine.Gizmos.color = color == default ? _prevColor : color;
        }

        public void Dispose()
        {
            UnityEngine.Gizmos.color = _prevColor;
        }
    }
}