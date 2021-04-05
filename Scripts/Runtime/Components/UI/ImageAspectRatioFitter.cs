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
        private AspectRatioFitter aspectRatioFitter;
        private Image image;

        private void Start()
        {
            aspectRatioFitter = GetComponent<AspectRatioFitter>();
            image = GetComponent<Image>();

            Fit();
        }

        public void Fit()
        {
            if (image.sprite)
                aspectRatioFitter.aspectRatio = (float) image.sprite.texture.width / image.sprite.texture.height;
        }
    }
}