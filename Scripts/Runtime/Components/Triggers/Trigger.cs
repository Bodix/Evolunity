// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using Evolutex.Evolunity.Attributes;
using NaughtyAttributes;

namespace Evolutex.Evolunity.Components.Triggers
{
    [AddComponentMenu("Evolunity/Triggers/Trigger")]
    public class Trigger : AbstractTrigger
    {
        [SerializeReference, TypeSelector, HideIf(nameof(HideTriggerableInInspector))]
        protected ITriggerable _triggerable;

        protected virtual bool HideTriggerableInInspector => false;

        protected override void EnterTrigger(Collider obj)
        {
            _triggerable.Trigger();

            InvokeTriggeredEvent();
        }
    }
}