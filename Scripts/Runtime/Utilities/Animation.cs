// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections;
using System.Linq;
using UnityEngine;
using Evolutex.Evolunity.Extensions;

namespace Evolutex.Evolunity.Utilities
{
    public static class Animation
    {
        public static IEnumerator FadeOut(GameObject target, float duration,
            float delay = 0, bool disableOnComplete = true)
        {
            if (delay > 0)
                yield return new WaitForSeconds(delay);

            yield return Fade(target, 1, 0, duration);

            if (disableOnComplete)
                target.SetActive(false);
        }

        public static IEnumerator FadeIn(GameObject target, float duration,
            float delay = 0, bool enableOnStart = true)
        {
            if (delay > 0)
                yield return new WaitForSeconds(delay);

            if (enableOnStart)
                target.SetActive(true);

            yield return Fade(target, 0, 1, duration);
        }

        /// <summary>
        /// A method for smoothly animating the transparency of a game object.
        /// 
        /// <para/>Algorithm:
        /// <br/>1. Caches materials properties on all active <see cref="MeshRenderer"/>s (optional).
        /// <br/>2. Sets rendering mode of these materials to Fade.
        /// <br/>3. Smoothly changes materials' alpha.
        /// <br/>4. Returns to the materials' previously cached properties (optional).
        ///
        /// <para/>Designed to use with `Standard` shaders.
        /// </summary>
        public static IEnumerator Fade(GameObject target, float fromAlpha, float toAlpha, float duration,
            bool preserveMaterialProperties = true)
        {
            Material[] materials = target.GetComponentsInChildren<MeshRenderer>()
                .SelectMany(x => x.materials).ToArray();
            MaterialRenderingProperties[] materialProperties = null;
            if (preserveMaterialProperties)
                materialProperties = materials.Select(x => x.GetRenderingProperties()).ToArray();

            materials.ForEach(material =>
            {
                material.SetupRenderingMode(RenderingMode.Fade);
                // To fix the overlap of transparent materials on top of each other.
                material.SetInt("_ZWrite", 1);
            });

            float progress = 0;
            while (progress < 1)
            {
                progress = Mathf.Clamp01(progress + Time.deltaTime / duration);

                float alpha = Mathf.Lerp(fromAlpha, toAlpha, progress);
                foreach (Material material in materials)
                    // TODO: Add Max()/Min() method for correct fading materials with intermediate alpha.
                    material.color = material.color.WithAlpha(alpha);

                yield return null;
            }

            if (preserveMaterialProperties)
                materials.ForEach((material, i) => materialProperties[i].ApplyTo(material));
        }
    }
}