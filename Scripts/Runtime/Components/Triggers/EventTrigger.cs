// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine.Events;

namespace Evolutex.Evolunity.Components.Triggers
{
    [Serializable]
    public class EventTrigger : ITrigger
    {
        public UnityEvent Event;
        
        public void Trigger()
        {
            Event.Invoke();
        }
    }
}