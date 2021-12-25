// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Components.UI
{
    [AddComponentMenu("Evolunity/UI/Image Aspect Ratio Fitter")]
    [RequireComponent(typeof(AspectRatioFitter), typeof(Image))]
    public class ImageAspectRatioFitter : MonoBehaviour
    {
        [HideInInspector]
        [SerializeField]
        private AspectRatioFitter aspectRatioFitter;
        [HideInInspector]
        [SerializeField]
        private Image image;

        private bool IsInitialized => aspectRatioFitter && image;

        private void OnValidate()
        {
            InitializeIfRequired();
        }

        private void Awake()
        {
            InitializeIfRequired();
        }

        private void Start()
        {
            Fit();
        }

        [ContextMenu("Fit")]
        public void Fit()
        {
            InitializeIfRequired();
            
            if (image.sprite)
                aspectRatioFitter.aspectRatio = (float)image.sprite.rect.width / image.sprite.rect.height;
            else
                Debug.LogWarning("Trying to use ImageAspectRatioFitter on Image without sprite");
        }

        private void InitializeIfRequired()
        {
            if (!IsInitialized)
            {
                aspectRatioFitter = GetComponent<AspectRatioFitter>();
                image = GetComponent<Image>();
            }
        }
    }
}