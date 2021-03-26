// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.IO;
using UnityEngine;

namespace Evolutex.Evolunity.Utilities.Screenshoter
{
    public static class Screenshot
    {
        private static Screenshoter screenshoter;

        public static readonly string ScreenshotPath = Application.persistentDataPath;

        public static void TakeAndSave(ScreenshotSettings settings = null, bool destroyOnComplete = true)
        {
            settings = settings ?? new ScreenshotSettings();
            
            Take(bytes => File.WriteAllBytes(
                    Path.Combine(ScreenshotPath, settings.Name + ".png"),
                    bytes.EncodeToPNG()), 
                settings, 
                destroyOnComplete);
        }

        public static void Take(Action<Texture2D> callback, ScreenshotSettings settings = null, bool destroyOnComplete = true)
        {
            settings = settings ?? new ScreenshotSettings();

            if (destroyOnComplete)
                callback += bytes => UnityEngine.Object.Destroy(screenshoter);

            Camera camera = settings.Camera ? settings.Camera : Camera.main;
            if (!screenshoter)
                screenshoter = camera.gameObject.AddComponent<Screenshoter>();

            screenshoter.TakeScreenshot(settings, callback);
        }
    }
}