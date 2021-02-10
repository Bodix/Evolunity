// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Evolutex.Evolunity.Editor.Hierarchy
{
    [InitializeOnLoad]
    public static class ExpandHierarchyObjects
    {
        private static readonly List<GameObject> gameObjectsBuffer = new List<GameObject>();

        static ExpandHierarchyObjects()
        {
            SubscribeToSceneOpened();
        }
        
        public static readonly List<string> gameObjectsToExpand = new List<string>
        {
            "Player",
            "Logic",
            "Environment"
        };

        private static void SubscribeToSceneOpened()
        {
            EditorSceneManager.sceneOpened += (scene, mode) =>
            {
                gameObjectsToExpand.ForEach(x => 
                    gameObjectsBuffer.Add(GameObject.Find(x)));
                
                foreach (GameObject gameObject in gameObjectsBuffer)
                    SceneHierarchyUtility.SetExpanded(gameObject, true);
                
                gameObjectsBuffer.Clear();
            };
        }
    }
}