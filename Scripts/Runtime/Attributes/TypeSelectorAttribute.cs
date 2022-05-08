// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

#if UNITY_2019_3_OR_NEWER

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class TypeSelectorAttribute : PropertyAttribute { }
}

#endif