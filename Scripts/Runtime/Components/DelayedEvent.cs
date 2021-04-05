// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;
using UnityEngine.Events;
using DelayUtility = Evolutex.Evolunity.Utilities.Delay;

namespace Evolutex.Evolunity.Components
{
    [AddComponentMenu("Evolunity/Delayed Event")]
    public class DelayedEvent : MonoBehaviour
    {
        public UnityEvent Event;
        [Delayed]
        public float Delay = 1f;
        public DelayedEventDelayType DelayType = DelayedEventDelayType.Seconds;
        public DelayedEventInvokeMethod InvokeMethod = DelayedEventInvokeMethod.Start;

        public enum DelayedEventInvokeMethod
        {
            Awake,
            Start
        }

        public enum DelayedEventDelayType
        {
            Frames,
            Seconds
        }
        
        private void Awake()
        {
            if (InvokeMethod == DelayedEventInvokeMethod.Awake)
                DelayedInvoke();
        }

        private void Start()
        {
            if (InvokeMethod == DelayedEventInvokeMethod.Start)
                DelayedInvoke();
        }

        private void OnValidate()
        {
            if (DelayType == DelayedEventDelayType.Frames)
                RoundDelayToInt();
        }
        
        [ContextMenu("Delayed Invoke")]
        public void DelayedInvoke()
        {
            switch (DelayType)
            {
                case DelayedEventDelayType.Frames:
                    RoundDelayToInt();
                    DelayUtility.ForFrames((int) Delay, Invoke, this);
                    break;
                case DelayedEventDelayType.Seconds:
                    DelayUtility.ForSeconds(Delay, Invoke, this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [ContextMenu("Invoke")]
        public void Invoke()
        {
            Event.Invoke();
        }

        private void RoundDelayToInt()
        {
            Delay = Mathf.RoundToInt(Delay);
        }
    }
}