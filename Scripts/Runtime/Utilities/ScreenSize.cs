// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Utilities
{
    // https://medium.com/@kunaltandon.kt/scaling-sprites-based-on-screen-resolutions-f28c47744ab5

    public static class ScreenSize
    {
        public static Vector2 GetWorldSize(Camera camera = null)
        {
            camera = camera ? camera : Camera.main;

            Vector2 topRightCorner = new Vector2(1, 1);

            return (camera.ViewportToWorldPoint(topRightCorner) - camera.transform.position) * 2;
        }

        public static float GetWorldHeight(Camera camera = null)
        {
            camera = camera ? camera : Camera.main;

            return GetWorldSize(camera).y;
        }

        public static float GetWorldWidth(Camera camera = null)
        {
            camera = camera ? camera : Camera.main;

            return GetWorldSize(camera).x;
        }
    }
}