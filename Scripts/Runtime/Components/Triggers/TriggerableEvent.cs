// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.Events;

namespace Evolutex.Evolunity.Components.Triggers
{
    [AddComponentMenu("Evolunity/Triggers/Triggerable Event")]
    public class TriggerableEvent : MonoBehaviour, ITriggerable
    {
        public UnityEvent Event;
        
        public void Trigger()
        {
            Event.Invoke();
        }
    }
}