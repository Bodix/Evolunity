// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Evolutex.Evolunity.Extensions
{
    public static class MaterialExtensions
    {
        /// https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/Inspector/StandardShaderGUI.cs
        /// SetupMaterialWithBlendMode() method.
        public static void SetupRenderingMode(this Material material, RenderingMode renderingMode,
            bool changeRenderQueue = true)
        {
            switch (renderingMode)
            {
                case RenderingMode.Opaque:
                    material.SetOverrideTag("RenderType", "");
                    material.SetInt("_SrcBlend", (int)BlendMode.One);
                    material.SetInt("_DstBlend", (int)BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    if (changeRenderQueue)
                        material.renderQueue = -1;
                    break;
                case RenderingMode.Cutout:
                    material.SetOverrideTag("RenderType", "TransparentCutout");
                    material.SetInt("_SrcBlend", (int)BlendMode.One);
                    material.SetInt("_DstBlend", (int)BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    if (changeRenderQueue)
                        material.renderQueue = (int)RenderQueue.AlphaTest;
                    break;
                case RenderingMode.Fade:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    if (changeRenderQueue)
                        material.renderQueue = (int)RenderQueue.Transparent;
                    break;
                case RenderingMode.Transparent:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)BlendMode.One);
                    material.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    if (changeRenderQueue)
                        material.renderQueue = (int)RenderQueue.Transparent;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(renderingMode), renderingMode, null);
            }
        }

        public static MaterialRenderingProperties GetRenderingProperties(this Material material)
        {
            return new MaterialRenderingProperties(material);
        }
    }

    public enum RenderingMode
    {
        Opaque,
        Cutout,
        Fade,
        Transparent
    }

    public readonly struct MaterialRenderingProperties
    {
        public readonly string RenderType;
        public readonly int ScrBlend;
        public readonly int DstBlend;
        public readonly int ZWrite;
        public readonly bool IsAlphaTestOn;
        public readonly bool IsAlphaBlendOn;
        public readonly bool IsAlphaPreMultiplyOn;
        public readonly int RenderQueue;

        public MaterialRenderingProperties(Material material)
        {
            RenderType = material.GetTag("RenderType", false);
            ScrBlend = material.GetInt("_SrcBlend");
            DstBlend = material.GetInt("_DstBlend");
            ZWrite = material.GetInt("_ZWrite");
            IsAlphaTestOn = material.IsKeywordEnabled("_ALPHATEST_ON");
            IsAlphaBlendOn = material.IsKeywordEnabled("_ALPHABLEND_ON");
            IsAlphaPreMultiplyOn = material.IsKeywordEnabled("_ALPHAPREMULTIPLY_ON");
            RenderQueue = material.renderQueue;
        }

        public Material ApplyTo(Material material)
        {
            material.SetOverrideTag("RenderType", RenderType);
            material.SetInt("_SrcBlend", ScrBlend);
            material.SetInt("_DstBlend", DstBlend);
            material.SetInt("_ZWrite", ZWrite);
            if (IsAlphaTestOn)
                material.EnableKeyword("_ALPHATEST_ON");
            else material.DisableKeyword("_ALPHATEST_ON");
            if (IsAlphaBlendOn)
                material.EnableKeyword("_ALPHABLEND_ON");
            else material.DisableKeyword("_ALPHABLEND_ON");
            if (IsAlphaPreMultiplyOn)
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            else material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = RenderQueue;

            return material;
        }
    }
}