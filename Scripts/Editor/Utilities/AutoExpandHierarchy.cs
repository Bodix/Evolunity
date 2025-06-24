// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Evolutex.Evolunity.Editor.Utilities
{
    // TODO: Add UI for adding GameObject's to expand.

    // Use it as follows:
    // [InitializeOnLoad]
    // public static class EditorBootstrap
    // {
    //     static EditorBootstrap()
    //     {
    //         AutoExpandHierarchy.GameObjectsToExpand.Add("Environment");  
    //         AutoExpandHierarchy.GameObjectsToExpand.Add("Game");
    //         AutoExpandHierarchy.GameObjectsToExpand.Add("UI");
    //     }
    // }

    [InitializeOnLoad]
    public static class AutoExpandHierarchy
    {
        static AutoExpandHierarchy()
        {
            EditorSceneManager.sceneOpened += (scene, mode) => ExpandObjects();
        }

        private static void ExpandObjects()
        {
            GameObjectsToExpand.ForEach(name =>
            {
                GameObject gameObject = GameObject.Find(name);
                if (gameObject)
                    SceneHierarchy.SetExpanded(gameObject, true);
            });
        }

        public static List<string> GameObjectsToExpand { get; } = new List<string>();
    }
}