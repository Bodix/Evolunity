// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using Evolutex.Evolunity.Components.Physics;

namespace Evolutex.Evolunity.Components.Triggers
{
    [AddComponentMenu("Evolunity/Triggers/UI Button Trigger")]
    [RequireComponent(typeof(BoxTrigger))]
    public class UiButtonTrigger : MonoBehaviour
    {
        [SerializeField, InterfaceType(typeof(ITriggerable))]
        private Object triggerable;
        [SerializeField, HideIf(nameof(HideButtonInInspector))]
        protected Button _uiButton;

        private BoxTrigger _boxTrigger;

        protected virtual bool HideButtonInInspector => false;
        public BoxTrigger BoxTrigger => _boxTrigger;
        private ITriggerable Triggerable => (ITriggerable)triggerable;

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
            _uiButton.onClick.AddListener(Triggerable.Trigger);
        }

        private void HideInteractButton(Collider obj)
        {
            _uiButton.gameObject.SetActive(false);
            _uiButton.onClick.RemoveListener(Triggerable.Trigger);
        }
    }
}