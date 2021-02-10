// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Evolutex.Evolunity.Editor.Utilities
{
    //https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/SceneHierarchyWindow.cs
    public static class SceneHierarchy
    {
        private static Type sceneHierarchyWindowType;
        private static Type SceneHierarchyWindowType
        {
            get
            {
                if (sceneHierarchyWindowType == null)
                    sceneHierarchyWindowType =
                        typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");

                return sceneHierarchyWindowType;
            }
        }

        private static EditorWindow sceneHierarchyWindow;
        private static EditorWindow SceneHierarchyWindow
        {
            get
            {
                if (sceneHierarchyWindow == null)
                {
                    Object[] allWindows = Resources.FindObjectsOfTypeAll(SceneHierarchyWindowType);
                    if (allWindows.Length > 0)
                        sceneHierarchyWindow = (EditorWindow) allWindows[0];
                }

                return sceneHierarchyWindow;
            }
        }

        private static object sceneHierarchyObject;
        private static object SceneHierarchyObject
        {
            get
            {
                if (sceneHierarchyObject == null)
                    sceneHierarchyObject = SceneHierarchyWindowType
                        .GetProperty("sceneHierarchy")
                        .GetValue(SceneHierarchyWindow);

                return sceneHierarchyObject;
            }
        }

        private static Type sceneHierarchyType;
        private static Type SceneHierarchyType
        {
            get
            {
                if (sceneHierarchyType == null)
                    sceneHierarchyType = SceneHierarchyObject.GetType();

                return sceneHierarchyType;
            }
        }

        private static MethodInfo expandTreeViewItemMethod;
        private static MethodInfo ExpandTreeViewItemMethod
        {
            get
            {
                if (expandTreeViewItemMethod == null)
                    expandTreeViewItemMethod = SceneHierarchyType
                        .GetMethod("ExpandTreeViewItem", BindingFlags.Instance | BindingFlags.NonPublic);

                return expandTreeViewItemMethod;
            }
        }

        public static void SetExpanded(GameObject gameObject, bool isExpanded)
        {
            ExpandTreeViewItemMethod.Invoke(SceneHierarchyObject, new object[] { gameObject.GetInstanceID(), isExpanded });
        }
    }
}