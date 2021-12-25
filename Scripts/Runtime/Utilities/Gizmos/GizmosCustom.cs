// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Utilities.Gizmos
{
    public static class GizmosCustom
    {
        public static void DrawRect(Rect rect, float thickness = 0.01f, float zPosition = 0f)
        {
            UnityEngine.Gizmos.DrawWireCube(
                new Vector3(rect.center.x, rect.center.y, zPosition),
                new Vector3(rect.size.x, rect.size.y, thickness));
        }
    }
}