// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using Evolutex.Evolunity.Attributes;

namespace Evolutex.Evolunity.Components.Triggers
{
    [AddComponentMenu("Evolunity/Triggers/UI Button Trigger")]
    public class UiButtonTrigger : AbstractTrigger
    {
        [SerializeReference, TypeSelector]
        private ITriggerable _triggerable;
        [SerializeField, HideIf(nameof(HideButtonInInspector))]
        protected Button _uiButton;

        protected virtual bool HideButtonInInspector => false;

        protected override void EnterTrigger(Collider other)
        {
            _uiButton.gameObject.SetActive(true);
            _uiButton.onClick.AddListener(_triggerable.Trigger);
        }

        protected override void ExitTrigger(Collider other)
        {
            _uiButton.gameObject.SetActive(false);
            _uiButton.onClick.RemoveListener(_triggerable.Trigger);
        }
    }
}