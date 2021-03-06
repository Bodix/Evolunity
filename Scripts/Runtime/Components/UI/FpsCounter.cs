﻿// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Components.UI
{
    // This makes no sense because the Game window is not updated every frame in the Editor.
    // [ExecuteAlways]
    // In the Editor, FPS can be obtained from the Stats panel in the Game window.
    [AddComponentMenu("Evolunity/UI/FPS Counter")]
    [RequireComponent(typeof(Text))]
    public class FpsCounter : MonoBehaviour
    {
        public float MeasurementPeriod = 0.5f;

        private Text text;
        private float nextMeasurementTime;
        private int fpsSinceLastMeasurement;

        public float CurrentFPS { get; private set; }

        private void Start()
        {
            text = GetComponent<Text>();
        }

        private void Update()
        {
            fpsSinceLastMeasurement++;

            if (Time.realtimeSinceStartup >= nextMeasurementTime)
            {
                CurrentFPS = Mathf.Round(fpsSinceLastMeasurement / MeasurementPeriod);

                nextMeasurementTime += MeasurementPeriod;
                fpsSinceLastMeasurement = 0;

                text.text = CurrentFPS.ToString();
            }
        }
    }
}