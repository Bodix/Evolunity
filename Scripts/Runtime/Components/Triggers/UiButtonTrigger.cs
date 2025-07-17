// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using Evolutex.Evolunity.Components.Physics;
using Evolutex.Evolunity.Attributes;

namespace Evolutex.Evolunity.Components.Triggers
{
    [AddComponentMenu("Evolunity/Triggers/UI Button Trigger")]
    [RequireComponent(typeof(BoxTrigger))]
    public class UiButtonTrigger : MonoBehaviour
    {
        [SerializeReference, TypeSelector]
        private ITriggerable _triggerable;
        [SerializeField, HideIf(nameof(HideButtonInInspector))]
        protected Button _uiButton;

        private BoxTrigger _boxTrigger;

        protected virtual bool HideButtonInInspector => false;
        public BoxTrigger BoxTrigger => _boxTrigger;

        private void Awake()
        {
            _boxTrigger = GetComponent<BoxTrigger>();
            _boxTrigger.TriggerEnter += ShowInteractButton;
            _boxTrigger.TriggerExit += HideInteractButton;
        }

        private void OnDestroy()
        {
            _boxTrigger.TriggerEnter -= ShowInteractButton;
            _boxTrigger.TriggerExit += HideInteractButton;
        }

        private void ShowInteractButton(Collider obj)
        {
            _uiButton.gameObject.SetActive(true);
            _uiButton.onClick.AddListener(_triggerable.Trigger);
        }

        private void HideInteractButton(Collider obj)
        {
            _uiButton.gameObject.SetActive(false);
            _uiButton.onClick.RemoveListener(_triggerable.Trigger);
        }
    }
}