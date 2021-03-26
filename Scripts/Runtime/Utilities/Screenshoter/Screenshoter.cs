// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Utilities.Screenshoter
{
    [RequireComponent(typeof(Camera))]
    [DisallowMultipleComponent]
    public class Screenshoter : MonoBehaviour
    {
        private Camera cam;
        private ScreenshotSettings settings;
        private Action<Texture2D> callback;
        private bool takeScreenshot;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }

        public void TakeScreenshot(ScreenshotSettings settings, Action<Texture2D> callback)
        {
            this.settings = settings;
            this.callback = callback;

            takeScreenshot = true;
        }

        private void LateUpdate()
        {
            if (takeScreenshot)
            {
                CameraClearFlags clearFlags = cam.clearFlags;
                Color backgroundColor = cam.backgroundColor;
                bool isOrthographic = cam.orthographic;
                float size = isOrthographic ? cam.orthographicSize : cam.fieldOfView;
                int cullingMask = cam.cullingMask;

                SetupCamera();
                Texture2D texture = RenderScreen();

                cam.clearFlags = clearFlags;
                cam.backgroundColor = backgroundColor;
                cam.orthographic = isOrthographic;
                if (isOrthographic) cam.orthographicSize = size;
                else cam.fieldOfView = size;
                cam.cullingMask = cullingMask;

                takeScreenshot = false;
                
                callback?.Invoke(texture);
            }
        }

        private void SetupCamera()
        {
            if (!settings.Resolution.HasValue)
                settings.Resolution = new Vector2Int(Screen.width, Screen.height);

            if (settings.ClearFlags.HasValue)
                cam.clearFlags = settings.ClearFlags.Value;
            if (settings.BackgroundColor.HasValue)
                cam.backgroundColor = settings.BackgroundColor.Value;
            if (settings.IsOrthographic.HasValue)
                cam.orthographic = settings.IsOrthographic.Value;
            if (settings.OrthographicSize.HasValue)
                cam.orthographicSize = settings.OrthographicSize.Value;
            if (settings.FieldOfView.HasValue)
                cam.fieldOfView = settings.FieldOfView.Value;
            if (settings.IsIgnoreUI)
                cam.cullingMask = ~(1 << 5);
        }

        private Texture2D RenderScreen()
        {
            // Render screen to texture.
            RenderTexture renderTexture =
                new RenderTexture(settings.Resolution.Value.x, settings.Resolution.Value.y, 24);
            cam.targetTexture = renderTexture;
            cam.Render();

            // Read pixels from texture.
            RenderTexture.active = renderTexture;
            Texture2D screenshot = new Texture2D(
                settings.Resolution.Value.x,
                settings.Resolution.Value.y,
                TextureFormat.RGB24,
                false);
            screenshot.ReadPixels(new Rect(0, 0, settings.Resolution.Value.x, settings.Resolution.Value.y), 0, 0);

            // Destroy texture.
            cam.targetTexture = null;
            RenderTexture.active = null;
            Destroy(renderTexture);

            return screenshot;
        }
    }
}