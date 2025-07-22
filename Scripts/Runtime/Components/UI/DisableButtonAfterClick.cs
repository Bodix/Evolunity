// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Components.UI
{
    [RequireComponent(typeof(Button))]
    public class DisableButtonAfterClick : MonoBehaviour
    {
        private void Awake()
        {
            Button button = GetComponent<Button>();

            button.onClick.AddListener(() => button.interactable = false);
        }
    }
}