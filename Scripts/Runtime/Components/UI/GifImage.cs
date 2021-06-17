// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Components.UI
{
    [AddComponentMenu("Evolunity/UI/GIF Image")]
    [RequireComponent(typeof(Image))]
    public class GifImage : MonoBehaviour
    {
        public float FramesPerSecond = 30f;

        [SerializeField]
        private Sprite[] sprites;

        [SerializeField]
        [HideInInspector]
        private Image image;

        public Sprite[] Sprites => sprites;
        public bool IsPlaying { get; private set; } = true;
        public bool IsContainsSprites => Sprites.Length != 0;

        public void OnValidate()
        {
            image = GetComponent<Image>();
        }

        private void Start()
        {
            if (!IsContainsSprites)
                IsPlaying = false;
        }

        private void Update()
        {
            if (IsPlaying)
            {
                int index = (int)(Time.time * FramesPerSecond) % Sprites.Length;

                image.sprite = Sprites[index];
            }
        }

        public void SetGif(Sprite[] sprites)
        {
            this.sprites = sprites;

            Reset();
        }

        public void Play()
        {
            if (!IsContainsSprites)
                throw new InvalidOperationException("Unable to play gif animation without sprites");

            IsPlaying = true;
        }

        public void Stop()
        {
            if (!IsPlaying)
                throw new InvalidOperationException("Unable to stop a gif animation that is not playing");

            IsPlaying = false;

            Reset();
        }

        private void Reset()
        {
            image.sprite = Sprites[0];
        }
    }
}