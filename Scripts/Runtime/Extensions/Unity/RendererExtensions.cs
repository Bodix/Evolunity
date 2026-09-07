// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Extensions
{
	public static class RendererExtensions
	{
		private static readonly int BaseColorPropertyId = Shader.PropertyToID("_BaseColor");
		private static readonly int ColorPropertyId = Shader.PropertyToID("_Color");
		private static readonly MaterialPropertyBlock PropertyBlock = new MaterialPropertyBlock();

		// http://wiki.unity3d.com/index.php?title=IsVisibleFrom
		public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
		{
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

			return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
		}

		public static void SetMainColor(this Renderer renderer, Color color)
		{
			renderer.GetPropertyBlock(PropertyBlock);
			PropertyBlock.SetColor(BaseColorPropertyId, color);
			PropertyBlock.SetColor(ColorPropertyId, color);
			renderer.SetPropertyBlock(PropertyBlock);
		}
	}
}