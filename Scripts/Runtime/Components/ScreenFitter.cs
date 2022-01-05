// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using Evolutex.Evolunity.Extensions;
using Evolutex.Evolunity.Utilities;
using UnityEngine;

namespace Evolutex.Evolunity.Components
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [AddComponentMenu("Evolunity/Screen Fitter")]
    public class ScreenFitter : MonoBehaviour
    {
        public FitMode Mode;
        public Vector2 AdditionalScale;

        private Vector3 _initialScale;

        private void Awake()
        {
            Fit();
        }

        private void Update()
        {
            if (Application.isEditor)
                Fit();
        }

        [ContextMenu("Fit")]
        public void Fit()
        {
            _initialScale = transform.localScale;
            Vector2 screenWorldSize = ScreenSize.GetWorldSize();

            switch (Mode)
            {
                case FitMode.Expand:
                    transform.localScale = (screenWorldSize + AdditionalScale).ToVector3().WithZ(_initialScale.z);
                    break;
                case FitMode.FitInScreen:
                    if (screenWorldSize.x < screenWorldSize.y)
                        transform.localScale = (Vector2.one * screenWorldSize.x + AdditionalScale)
                            .ToVector3().WithZ(_initialScale.z);
                    else
                        transform.localScale = (Vector2.one * screenWorldSize.y + AdditionalScale)
                            .ToVector3().WithZ(_initialScale.z);
                    break;
                case FitMode.EnvelopeScreen:
                    if (screenWorldSize.x > screenWorldSize.y)
                        transform.localScale = (Vector2.one * screenWorldSize.x + AdditionalScale)
                            .ToVector3().WithZ(_initialScale.z);
                    else
                        transform.localScale = (Vector2.one * screenWorldSize.y + AdditionalScale)
                            .ToVector3().WithZ(_initialScale.z);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Similar to AspectRatioFitter.
        public enum FitMode
        {
            Expand,
            FitInScreen,
            EnvelopeScreen
        }
    }
}