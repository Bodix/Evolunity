// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Components
{
    [AddComponentMenu("Evolunity/Development Only")]
    [DisallowMultipleComponent]
    public sealed class DevelopmentOnly : MonoBehaviour
    {
        [SerializeField]
        private DisableMethod disableMethod = DisableMethod.Destroy;

        private bool disable;

        private void Awake()
        {
#if !DEVELOPMENT
            disable = true;

            switch (disableMethod)
            {
                case DisableMethod.Destroy:
                    Destroy(gameObject);
                    break;
                case DisableMethod.Disable:
                    gameObject.SetActive(false);
                    break;
            }
#endif
        }

        private void OnDisable()
        {
            if (disable && disableMethod == DisableMethod.Disable)
                Debug.Log("The GameObject \"" + gameObject.name + "\" was disabled by the DevelopmentOnly component",
                    this);
        }

        private void OnDestroy()
        {
            if (disable && disableMethod == DisableMethod.Destroy)
                Debug.Log("The GameObject \"" + gameObject.name + "\" was destroyed by the DevelopmentOnly component");
        }
    }
}