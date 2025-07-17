// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using Evolutex.Evolunity.Components.Physics;

namespace Evolutex.Evolunity.Components.Triggers
{
    [RequireComponent(typeof(BoxTrigger))]
    public class AutoTrigger : MonoBehaviour
    {
        // [SerializeReference, SubclassSelector]
        // private ITrigger _trigger;
        [SerializeField, InterfaceType(typeof(ITrigger))]
        private Object trigger;
        private ITrigger _trigger => (ITrigger)trigger;

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
            _trigger.Trigger();
        }
    }
}