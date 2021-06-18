// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Components.Animations
{
    [AddComponentMenu("Evolunity/Animations/Fade")]
    [RequireComponent(typeof(Image))]
    public class Fade : InOutBehaviour
    {
        public float Duration = 1f;
        public Color Color = Color.black;

        private Image image;

        public bool IsPlaying { get; private set; }

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        protected override IEnumerator InCoroutine(Action onComplete = null)
        {
            IsPlaying = true;

            for (float i = 0; i <= Duration; i += Time.deltaTime)
            {
                image.color = new Color(Color.r, Color.g, Color.b, i / Duration);

                yield return null;
            }

            image.color = Color;
            IsPlaying = false;
            
            onComplete?.Invoke();
        }

        protected override IEnumerator OutCoroutine(Action onComplete = null)
        {
            IsPlaying = true;

            for (float i = Duration; i >= 0; i -= Time.deltaTime)
            {
                image.color = new Color(Color.r, Color.g, Color.b, i / Duration);

                yield return null;
            }

            image.color = Color.clear;
            IsPlaying = false;
            
            onComplete?.Invoke();
        }
    }
}