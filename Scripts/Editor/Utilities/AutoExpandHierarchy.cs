// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Evolutex.Evolunity.Editor.Utilities
{
    // TODO:
    // Add UI for adding GameObject's to expand.
    
    [InitializeOnLoad]
    public static class AutoExpandHierarchy
    {
        private static readonly List<GameObject> gameObjectsBuffer = new List<GameObject>();

        static AutoExpandHierarchy()
        {
            EditorSceneManager.sceneOpened += (scene, mode) =>
            {
                GameObjectsToExpand.ForEach(name =>
                {
                    GameObject gameObject = GameObject.Find(name);
                    if (gameObject)
                        gameObjectsBuffer.Add(gameObject);
                });

                foreach (GameObject gameObject in gameObjectsBuffer)
                    SceneHierarchy.SetExpanded(gameObject, true);

                gameObjectsBuffer.Clear();
            };
        }
        
        public static List<string> GameObjectsToExpand { get; } = new List<string>
        {
            "Player",
            "Logic",
            "Environment",
            "Test"
        };
    }
}