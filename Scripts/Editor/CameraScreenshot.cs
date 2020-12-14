// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.IO;
using UnityEngine;

namespace Evolutex.Evolunity.Editor
{
    public static class CameraScreenshot
    {
        private static readonly string path = 
            $"{Application.dataPath.Replace("/Assets", string.Empty)}/Screenshots/";
        
        public static void Take()
        {
            Camera camera = Camera.main;
            RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

            camera.targetTexture = renderTexture;
            RenderTexture.active = renderTexture;

            camera.Render();
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height);
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

            camera.targetTexture = null;
            RenderTexture.active = null;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string filename = $"Screenshot_{Application.productName}_{DateTime.Now:dd-MM-yyyy_HH-mm-ss}.png";
            File.WriteAllBytes(path + filename, texture.EncodeToPNG());
            
            Debug.Log("Screenshot saved as \"" + path + filename + "\"");
        }
    }
}