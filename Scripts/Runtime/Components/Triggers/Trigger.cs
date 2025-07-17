// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using Evolutex.Evolunity.Attributes;

namespace Evolutex.Evolunity.Components.Triggers
{
    [AddComponentMenu("Evolunity/Triggers/Trigger")]
    public class Trigger : AbstractTrigger
    {
        [SerializeReference, TypeSelector]
        private ITriggerable _triggerable;

        protected override void EnterTrigger(Collider obj)
        {
            _triggerable.Trigger();
            
            InvokeTriggeredEvent();
        }
    }
}