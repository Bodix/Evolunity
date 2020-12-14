// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    public static class RendererExtensions
    {
        // http://wiki.unity3d.com/index.php?title=IsVisibleFrom
        public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }
    }
}