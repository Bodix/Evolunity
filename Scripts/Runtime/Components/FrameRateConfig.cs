// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Components
{
    [AddComponentMenu("Evolunity/Frame Rate Config")]
    [DisallowMultipleComponent]
    public class FrameRateConfig : MonoBehaviour
    {
        [SerializeField]
        private int targetFrameRate = 120;
        [SerializeField]
        private int vSyncCount = 0;

        private void Start()
        {
            QualitySettings.vSyncCount = vSyncCount;
            Application.targetFrameRate = targetFrameRate;
        }
    }
}