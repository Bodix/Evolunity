// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Components.Triggers
{
    [Serializable]
    public class TriggerableUnityObject : ITriggerable
    {
        [SerializeField, InterfaceType(typeof(ITriggerable))]
        private UnityEngine.Object _object;

        public void Trigger()
        {
            ((ITriggerable)_object).Trigger();
        }
    }
}