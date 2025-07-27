// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Components
{
    [AddComponentMenu("Evolunity/Set Parent On Awake")]
    public class SetParentOnAwake : MonoBehaviour
    {
        public Transform NewParent;
        public bool WorldPositionStays = true;

        private void Awake()
        {
            transform.SetParent(NewParent, WorldPositionStays);
        }
    }
}