// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using Evolutex.Evolunity.Components.Physics;

namespace Evolutex.Evolunity.Components.Triggers
{
    [RequireComponent(typeof(BoxTrigger))]
    public class UiButtonTrigger : MonoBehaviour
    {
        // [SerializeReference, SubclassSelector]
        // private ITrigger _trigger;
        [SerializeField, HideIf(nameof(HideButtonInInspector))]
        protected Button _uiButton;

        private BoxTrigger _boxTrigger;

        public BoxTrigger BoxTrigger => _boxTrigger;
        protected virtual bool HideButtonInInspector => false;

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
            // _uiButton.onClick.AddListener(_trigger.Trigger);
        }

        private void HideInteractButton(Collider obj)
        {
            _uiButton.gameObject.SetActive(false);
            // _uiButton.onClick.RemoveListener(_trigger.Trigger);
        }
    }
}