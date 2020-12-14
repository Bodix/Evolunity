/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    // http://wiki.unity3d.com/index.php/LayerMaskExtensions
    public static class LayerMaskExtensions
    {
        public static LayerMask GetMask(params string[] layerNames)
        {
            return LayerMask.GetMask(layerNames);
        }

        public static LayerMask GetMask(params int[] layerNumbers)
        {
            if (layerNumbers == null)
                throw new ArgumentNullException(nameof(layerNumbers));

            LayerMask layerMask = 0;

            foreach (int layer in layerNumbers)
                layerMask |= 1 << layer;

            return layerMask;
        }

        public static LayerMask Inverse(this LayerMask layerMask)
        {
            return ~layerMask;
        }

        public static LayerMask AddToMask(this LayerMask layerMask, params string[] layerNames)
        {
            return layerMask | GetMask(layerNames);
        }

        public static LayerMask AddToMask(this LayerMask layerMask, params int[] layerNumbers)
        {
            return layerMask | GetMask(layerNumbers);
        }

        public static LayerMask RemoveFromMask(this LayerMask layerMask, params string[] layerNames)
        {
            return ~(~layerMask | GetMask(layerNames));
        }

        public static LayerMask RemoveFromMask(this LayerMask layerMask, params int[] layerNumbers)
        {
            return ~(~layerMask | GetMask(layerNumbers));
        }

        public static string[] GetLayerNames(this LayerMask layerMask)
        {
            List<string> names = new List<string>();

            for (int i = 0; i < 32; ++i)
            {
                int shiftedLayer = 1 << i;

                if ((layerMask & shiftedLayer) == shiftedLayer)
                {
                    string layerName = LayerMask.LayerToName(i);

                    if (!string.IsNullOrEmpty(layerName))
                        names.Add(layerName);
                }
            }

            return names.ToArray();
        }

        public static string AsString(this LayerMask layerMask)
        {
            return AsString(layerMask, ", ");
        }

        public static string AsString(this LayerMask layerMask, string separator)
        {
            return string.Join(separator, GetLayerNames(layerMask));
        }
    }
}