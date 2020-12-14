/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using UnityEngine;

namespace Evolunity.Components
{
    [AddComponentMenu("Toolkit/Don't Destroy On Load", 10000)]
    [DisallowMultipleComponent]
    public sealed class DontDestroyOnLoad : MonoBehaviour
    {
        [SerializeField]
        private bool log = true;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            if (log)
                Debug.Log("The GameObject \"" + gameObject.name + "\" was set as DontDestroyOnLoad", this);
        }
    }
}