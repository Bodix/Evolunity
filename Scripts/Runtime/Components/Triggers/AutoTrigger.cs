// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using Evolutex.Evolunity.Components.Physics;
using Evolutex.Evolunity.Attributes;

namespace Evolutex.Evolunity.Components.Triggers
{
    [AddComponentMenu("Evolunity/Triggers/Auto Trigger")]
    [RequireComponent(typeof(BoxTrigger))]
    public class AutoTrigger : MonoBehaviour
    {
        [SerializeReference, TypeSelector]
        private ITriggerable _triggerable;

        private BoxTrigger _boxTrigger;

        public BoxTrigger BoxTrigger => _boxTrigger;

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
            _triggerable.Trigger();
        }
    }
}