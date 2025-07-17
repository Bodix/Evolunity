// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using Evolutex.Evolunity.Components.Physics;

namespace Evolutex.Evolunity.Components.Triggers
{
    [AddComponentMenu("Evolunity/Triggers/Auto Trigger")]
    [RequireComponent(typeof(BoxTrigger))]
    public class AutoTrigger : MonoBehaviour
    {
        [SerializeField, InterfaceType(typeof(ITriggerable))]
        private Object triggerable;

        private BoxTrigger _boxTrigger;

        public BoxTrigger BoxTrigger => _boxTrigger;
        private ITriggerable Triggerable => (ITriggerable)triggerable;

        private void Awake()
        {
            _boxTrigger = GetComponent<BoxTrigger>();
            _boxTrigger.TriggerEnter += Trigger;
        }

        private void OnDestroy()
        {
            _boxTrigger.TriggerEnter -= Trigger;
        }

        private void Trigger(Collider obj)
        {
            Triggerable.Trigger();
        }
    }
}