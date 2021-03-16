// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.IO;
using UnityEngine;

namespace Evolutex.Evolunity.Editor.Utilities
{
    public static class CameraScreenshot
    {
        private static readonly string path = Application.dataPath.Replace("/Assets", "/Screenshots");

        public static void Take()
        {
            Camera camera = Camera.main;
            RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

            RenderTexture cameraTargetTexture = camera.targetTexture;
            RenderTexture activeRenderTexture = RenderTexture.active;

            camera.targetTexture = renderTexture;
            RenderTexture.active = renderTexture;

            camera.Render();
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

            camera.targetTexture = cameraTargetTexture;
            RenderTexture.active = activeRenderTexture;
            UnityEngine.Object.Destroy(renderTexture);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileName = $"Screenshot_{Application.productName}_{DateTime.Now:dd-MM-yyyy_HH-mm-ss}.png";
            string filePath = Path.Combine(path, fileName);
            File.WriteAllBytes(filePath, texture.EncodeToPNG());

            Debug.Log("Screenshot saved as \"" + filePath + "\"");
        }
    }
}