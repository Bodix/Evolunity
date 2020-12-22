// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Components
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