// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using NaughtyAttributes;
using UnityEngine;

namespace Evolutex.Evolunity.Components.Triggers
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class AbstractTrigger : MonoBehaviour
    {
        public bool DisableAfterEnter = false;
        [Tag, ValidateInput(nameof(ValidateTag),
             "If \"(None)\" is selected, then collider with any tag will activate this trigger")]
        public string AllowedTag = "Player";

        private BoxCollider _boxCollider;
#if UNITY_2023_2_OR_NEWER
        // https://docs.unity3d.com/2023.2/Documentation/ScriptReference/TagHandle.html
        private TagHandle _tagHandle;
#endif

        public BoxCollider BoxCollider => _boxCollider;
        
        public event Action Triggered;

        private void OnValidate()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.isTrigger = true;
        }

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
#if UNITY_2023_2_OR_NEWER
            _tagHandle = TagHandle.GetExistingTag(AllowedTag);
#endif
        }

        private void OnTriggerEnter(Collider other)
        {
#if UNITY_2023_2_OR_NEWER
            if (string.IsNullOrEmpty(AllowedTag) || other.CompareTag(_tagHandle))
#else
            if (string.IsNullOrEmpty(AllowedTag) || other.CompareTag(AllowedTag))
#endif
            {
                EnterTrigger(other);

                if (DisableAfterEnter)
                    gameObject.SetActive(false);
            }
        }

        private void OnTriggerStay(Collider other)
        {
#if UNITY_2023_2_OR_NEWER
            if (string.IsNullOrEmpty(AllowedTag) || other.CompareTag(_tagHandle))
#else
            if (string.IsNullOrEmpty(AllowedTag) || other.CompareTag(AllowedTag))
#endif
                StayInTrigger(other);
        }

        private void OnTriggerExit(Collider other)
        {
#if UNITY_2023_2_OR_NEWER
            if (string.IsNullOrEmpty(AllowedTag) || other.CompareTag(_tagHandle))
#else
            if (string.IsNullOrEmpty(AllowedTag) || other.CompareTag(AllowedTag))
#endif
                ExitTrigger(other);
        }

        protected abstract void EnterTrigger(Collider other);

        protected virtual void StayInTrigger(Collider other) { }

        protected virtual void ExitTrigger(Collider other) { }

        protected void InvokeTriggeredEvent()
        {
            Triggered?.Invoke();
        }

        // Should be protected to be visible by in derived class by NaughtyAttributes.
        protected bool ValidateTag(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}