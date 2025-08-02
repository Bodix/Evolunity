// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

namespace Evolutex.Evolunity.Components.Triggers
{
    [AddComponentMenu("Evolunity/Triggers/UI Button Trigger")]
    public class UiButtonTrigger : Trigger
    {
        [SerializeField, HideIf(nameof(HideButtonInInspector))]
        protected Button _uiButton;

        protected virtual bool HideButtonInInspector => false;

        protected override void EnterTrigger(Collider other)
        {
            ShowButton();
            _uiButton.onClick.AddListener(InvokeTrigger);
        }

        protected override void ExitTrigger(Collider other)
        {
            HideButton();
            _uiButton.onClick.RemoveListener(InvokeTrigger);
        }

        protected virtual void ShowButton()
        {
            _uiButton.gameObject.SetActive(true);
        }

        protected virtual void HideButton()
        {
            _uiButton.gameObject.SetActive(false);
        }
    }
}