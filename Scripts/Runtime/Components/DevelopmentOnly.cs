/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using UnityEngine;

namespace Evolunity.Components
{
    [AddComponentMenu("Toolkit/Development Only")]
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
            if (disable && disableMethod != DisableMethod.Destroy)
                Debug.Log("The GameObject \"" + gameObject.name + "\" was disabled by the DevelopmentOnly component",
                    this);
        }

        private void OnDestroy()
        {
            if (disable)
                Debug.Log("The GameObject \"" + gameObject.name + "\" was destroyed by the DevelopmentOnly component");
        }
    }
}