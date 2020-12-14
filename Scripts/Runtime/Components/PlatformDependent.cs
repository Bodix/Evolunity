// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolunity.Components
{
    [AddComponentMenu("Toolkit/Platform Dependent", 10000)]
    [DisallowMultipleComponent]
    public sealed class PlatformDependent : MonoBehaviour
    {
        [SerializeField]
        private DisableMethod disableMethod = DisableMethod.Destroy;
        [SerializeField]
        private bool PC = true;
        [SerializeField]
        private bool iOS = true;
        [SerializeField]
        private bool android = true;
        [SerializeField]
        private bool webGL = true;
        [SerializeField]
        private bool other = true;

        private bool disable;

        private void Awake()
        {
            RuntimePlatform platform = Application.platform;

            disable =
                ((platform == RuntimePlatform.WindowsPlayer
                  || platform == RuntimePlatform.WindowsEditor
                  || platform == RuntimePlatform.OSXPlayer
                  || platform == RuntimePlatform.OSXEditor
                  || platform == RuntimePlatform.LinuxPlayer
                  || platform == RuntimePlatform.LinuxEditor) && !PC) ||
                (platform == RuntimePlatform.IPhonePlayer && !iOS) ||
                (platform == RuntimePlatform.Android && !android) ||
                (platform == RuntimePlatform.WebGLPlayer && !webGL) ||
                ((platform != RuntimePlatform.WindowsPlayer
                 && platform != RuntimePlatform.WindowsEditor
                 && platform != RuntimePlatform.OSXPlayer
                 && platform != RuntimePlatform.OSXEditor
                 && platform != RuntimePlatform.LinuxPlayer
                 && platform != RuntimePlatform.LinuxEditor
                 && platform != RuntimePlatform.IPhonePlayer
                 && platform != RuntimePlatform.Android
                 && platform != RuntimePlatform.WebGLPlayer) && !other);

            if (disable)
                switch (disableMethod)
                {
                    case DisableMethod.Destroy:
                        Destroy(gameObject);
                        break;
                    case DisableMethod.Disable:
                        gameObject.SetActive(false);
                        break;
                }
        }

        private void OnDisable()
        {
            if (disable && disableMethod != DisableMethod.Destroy)
                Debug.Log("The GameObject \"" + gameObject.name + "\" was disabled by the PlatformDependent component",
                    this);
        }

        private void OnDestroy()
        {
            if (disable)
                Debug.Log("The GameObject \"" + gameObject.name + "\" was destroyed by the PlatformDependent component");
        }
    }
}