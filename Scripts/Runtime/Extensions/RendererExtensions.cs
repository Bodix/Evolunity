/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

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