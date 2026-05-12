// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

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
					sceneHierarchyWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");

				return sceneHierarchyWindowType;
			}
		}

		private static EditorWindow sceneHierarchyWindow;
		private static EditorWindow SceneHierarchyWindow
		{
			get
			{
				if (sceneHierarchyWindow == null)
					sceneHierarchyWindow = EditorWindow.GetWindow(SceneHierarchyWindowType);

				return sceneHierarchyWindow;
			}
		}

		private static MethodInfo setExpandedRecursiveMethod;
		private static MethodInfo SetExpandedRecursiveMethod
		{
			get
			{
				if (setExpandedRecursiveMethod == null)
					setExpandedRecursiveMethod = SceneHierarchyWindowType.GetMethod("SetExpandedRecursive",
						BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

				return setExpandedRecursiveMethod;
			}
		}

#if UNITY_6000
		private static MethodInfo setExpandedMethod;
		private static MethodInfo SetExpandedMethod
		{
			get
			{
				if (setExpandedMethod == null)
					setExpandedMethod = SceneHierarchyWindowType.GetMethod("SetExpanded",
						BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

				return setExpandedMethod;
			}
		}
#endif

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


		public static void SetExpanded(GameObject gameObject, bool isExpanded, bool isRecursive = false)
		{
			if (isRecursive)
				SetExpandedRecursiveMethod.Invoke(SceneHierarchyWindow, new object[] { gameObject.GetInstanceID(), isExpanded });
			else
#if UNITY_6000
				SetExpandedMethod.Invoke(SceneHierarchyWindow, new object[] { gameObject.GetInstanceID(), isExpanded });
#else
				ExpandTreeViewItemMethod.Invoke(SceneHierarchyObject, new object[] { gameObject.GetInstanceID(), isExpanded });
#endif
		}
	}
}