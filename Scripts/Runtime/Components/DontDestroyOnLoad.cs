// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Components
{
    [AddComponentMenu("Evolunity/Don't Destroy On Load")]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-1)]
    public sealed class DontDestroyOnLoad : MonoBehaviour
    {
        [SerializeField]
        private bool log = true;
        
        private void Awake()
        {
            // DontDestroyOnLoad only works for root GameObjects or components on root GameObjects.
            transform.SetParent(null);
            
            DontDestroyOnLoad(gameObject);
            
            if (log)
                Debug.Log("The GameObject \"" + gameObject.name + "\" was set as DontDestroyOnLoad", this);
        }
    }
}