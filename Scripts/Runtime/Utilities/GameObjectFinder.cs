// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Linq;
using Evolutex.Evolunity.Extensions;
using UnityEngine;

namespace Evolutex.Evolunity.Utilities
{
    public static class GameObjectFinder
    {
        /// <summary>
        /// Use <see cref="TransformExtensions.GetIndexedPath"/> to get the indexed path.
        /// </summary>
        public static GameObject FindByIndexedPath(string indexedPath)
        {
            IndexedPathSegment[] segments = indexedPath
                .Split('/')
                .Select(ParsePathSegment)
                .ToArray();

            Transform transform = GameObject.Find(segments[0].Name).transform;
            for (int i = 1; i < segments.Length; i++)
            {
                IndexedPathSegment segment = segments[i];

                transform = transform.GetChild(segment.Index);
                if (transform.name != segment.Name)
                {
                    Debug.LogError($"Game object at index [{segment.Index}] should be \"{segment.Name}\", " +
                        $"but actual game object is \"{transform.name}\". This could happen if the hierarchy " +
                        $"has changed (order or presence of elements). Full path:\n" + indexedPath, transform);

                    return null;
                }
            }

            return transform.gameObject;
        }

        private static IndexedPathSegment ParsePathSegment(string pathSegment)
        {
            int openBracketIndex = pathSegment.LastIndexOf('[');
            int closeBracketIndex = pathSegment.LastIndexOf(']');

            return new IndexedPathSegment(
                pathSegment.Substring(0, openBracketIndex),
                int.Parse(pathSegment.Substring(openBracketIndex + 1, closeBracketIndex - openBracketIndex - 1)));
        }

        private struct IndexedPathSegment
        {
            public readonly string Name;
            public readonly int Index;

            public IndexedPathSegment(string name, int index)
            {
                Name = name;
                Index = index;
            }
        }
    }
}