// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    public static class ObjectExtensions
    {
        // http://answers.unity.com/answers/1224404/view.html
        public static bool IsRealNull(this Object obj)
        {
            return ((object) obj) == null;
        }

        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;

            for (int i = 0, count = gameObject.transform.childCount; i < count; i++)
                gameObject.transform.GetChild(i).gameObject.SetLayerRecursively(layer);
        }
    }
}